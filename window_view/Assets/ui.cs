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



    // SendParameters is called when the user presses "OK"-button
    public void SendParameters()
    {
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

            target_coordinates.Set(0f, height, 0f);
            Quaternion target_rotation = Quaternion.Euler(0, direction, 0);

            targetEmpty.position = target_coordinates;
            targetEmpty.rotation = target_rotation;         
            

            Debug.Log(target_coordinates);
            Debug.Log(target_rotation);


        }
        catch (Exception)
        {
            Debug.Log("Virheellinen sy�te!");
        }
    }
}
