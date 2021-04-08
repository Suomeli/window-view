using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ui : MonoBehaviour
	
	public InputField input_adress;
	public InputField input_height;
	public InputField input_direction;
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
		string adress = input_adress.text;
		double height = input_height.text;
		double direction = input_direction.text;
		Debug.Log("Annettu osoite: " + adress + " Annettu korkeus: " + height + " Annettu suunta: " + direction);
	}
}
