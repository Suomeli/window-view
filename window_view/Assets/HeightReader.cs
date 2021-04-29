using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System;
using System.Globalization;

/*
This script searches height of the input coordinates.
The file used is the the elevation model as a ascii-file (.xyz),
but should be able to use with any text-like file where each row
contain the coordinates and the height.

Necessary parameters are inputCoordinates and asciiFileLocation. InputCoordinates is for coordinates
in List format. asciiFileLocation is a string parameter for ascii file name. If the ascii file
is in a folder, remember to add the folder name to the string parameter. 
For example "/tiedostot/1x1m_672497.xyz"

Returns the height in int format. 
*/

public class HeightReader : MonoBehaviour {
	
	//Remove this void when complete.
	
	void Start () {
		
		int[] test = {25497014,6672000};
		double height = returnHeight(test, "/1x1m_672497.xyz");

		Debug.Log(height);
    }
	
	public double returnHeight(int[] inputCoordinates, string asciiFileLocation) {
		
		string line;
		
		// Create an variable for height.
		double height = 0;
		
		// Path of the ascii file.
        string path = Application.dataPath + asciiFileLocation;
		
		StreamReader file = new StreamReader(path);
		
		// Loop through rows in ascii file.
        while((line = file.ReadLine()) != null)  
		{
			// Split the row and save the results to an array.
			string[] rowArray = line.Split(char.Parse(" "));
			
			// Transforming the coordinates from file into int format.
			Debug.Log(rowArray[0]);
			float test;
			float lat = Single.TryParse(rowArray[0], NumberStyles.Any, out test);
			Debug.Log(test);
			float lon = Single.TryParse(rowArray[1], NumberStyles.Any, out lon);
			
			// Check that the east coordinate is correct.
			if (inputCoordinates[0] == lat){
				// Vheck that the north coordinate is correct.
				if (inputCoordinates[1] == lon){
					//When both are correct save the height.
					height=Convert.ToDouble(rowArray[2]);
					break;
				}
			}
		}
		
		//Check if no height were found.
		if (height == 0){
			Debug.Log("Did not find a height. Check coordinates for typos.");
		}
		else {
			Debug.Log("The height: "+height);
		}
		
		// return results
		return height;
	}
}