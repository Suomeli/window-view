using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using SimpleJSON;

/* 
This script searches for coordinates of input address from osoitteet_hki.json file.
Necessary parameters are inputAddress and jsonFileLocation. InputAddress is for address
in string format. JsonFileLocation is a string parameter for json file name. If the json file
is in a folder, remember to add the folder name to the string parameter. 
For example "/tiedostot/osoitteet_hki.json"

Currently returns coordinates in int format. Float does not work as intended at the moment because
of scientific notation. Unity shortens too large values. InputAddress string has to be exactly as
the address in json file. There is no fuzzy search.

sources:
SimpleJSON by Bunny83
Regular expression and code snippets translated to C# from HelsinkiGeocoder.py script by Henri Kotkanen and Jussi Arpalahti
*/
public class AddressReader : MonoBehaviour
{
    public List<int> returnCoordinates(string inputAddress, string jsonFileLocation) {
        // Create an empty list for coordinates
        List<int> coordinates = new List<int>();
        
        // Trim whitespaces from front and end of the string. Replace multiple whitespace with one white space.
        inputAddress = inputAddress.Trim();
        inputAddress = Regex.Replace(inputAddress,@"\s+"," ");

        // Regular expression that is used to create groups of the input address
        Regex regex = new Regex(@"^([^\d^/]*)(\s*[-\d]*)(\s*[A-ö]*)");
        GroupCollection group = regex.Match(inputAddress).Groups;

        // Storing group values at certain index to new variables
        string streetName = group[1].Value.Trim();
        string buildingNumber = group[2].Value.Any() ? group[2].Value.Trim() : null;
        string buildingLetter = group[3].Value.Any() ? group[3].Value.Trim() : null;

        // Combining building number and letter for later when comparing addresses
        string buildingNumLet = buildingNumber != null ? buildingNumber : null;
        buildingNumLet = buildingNumLet != null && buildingLetter != null ? buildingNumber+buildingLetter.ToLower() : buildingNumber;

        // Path of the json file. Read all lines into a string. Parse the json string via simpleJSON plugin
        string path = Path.Combine(Application.streamingAssetsPath,jsonFileLocation);
        string jsonString = File.ReadAllText(path);
        JSONNode data = JSON.Parse(jsonString);

        // Loop through features in json file
        foreach(JSONNode location in data["features"]) {

            // Check if the current street name matches json files street name
            if (streetName.ToLower() == location["properties"]["katunimi"].Value.ToLower() || streetName.ToLower() == location["properties"]["gatan"].Value.ToLower()) {
                // Check if the current osoitenumero_teksti is not null and matches the buildingNumber variable
                if (buildingNumLet == location["properties"]["osoitenumero_teksti"] && buildingNumLet != null) {
                    // Debug.Log(location["properties"]["osoitenumero_teksti"]);
                    // Add coordinates to list
                    coordinates.Add(location["geometry"]["coordinates"][0]);
                    coordinates.Add(location["geometry"]["coordinates"][1]);
                }

                // Check if the osoitenumero_teksti is null
                else if (location["properties"]["osoitenumero_teksti"] == null) {
                    // Add coordinates to list
                    coordinates.Add(location["geometry"]["coordinates"][0]);
                    coordinates.Add(location["geometry"]["coordinates"][1]);
                }
            }
        }

        // Simple checker if list is empty or not
        if (coordinates.Count.Equals(0)) {
            Debug.Log("Did not find coordinates. Check address for typos.");
        }

        else {
            Debug.Log(coordinates[0] + "," + coordinates[1]);
        }

        // return results
        return coordinates;
    }

}

