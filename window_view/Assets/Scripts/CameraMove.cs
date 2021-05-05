using UnityEngine;
using TMPro;

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

    private float yaw = -0.0f;
    private float pitch = 0.0f;
    public Vector3 rotation;


    //variables for checkin if UI is active
    public TMP_InputField field1;
    public TMP_InputField field2;
    public TMP_InputField field3;

    private bool UI_active = false;
    


    void Update()
    {
        //check if any inputfield is active
        //this is quite a hackey way to do it, maybe better ways exist
        //change UI_active variable based on results
        if (field1.GetComponent<TMP_InputField>().isFocused || field2.GetComponent<TMP_InputField>().isFocused || field3.GetComponent<TMP_InputField>().isFocused)
        {
            UI_active = true;
        }
        else
        {
            UI_active = false;
        }


        ///FreeMode toggle check
        if (UI_active == false && Input.GetKeyDown("f"))
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
            //get default view direction for freecam from target
            yaw = target.transform.localRotation.eulerAngles.y;

        }
        else if(UI_active == false)
        {
            ///Freemode camera rotation
            yaw += speedH * Input.GetAxis("Mouse X");
            pitch -= speedV * Input.GetAxis("Mouse Y");

            transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);


        }
    }


    
    
}
    