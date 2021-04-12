using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Globalization;

public class ui : MonoBehaviour { 
	
	public InputField input_adress;
	public InputField input_height;
	public InputField input_direction;

    // Start is called before the first frame update
    void Start()
    {
        //Here code that clears the text in the input fields
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // SendParameters is called when the user presses "OK"-button
    void SendParameters()
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
