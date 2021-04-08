using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ui : MonoBehaviour
{
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
	void SendParameters ()
	{
		string adress = Input_adress.text;
		double height = Input_height.text;
		double direction = Input_direction.text;
		Debug.Log("Annettu osoite: " + adress + " Annettu korkeus: " + height + " Annettu suunta: " + direction);
	}
}
