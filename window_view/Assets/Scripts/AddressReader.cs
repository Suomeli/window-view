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
*/
public class AddressReader : MonoBehaviour
{
    public List<int> returnCoordinates(string inputAddress, string jsonFileLocation) {
        // Create an empty list for coordinates
        List<int> coordinates = new List<int>();
        
        // Trim whitespaces from front and end of the string. Replace multiple whitespace with one white space.
        // Split the string and save the results to an array.
        inputAddress = inputAddress.Trim();
        inputAddress = Regex.Replace(inputAddress,@"\s+"," ");
        string[] stringArray = inputAddress.Split(char.Parse(" "));

        // Store street name. Check if the selected indexes of array are null or not and save necessary data based on that.
        string streetName = stringArray[0];
        string buildingNumber = stringArray.ElementAtOrDefault(1) != null ? stringArray[1] : null;
        string buildingLetter = stringArray.ElementAtOrDefault(2) != null ? stringArray[2] : null;

        // Path of the json file. Read all lines into a string. Parse the json string via simpleJSON plugin
        string path = Application.dataPath + jsonFileLocation;
        string jsonString = File.ReadAllText(path);
        JSONNode data = JSON.Parse(jsonString);

        // Loop through features in json file
        foreach(JSONNode location in data["features"]) {
            // Check if the current street name matches json files street name
            if (streetName == location["properties"]["katunimi"]) {
                // Check if the current osoitenumero_teksti is not null and matches the buildingNumber variable
                if (buildingNumber == location["properties"]["osoitenumero_teksti"] && location["properties"]["osoitenumero_teksti"] != null) {
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

