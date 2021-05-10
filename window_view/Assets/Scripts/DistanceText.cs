using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DistanceText : MonoBehaviour
{
    public Text distanceLabel;   
    
    //update text label position to be above the pin
    void Update()
    {
        Vector3 textPos = Camera.main.WorldToScreenPoint(this.transform.position);
        distanceLabel.transform.position = textPos;
    }
}
