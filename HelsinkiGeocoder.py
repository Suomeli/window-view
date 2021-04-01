import json
import sys
import re
from difflib import SequenceMatcher

class HelsinkiGeocoder():
    def __init__(self, addr_datafile, filter_func=None):
        self.filter_func = filter_func
        self.load_data(addr_datafile)
        self.pattern = re.compile("^([^\d^/]*)(\s*[-\d]*)(\s*[A-ö]*)")

    def argsort(self, seq):
        # this is to easily find the best candidate of the fuzzy sequence matching
        # http://stackoverflow.com/questions/3071415/efficient-method-to-calculate-the-rank-vector-of-a-list-in-python
        return sorted(range(len(seq)), key=seq.__getitem__)

    def load_data(self, addr_datafile):
        try:
            self.addresses = json.load(
            open(addr_datafile, 'r', 
            encoding='iso-8859-15'))['features']
        except:
            sys.exit('Couldn\'t read the address data. Is it GeoJSON?')

        # construct a dict for searching: top level keys are street names
        # value for a street name is a dict: keys are house numbers on that street
        self.addr_dict = {}
        for place in self.addresses:

            # Skip places filter returns false for
            if self.filter_func and not self.filter_func(place):
                continue

            street = place['properties']['katunimi'].lower()

            #if key for current street doesn't exist, add it
            if street not in self.addr_dict.keys():
                self.addr_dict[street.lower()] = {}

            #there are a couple of addresses with number None...index those by 0
            try:
                number = place['properties']['osoitenumero_teksti']
                number = number.lower() if number else '0'
                self.addr_dict[street][number] = place
            except KeyError:
                number = place['properties']['id']

    def geocode(self, address):
        lookup_addr = address.strip().lower()
        result = None

        # Get address-looking components from the input string
        hits = re.search(self.pattern, lookup_addr)
        street_name = hits.group(1).strip() #strip the trailing space
        building_number = hits.group(2).strip() if hits.group(2) else '0'
        next_letter = hits.group(3).strip() if hits.group(3) else None

        # === Start searching for a match for the input address ===
        # Try to find an exact match for the street name component
        try:
            street = self.addr_dict[street_name]

        # Nothing matching the exact street name found so try fuzzy matching
        except KeyError:
            all_streets = self.addr_dict.keys()
            # filter the streetnames by identical initial letters for performance
            candidates = [name for name in all_streets if name[:2] == street_name[:2]]
            if not candidates:
                # there is nothing that matches even the first 2 letters...
                return None
            # compute fuzzy matches
            ratios = [SequenceMatcher(lambda x: x == " ", street_name, candidate).ratio() for candidate in candidates]
            best_index = self.argsort(ratios)[-1]
            best_match = candidates[best_index]

            # only continue the search if similarity ratio exceeds ARBITRARY threshold
            # this just barely allows "hakaniemen halli" to be matched to "hakaniemen kauppahalli"
            if ratios[best_index] < 0.84:
                print(street_name.encode('ascii', errors='ignore'))
                return None

            street = self.addr_dict[best_match]

        # == Now that we found the street, match cases descending in accuracy ==
        # 1. Try to match the exact building number
        if building_number in street:
            return street[building_number]

        # 2. Try to match building number + proceeding letter ("10a" is valid but could by typoed as "10 A")
        elif next_letter:
            building_numbertext = ''.join([building_number, next_letter.lower()])
            try:
                return street[building_numbertext]
            except KeyError:
                pass

        # 3. Case "10-12": exact matching didnt work, so try with only the first part of the dashed number pair
        if '-' in building_number:
            try:
                return street[building_number.split('-')[0]]
            except KeyError:
                pass

        # All options exhausted, didn't find anything :(
        return None
