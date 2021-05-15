using System.IO;
using UnityEngine;
using System;

/*
This script searches height of the input coordinates from a text file with coordinates and heights.
The file used (and tested for) is the the elevation model as a ascii-file (.xyz),
but should be able to use with any text-like file where each row
contain the coordinates and the height in the shape of: "E N height".

Necessary parameters are inputCoordinates and asciiFileLocation. InputCoordinates is for coordinates
in int ARRAY format. asciiFileLocation is a string parameter for ascii file name. If the ascii file
is in a folder, remember to add the folder name to the string parameter. 
For example "/tiedostot/1x1m_672497.xyz"

This script takes into account the culturel differences with the decimal separator and could be therefore
used with any language system.

This script will use the closest coordinates if the perfect match of coordinates would not be found. 
This makes it posible to use bigger grids. 

This script will only search for the height in one elevation model file and return the closest height. 
To be able to search through multiple files for the correct height this scripts needs some changes. 
This is a possible implementation in the future.

Returns the height in float format. 
*/

public class HeightReader : MonoBehaviour {
	
	public float returnHeight(int[] inputCoordinates, string asciiFileLocation) {
        
		string line;
		float height = 0;
		double dist_closest = 100000;
		float height_closest = 0;
		
		// Path of the ascii file.
        string path = Application.dataPath + asciiFileLocation;
        
		StreamReader file = new StreamReader(path);

        // Loop through rows in ascii file.
        while ((line = file.ReadLine()) != null)
        {
            // Split the row and save the results to an array.
            string[] rowArray = line.Split(char.Parse(" "));

            // Transforming the coordinates from file string into int format.
            // Two different methdods depending on the computer language.
            int lat = 0;
            int lon = 0;
            float height_loop = 0;
			
            // If computer uses . as decimal seperator
            try
            {
                lat = Int32.Parse(rowArray[0]);
                lon = Int32.Parse(rowArray[1]);
                height_loop = Convert.ToSingle(rowArray[2]);
            }
            // If computer uses , as decimal seperator
            catch (FormatException)
            {
                string[] coord1 = rowArray[0].Split(char.Parse("."));
                string[] coord2 = rowArray[1].Split(char.Parse("."));
                string[] height1 = rowArray[2].Split(char.Parse("."));

                lat = Convert.ToInt32(Convert.ToSingle(coord1[0] + "." + coord1[1]));
                lon = Convert.ToInt32(Convert.ToSingle(coord2[0] + "." + coord2[1]));
                height_loop = Convert.ToSingle(height1[0] + "." + height1[1]);
            }
            
            // Cheking if the current row is the perfect match
			
            // Check that the east coordinate is correct.
            if (inputCoordinates[0] == lat){
				// Check that the north coordinate is correct.
				if (inputCoordinates[1] == lon){
					//When both are correct save the height.
					height = height_loop;
                    break;
				}
			}
			// when the correct coordinates has not been found keep track of the closest coordinates and its height.
			if (height == 0){
				//Calculate the distance between coordinates.
				double dist = Math.Sqrt(Math.Pow((inputCoordinates[0] - lat), 2) + Math.Pow((inputCoordinates[1] - lon), 2));
				// If the current row has closer distance to the wanted coordinates
				if (dist < dist_closest){
                    //save the closest coordinates height value and update the closest distance.
                    height_closest = height_loop;
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