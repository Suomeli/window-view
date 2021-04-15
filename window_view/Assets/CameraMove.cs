using UnityEngine;

public class CameraMove : MonoBehaviour
{
    
    ///Coordinate mode parameters
    public Transform target;

    public float smoothTime = 0.3f;
    private Vector3 velocity = Vector3.zero;

    public Vector3 offset;
    public float turningRate = 30f;

    /// FreeMode parameters
    public bool FreeMode = false;

    public float speedH = 2.0f;
    public float speedV = 2.0f;

    private float yaw = 0.0f;
    private float pitch = 0.0f;


    void Update()
    {
        ///FreeMode toggle check
        if (Input.GetKeyDown("f"))
        {
            FreeMode = !FreeMode;
        }
        

        if (FreeMode == false)
        {
            
            ///updates each frame to move camera towards desired position until reached
            Vector3 targetPosition = target.TransformPoint(new Vector3(0, 0, 0));
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);

            ///updates rotation until desired rotation reached
            Quaternion target_rotation = Quaternion.Euler(target.transform.localRotation.eulerAngles);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, target_rotation, turningRate * Time.deltaTime);
        }
        else
        {
            ///Freemode camera rotation
            yaw += speedH * Input.GetAxis("Mouse X");
            pitch -= speedV * Input.GetAxis("Mouse Y");

            transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);


        }
    }

}