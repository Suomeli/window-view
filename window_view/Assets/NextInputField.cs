using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NextInputField : MonoBehaviour{

    public TMP_InputField nextField;

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<TMP_InputField>().isFocused && Input.GetKeyDown(KeyCode.Tab))
        {
            nextField.ActivateInputField();
        }
    }
}