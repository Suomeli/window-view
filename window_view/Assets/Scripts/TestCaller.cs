using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCaller : MonoBehaviour
{
    AddressReader addressReader;
    public string address;
    public List<int> coordinates;

    // Start is called before the first frame update
    void Start()
    {
        addressReader = GetComponent<AddressReader>();
    }

    // Update is called once per frame
    void Update()
    {
        // Activate ReturnCoordinates function when space is pressed in play mode
        if (Input.GetKeyDown(KeyCode.Space)) {
            addressReader.ReturnCoordinates(address,"/osoitteet_hki.json",coordinates);
            Debug.Log(coordinates);
        }
    }
}
