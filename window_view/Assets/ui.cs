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

    public float floor_height = 3f;
    public float ground_height = 0f;



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
            string adress = input_adress.text;
            float height = Convert.ToSingle(Convert.ToDouble(input_height.text, formatinfo));
            float direction = Convert.ToSingle(Convert.ToDouble(input_direction.text, formatinfo));
            Debug.Log("Annettu osoite: " + adress + " Annettu korkeus: " + height + " Annettu suunta: " + direction);

            ///target height from floors and terrain

            float target_height = height * floor_height + ground_height;

            ///set target values to variables
            target_coordinates.Set(0f, target_height, 0f);
            Quaternion target_rotation = Quaternion.Euler(0, direction, 0);

            ///move target empty object to target values of transform and rotation
            targetEmpty.position = target_coordinates;
            targetEmpty.rotation = target_rotation;         
            
            ///show in console
            Debug.Log(target_coordinates);
            Debug.Log(target_rotation);


        }
        ///faulty input
        catch (Exception)
        {
            Debug.Log("Virheellinen syöte!");
        }
    }
}
