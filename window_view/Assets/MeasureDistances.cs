using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;





public class MeasureDistances : MonoBehaviour
{
    public Camera camera1;
    public LayerMask layer;

    public int draw_time = 5;
    public GameObject marker;

    float FOV_horizontal = 70f;
    float FOV_vertical = 41.02922f;


    public void AnalyzeView()
    {
        RaycastHit hit1;
        RaycastHit furthest;

        int horizontal_angle = (int) (FOV_horizontal / 2 * -1);
        int vertical_angle = (int) (FOV_vertical / 2 * -1);
        float angle_change = 1;
        float furthest_distance = 0;


        //forming the basic grid of raycasts
        for (int y = vertical_angle; y <= vertical_angle * -1; y++)
        {
            for (int x = horizontal_angle; x <= horizontal_angle * -1; x++)
            {
                //angle based on grid direction
                Vector3 direction = Quaternion.Euler(0, x, y) * camera1.transform.forward;

                //cast a ray, if hit measure distance and see if grid is dense enough
                if (Physics.Raycast(camera1.transform.position, direction, out hit1, Mathf.Infinity, layer))
                {
                    

                    //calculate measurement distance from angle, if distance larger than 1 m, decrease angle recursively
                    if (2 * Math.Tan((Math.PI / 180) * angle_change) * hit1.distance > 1)
                    {
                        Debug.DrawRay(camera1.transform.position, direction * hit1.distance, Color.red, draw_time);
                        RaycastHit output = CastRay(angle_change, direction, hit1);

                        if (output.distance > furthest_distance)
                        {
                            furthest = output;
                            furthest_distance = furthest.distance;
                            Debug.Log(furthest.distance);
                            marker.transform.position = furthest.point;
                        }
                        
                    }
                    else if (hit1.distance > furthest_distance)
                    {
                        furthest = hit1;
                        furthest_distance = furthest.distance;
                        Debug.Log(furthest.distance);
                        marker.transform.position = furthest.point;
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
            Debug.DrawRay(camera1.transform.position, angle_up * hit_up.distance, Color.yellow, draw_time);
            if (hit_up.distance > hit.distance)
            {
                hit = hit_up;
            }
            //calculate measurement distance from angle, if distance larger than 1 m, decrease angle recursively
            if (2 * Math.Tan((Math.PI / 180) * new_angle) * hit_up.distance > 1)
            {
                RaycastHit output = CastRay(new_angle, angle_up, hit_up);
                hit = output;
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
                Debug.DrawRay(camera1.transform.position, angle_right * hit_right.distance, Color.blue, draw_time);
                RaycastHit output = CastRay(new_angle, angle_right, hit_right);
                hit = output;
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
                Debug.DrawRay(camera1.transform.position, angle_down * hit_down.distance, Color.green, draw_time);
                RaycastHit output = CastRay(new_angle, angle_down, hit_down);
                hit = output;
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
                Debug.DrawRay(camera1.transform.position, angle_left * hit_left.distance, Color.white, draw_time);
                RaycastHit output = CastRay(new_angle, angle_left, hit_left);
                hit = output;
            }

        }


        return hit;
    }
}
