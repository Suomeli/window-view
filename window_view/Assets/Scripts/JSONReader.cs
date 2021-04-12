using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using SimpleJSON;

public class JSONReader : MonoBehaviour
{
    string path;
    string testString;
    public TextAsset jsonFile;
    // Start is called before the first frame update
    void Start()
    {
        path = Application.dataPath + "/osoitteet_hki.json";
        testString = File.ReadAllText(path);
        JSONNode data = JSON.Parse(testString);

        foreach(JSONNode feature in data["features"]) {
            Debug.Log(feature["geometry"]["coordinates"]);
            break;
        }

        // path = Application.dataPath + "/osoitteet_hki.json";
        // Debug.Log(path);
        // testString = File.ReadAllText(path);
        // Processjson(jsonFile);

        // path = Application.dataPath + "/osoitteet_hki.json";
        // testString = File.ReadAllText(path);
        // ListRecords listRecords = JsonUtility.FromJson<ListRecords> (jsonString);
        // Debug.Log(listRecords);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    

    // private void Processjson(TextAsset jsonFile) {
    //     // ListResults listResults = JsonUtility.FromJson<ListResults>(jsonFile.text);
    //     // Debug.Log(listResults.results[0]);
    //     ListFeatures listFeatures = JsonUtility.FromJson<ListFeatures>(jsonFile.text);
    //     Debug.Log(listFeatures.features[0]);
    //     Debug.Log(listFeatures.features[1]);


    // }

    // [System.Serializable]
    // public class Properties {
    //     public string katunimi;
    // }

    // public class ListFeatures {
    //     // public List<Features> results;
    //     public Properties[] features;
    // }

}

// [System.Serializable]
// public class Feature {
//     public string type;
// }
