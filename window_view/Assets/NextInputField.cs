using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class NextInputField : MonoBehaviour{
    
    public Camera maincamera;
    public TMP_InputField nextField;
    public bool active;

    // Update is called once per frame
    void Update()
    {
        //activate next field when tab is pressed
        if (GetComponent<TMP_InputField>().isFocused && Input.GetKeyDown(KeyCode.Tab))
        {
            nextField.ActivateInputField();
        }
        else
        {
            //exit freemode when field is active
        CameraMove cameramove = maincamera.GetComponent<CameraMove>();

        if (GetComponent<TMP_InputField>().isFocused)
        {
                cameramove.UIactive = true;
                cameramove.FreeMode = false;
        }
        else
        {
            cameramove.UIactive = false;
        }
        }

        
        
    }
}