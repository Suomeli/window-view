using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using SimpleJSON;
using System.Linq;

public class JSONReader : MonoBehaviour
{
    string path;
    string jsonString;
    string[] stringArray;
    string streetName;
    string buildingNumber;
    string buildingLetter;
    public string inputLocation;
    public List<int> coordinates;
    
    // Start is called before the first frame update
    void Start()
    {
        // Trim whitespaces from front and end of the string
        inputLocation = inputLocation.Trim();
        // Split the string and save the results into an array
        stringArray = inputLocation.Split(char.Parse(" "));
        
        // Streetname
        streetName = stringArray[0];
        // Check if the array is not null at the index location. If true save building number. If not save 0 value.
        buildingNumber = stringArray.ElementAtOrDefault(1) != null ? stringArray[1] : "0";
        // Check if the array is not null at the index location. If true save building number. If not save null.
        buildingLetter = stringArray.ElementAtOrDefault(2) != null ? stringArray[2] : null;

        // Testing of results
        Debug.Log(streetName);
        Debug.Log(buildingNumber);
        Debug.Log(buildingLetter);

        // Path of the json file
        path = Application.dataPath + "/osoitteet_hki.json";
        // Read all lines into a string
        jsonString = File.ReadAllText(path);
        // Parse the json string via simpleJSON plugin
        JSONNode data = JSON.Parse(jsonString);

        // Loop through features in json file
        foreach(JSONNode location in data["features"]) {
            // Check if current street name matches the name in json file
            if (streetName == location["properties"]["katunimi"]) {
                // Check if current building number matchest json files number
                if (buildingNumber == location["properties"]["osoitenumero_teksti"]) {
                    // Add coordinates to an empty list
                    coordinates.Add(location["geometry"]["coordinates"][0]);
                    coordinates.Add(location["geometry"]["coordinates"][1]);
                    Debug.Log(coordinates[0]+","+coordinates[1]);

                }
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}