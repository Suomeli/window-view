using System.IO;
using UnityEngine;
using System;

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
        while ((line = file.ReadLine()) != null)
        {
            // Split the row and save the results to an array.
            string[] rowArray = line.Split(char.Parse(" "));

            // Transforming the coordinates from file into int format. Two different methdods depending on the computer language.
            //
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
            
            
            // Check that the east coordinate is correct.
            if (inputCoordinates[0] == lat){
				// Check that the north coordinate is correct.
				if (inputCoordinates[1] == lon){
					//When both are correct save the height.
					height = height_loop;
                    Debug.Log(height);
                    break;
				}
			}
			// when the correct coordinates has not been found keep track of the closest coordinates.
			if (height == 0){
				//Calculate the distance between coordinates.
				double dist = Math.Sqrt(Math.Pow((inputCoordinates[0] - lat), 2) + Math.Pow((inputCoordinates[1] - lon), 2));
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