using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;





public class MeasureDistances : MonoBehaviour
{
    public Camera camera1;
    public LayerMask layer;

    private int debug_draw_time = 5;
    public GameObject marker;

    float FOV_horizontal = 68f;
    float FOV_vertical = 35;

    public Text distanceText;


    public void AnalyzeView()
    {
        RaycastHit hit1;
        RaycastHit furthest;


        //starting angles for raycasting from set FOV numbers
        int horizontal_angle = (int) (FOV_horizontal / 2 * -1);
        int vertical_angle = (int) (FOV_vertical / 2 * -1);
        
        // angle between two rays in the sparse raycast grid
        float angle_change = 1;
        // distance initialized
        float furthest_distance = 0;


        //forming the basic grid of raycasts
        for (int y = vertical_angle; y <= vertical_angle * -1; y++)
        {
            for (int x = horizontal_angle; x <= horizontal_angle * -1; x++)
            {
                //world direction angle based on grid direction
                Vector3 direction = Quaternion.Euler(0, x, y) * camera1.transform.forward;

                //cast a ray, if hit measure distance and see if grid is dense enough
                if (Physics.Raycast(camera1.transform.position, direction, out hit1, Mathf.Infinity, layer))
                {
                    Debug.DrawRay(camera1.transform.position, direction * hit1.distance, Color.red, debug_draw_time);

                    //calculate measurement distance from angle, if distance larger than 1 m, decrease angle recursively
                    if (2 * Math.Tan((Math.PI / 180) * angle_change) * hit1.distance > 1)
                    {
                        RaycastHit output = CastRay(angle_change, direction, hit1);

                        //if furthest distance is returned, store the furthes ray (recursion)
                        if (output.distance > furthest_distance)
                        {
                            furthest = output;
                            furthest_distance = furthest.distance;
                            //Debug.Log(furthest.distance);
                            marker.transform.position = furthest.point;
                            distanceText.text = ((int) furthest.distance).ToString() + " m";

                        }
                        
                    }
                    //if furthest distance is returned, store the furthes ray (1 deg increment)
                    if (hit1.distance > furthest_distance)
                    {
                        furthest = hit1;
                        furthest_distance = furthest.distance;
                        //Debug.Log(furthest.distance);
                        marker.transform.position = furthest.point;
                        distanceText.text = ((int) furthest.distance).ToString() + " m";

                    }

                }

            }

        }

    }

    ///casting new ray from previous center
    public RaycastHit CastRay(float angle_change, Vector3 direction, RaycastHit parent)
    {
        float new_angle = angle_change / 2;

        RaycastHit hit;

        RaycastHit hit_up;
        RaycastHit hit_right;
        RaycastHit hit_down;
        RaycastHit hit_left;

        Vector3 angle_up = Quaternion.Euler(0, 0, new_angle) * direction;
        Vector3 angle_right = Quaternion.Euler(0, new_angle, 0) * direction;
        Vector3 angle_down = Quaternion.Euler(0, 0, -new_angle) * direction;
        Vector3 angle_left = Quaternion.Euler(0, -new_angle, 0) * direction;

        hit = parent;

        //cast up
        if (Physics.Raycast(camera1.transform.position, angle_up, out hit_up, Mathf.Infinity, layer))
        {
            Debug.DrawRay(camera1.transform.position, angle_up * hit_up.distance, Color.yellow, debug_draw_time);
            if (hit_up.distance > hit.distance)
            {
                hit = hit_up;
            }
            //calculate view angle covered distance from angle, if distance larger than 1 m, decrease angle recursively
            if (2 * Math.Tan((Math.PI / 180) * new_angle) * hit_up.distance > 1)
            {
                RaycastHit output = CastRay(new_angle, angle_up, hit_up);

                if (output.distance > hit.distance)
                {
                    hit = output;
                }
            }
            
        }
        //cast right
        if (Physics.Raycast(camera1.transform.position, angle_right, out hit_right, Mathf.Infinity, layer))
        {
            if (hit_right.distance > hit.distance)
            {
                hit = hit_right;
            }

            //calculate measurement distance from angle, if distance larger than 1 m, decrease angle recursively
            if (2 * Math.Tan((Math.PI / 180) * new_angle) * hit_right.distance > 1)
            {
                Debug.DrawRay(camera1.transform.position, angle_right * hit_right.distance, Color.blue, debug_draw_time);
                RaycastHit output = CastRay(new_angle, angle_right, hit_right);

                if (output.distance > hit.distance)
                {
                    hit = output;
                }
            }

        }
        //cast down
        if (Physics.Raycast(camera1.transform.position, angle_down, out hit_down, Mathf.Infinity, layer))
        {
            if (hit_down.distance > hit.distance)
            {
                hit = hit_down;
            }

            //calculate measurement distance from angle, if distance larger than 1 m, decrease angle recursively
            if (2 * Math.Tan((Math.PI / 180) * new_angle) * hit_down.distance > 1)
            {
                Debug.DrawRay(camera1.transform.position, angle_down * hit_down.distance, Color.green, debug_draw_time);
                RaycastHit output = CastRay(new_angle, angle_down, hit_down);

                if (output.distance > hit.distance)
                {
                    hit = output;
                }
            }

        }
        //cast left
        if (Physics.Raycast(camera1.transform.position, angle_left, out hit_left, Mathf.Infinity, layer))
        {
            if (hit_left.distance > hit.distance)
            {
                hit = hit_left;
            }

            //calculate measurement distance from angle, if distance larger than 1 m, decrease angle recursively
            if (2 * Math.Tan((Math.PI / 180) * new_angle) * hit_left.distance > 1)
            {
                Debug.DrawRay(camera1.transform.position, angle_left * hit_left.distance, Color.white, debug_draw_time);
                RaycastHit output = CastRay(new_angle, angle_left, hit_left);

                if (output.distance > hit.distance)
                {
                    hit = output;
                }
   
            }

        }

        
        return hit;
    }

}