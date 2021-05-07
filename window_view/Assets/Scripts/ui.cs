using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Globalization;
using TMPro;

public class ui : MonoBehaviour { 
	
	public TMP_InputField input_adress;
	public TMP_InputField input_height;
	public TMP_InputField input_direction;
    public Vector3 target_coordinates = Vector3.zero;
    public Transform targetEmpty;

    public bool test_mode;

    public float floor_height = 3f;
    public float ground_height = 0f;

    public string test_address; 
    AddressReader addressReader;
    HeightReader heightReader;
    public List<int> coordinates;

    // coordinates for tie-point in real life and in engine
    public Vector3 coordinateRealLife = new Vector3(25497471.56f, 0f, 6672725.34f);
    public Transform coordinateTarget;

    //variable to check if input has changed for getting new coordinates
    private string previousaddress = "";


    public LayerMask layer;
    private Vector3 difference = Vector3.zero;



    // Start is called before the first frame update
    void Start()
    {
        // Returns script component of the gameobject addressreader. If component is not found returns null;
        addressReader = GetComponent<AddressReader>();
        heightReader = GetComponent<HeightReader>();
    }


    // SendParameters is called when the user presses "OK"-button
    public void SendParameters()
    {
        ///check for proper input
        try
        {
            ///Define possible decimal separators of number format
            NumberFormatInfo formatinfo = new NumberFormatInfo();
            formatinfo.NumberDecimalSeparator = ".";
            formatinfo.NumberGroupSeparator = ",";

            ///read inputs into variables
            ///nuber variables are converted from text to double and then to float
            string address = input_adress.text;
            float height = Convert.ToSingle(Convert.ToDouble(input_height.text, formatinfo));
            float direction = Convert.ToSingle(Convert.ToDouble(input_direction.text, formatinfo));
            Debug.Log("Annettu osoite: " + address + " Annettu korkeus: " + height + " Annettu suunta: " + direction);

            // Save coordinates into a list
            // if address has changed, get new coordinates and ground height
            if (previousaddress.Equals(address) == false){
                //test_address 
                if (test_mode == true)
                {
                    address = test_address;
                }
                //coordinates
                coordinates = addressReader.returnCoordinates(address, "/data/osoitteet_hki.json");
                previousaddress = address;

                //ground height
                Debug.Log("Getting new height");
                int[] toHeightReader = new int[] { coordinates[0], coordinates[1] };
                ground_height = heightReader.returnHeight(toHeightReader, "/data/1x1m_672497.xyz");
            }


            ///target height from floors and terrain
            float target_height = 1 + (height - 1) * floor_height + ground_height;

            ///set target values to variables
            Vector3 coordinateTransformation = coordinateTarget.position - coordinateRealLife;
            target_coordinates.Set(coordinates[0] + coordinateTransformation[0], target_height, coordinates[1] + coordinateTransformation[2]);
            Quaternion target_rotation = Quaternion.Euler(0, direction, 0);

            ///move target empty object to target values of transform and rotation
            targetEmpty.position = target_coordinates;
            targetEmpty.rotation = target_rotation;

            //raycasting

            ///hits for forward and backward rays
            RaycastHit hit1;
            RaycastHit hit2;

            ///check if there is building in front of the camera (ignores meshes from the wrong side)
            if (Physics.Raycast(targetEmpty.position, targetEmpty.forward, out hit1, 100, layer))
            {
                ///if is, check back if wall is closer than camera
                if (Physics.Raycast(hit1.point, targetEmpty.forward * -1, out hit2, 100, layer))
                {
                    ///if wall closer than camera, move target to the wall
                    difference = targetEmpty.position - hit2.point;
                    targetEmpty.position = targetEmpty.position - difference;
                    
                }
            }
            else
            {
                ///if no buildings in sight, cast ray back from 100 m away
                if (Physics.Raycast(targetEmpty.position + targetEmpty.forward * 100, targetEmpty.forward * -1, out hit2, 200, layer))
                {
                    ///if building hit behind camera, move target to wall
                    difference = targetEmpty.position - hit2.point;
                    targetEmpty.position = targetEmpty.position - difference;
                    
                }
            }
            //move target 10cm away from wall
            targetEmpty.position = targetEmpty.position + targetEmpty.forward * 0.1f;

        }
        ///faulty input
        catch (Exception)
        {
            Debug.Log("Virheellinen syöte!");
        }
    }
}
