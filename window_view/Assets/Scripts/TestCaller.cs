using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Testing area for AddressReader script.
Works only if this script and AddressReader script are
in attached to same object. Getcomponent searches for the
script that is attached to the same gameobject.
*/

public class TestCaller : MonoBehaviour
{
    AddressReader addressReader;
    public string address;
    public List<int> coordinates;

    // Start is called before the first frame update
    void Start()
    {
        // Returns script component of the gameobject. If component is not found returns null;
        addressReader = GetComponent<AddressReader>();
    }

    // Update is called once per frame
    void Update()
    {
        // Activate ReturnCoordinates function when space is pressed in play mode
        if (Input.GetKeyDown(KeyCode.Space)) {
            // Save coordinates into a list
            coordinates = addressReader.returnCoordinates(address,"/osoitteet_hki.json");
        }
    }
}
