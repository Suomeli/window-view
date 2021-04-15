using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using SimpleJSON;
using System.Linq;

public class AddressReader : MonoBehaviour
{
    public void ReturnCoordinates(string inputAddress, string jsonFileLocation, List<int> coordinates) {
        // Trim whitespaces from front and end of the string
        inputAddress = inputAddress.Trim();
        // Split the string and save the results into an array
        string[] stringArray = inputAddress.Split(char.Parse(" "));

        // Store street name from array to a variable
        string streetName = stringArray[0];
        // Check if the array is not null at the index location. If true save building number. If not save 0 value.
        string buildingNumber = stringArray.ElementAtOrDefault(1) != null ? stringArray[1] : "0";
        // Check if the array is not null at the index location. If true save building number. If not save null.
        string buildingLetter = stringArray.ElementAtOrDefault(2) != null ? stringArray[2] : null;

        // Path of the json file
        string path = Application.dataPath + jsonFileLocation;
        // Read all lines into a string
        string jsonString = File.ReadAllText(path);
        // Parse the json string via simpleJSON plugin
        JSONNode data = JSON.Parse(jsonString);
        // Clear list from data
        coordinates.Clear();

        // Loop through features in json file
        foreach(JSONNode location in data["features"]) {
            // Check if current street name matches the name in json file
            if (streetName == location["properties"]["katunimi"]) {
                // Check if current building number matchest json files number
                if (buildingNumber == location["properties"]["osoitenumero_teksti"]) {
                    // Add coordinates to an empty list
                    coordinates.Add(location["geometry"]["coordinates"][0]);
                    coordinates.Add(location["geometry"]["coordinates"][1]);
                }
            }
        }
    }
}
