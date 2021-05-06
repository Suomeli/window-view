using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
This script consists of two functions, which are RaycastForward and RaycastBackwards.

RaycastForward searches for surfaces of buildings in front of the camera. 
If conditions are met the camera moves to the otherside of the building at front. 
If conditions are not met print a message to the Unity console.

RaycastBackwards searches for surfaces of buildings behind the camera. 
If conditions are met the camera moves to the surface of the building 
that is behind current location/building. 
If conditions are not met print a message to the Unity console.
*/

public class ChangeBuildingSide : MonoBehaviour
{
    public Transform targetEmpty;
    public LayerMask layer;
    private Vector3 difference = Vector3.zero;

    // Search for building surfaces that are in front of the curren location/building
    public void RaycastForward() {
        // Raycast hits for rays
        RaycastHit hit1;
        RaycastHit hit2;
        RaycastHit hit3;

        // Check if there is a building in front of the camera (ignores meshes from the wrong side)
        if (Physics.Raycast(targetEmpty.position + targetEmpty.forward * 0.1f, targetEmpty.forward, out hit1, 100, layer)) {
            // If building found, cast ray through the building and search for another structure behind it
            if (Physics.Raycast(hit1.point + targetEmpty.forward * 0.1f, targetEmpty.forward, out hit2, 100, layer)) {
                    // If building found, cast ray back from the surface
                    if (Physics.Raycast(hit2.point, targetEmpty.forward * -1, out hit3, 100, layer)) {
                        difference = targetEmpty.position - hit3.point;
                        targetEmpty.position = targetEmpty.position - difference;
                    }
            }
            
            else {
                    // If no buildings in sight, cast ray back from 100 m away
                    if (Physics.Raycast(hit1.point + targetEmpty.forward * 100, targetEmpty.forward * -1, out hit2, 200, layer)) {
                        difference = targetEmpty.position - hit2.point;
                        targetEmpty.position = targetEmpty.position - difference;
                    }
            }
            
            //move target 10cm away from wall
            targetEmpty.position = targetEmpty.position + targetEmpty.forward * 0.1f;
        }
        
        // Debug message to the console if nothing found
        else {
            Debug.Log("Surface not found. Make sure there is a building in front of the camera that is close enough.");
        }

    }

    // Search for building surfaces behind the current location/building
    public void RaycastBackwards() {
        // Raycast hits for rays
        RaycastHit hit1;
        RaycastHit hit2;

        // Check if there is a surface behind camera
        if (Physics.Raycast(targetEmpty.position, targetEmpty.forward * -1, out hit1, 100, layer)) {
            // If surface found, cast another ray through the strcuture and search for another surface within 100 m
            if (Physics.Raycast(hit1.point - targetEmpty.forward * 0.1f, targetEmpty.forward * -1, out hit2, 100, layer)) {
                difference = targetEmpty.position - hit2.point;
                targetEmpty.position = targetEmpty.position - difference;
            }

            //move target 10cm away from wall
            targetEmpty.position = targetEmpty.position + targetEmpty.forward * 0.1f;
        }

        else {
            Debug.Log("Surface not found");
        }
    }
}
