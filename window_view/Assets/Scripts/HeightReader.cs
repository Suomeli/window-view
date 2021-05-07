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
in int ARRAY format. asciiFileLocation is a string parameter for ascii file name. If the ascii file
is in a folder, remember to add the folder name to the string parameter. 
For example "/tiedostot/1x1m_672497.xyz"

Returns the height in float format. 
*/

public class HeightReader : MonoBehaviour {
	
	public float returnHeight(int[] inputCoordinates, string asciiFileLocation) {
        
		string line;
		
		// Create an variable for height, dist and closest height.
		float height = 0;
		double dist_closest = 100000;
		float height_closest = 0;
		
		// Path of the ascii file.
        string path = Application.dataPath + asciiFileLocation;
        
		StreamReader file = new StreamReader(path);
		
		// Loop through rows in ascii file.
        while((line = file.ReadLine()) != null)  
		{
			// Split the row and save the results to an array.
			string[] rowArray = line.Split(char.Parse(" "));
			
			// Cutting the .000 out from the coordinates in the ascii file so that parse can be used on all computers. 
			//Parse is language depending (if . or , in float).
			string[] coord1 = rowArray[0].Split(char.Parse("."));
			string[] coord2 = rowArray[1].Split(char.Parse("."));
			
			// Transforming the coordinates from file into int format.
			int lat = Int32.Parse(coord1[0]);
			int lon = Int32.Parse(coord2[0]);
            
            // Check that the east coordinate is correct.
            if (inputCoordinates[0] == lat){
				// Vheck that the north coordinate is correct.
				if (inputCoordinates[1] == lon){
					//When both are correct save the height.
					height = Convert.ToSingle(rowArray[2].Split(char.Parse("."))[0]);
					break;
				}
			}
			// when the correct coordinates has not been found keep track of the closest coordinates.
			if (height == 0){
				//Calculate the distance between coordinates.
				double dist = Math.Sqrt(Math.Pow((inputCoordinates[0] - lat), 2) + Math.Pow((inputCoordinates[1] - lon), 2));
				if (dist < dist_closest){
                    //save the closest coordinates height value and update the closest distance.
                
                    height_closest = Convert.ToSingle(rowArray[2].Split(char.Parse("."))[0]);
					dist_closest = dist;
				}
			}
		}
		
		//Check if no height were found.
		// If no exact coordinates were found then use closest. 
		if (height == 0){
			height = height_closest;
		}
        
        // return results
        return height;
	}
}