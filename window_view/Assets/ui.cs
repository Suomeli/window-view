using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using InputField Input_adress;
using InputField Input_height;
using InputField Input_direction;

public class ui : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	// 
	public void SendParameters ()
	{
		adress = Input_adress.text;
		height = Input_height.text;
		direction = Input_direction.text;
		Debug.Log("Tämä lähettää nyt parametrit oikeisiin paikkoihin")
	}
}
