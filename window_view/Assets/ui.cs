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
            ///nuber variables are converted from text to doouble
            string adress = input_adress.text;
            double height = Convert.ToDouble(input_height.text, formatinfo);
            double direction = Convert.ToDouble(input_direction.text, formatinfo);
            Debug.Log("Annettu osoite: " + adress + " Annettu korkeus: " + height + " Annettu suunta: " + direction);

        }
        catch (Exception ex)
        {
            Debug.Log("Virheellinen syöte!");
        }
    }
}
