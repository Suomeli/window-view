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

            ///updates the position of the target in X and Z axis by arrow keys
            Vector3 new_targetPosition = ArrowKeyMovement();
            new_targetPosition = new_targetPosition * Time.deltaTime * 5.0f;
            target.Translate(new_targetPosition);
        }
        else
        {
            ///Freemode camera rotation
            yaw += speedH * Input.GetAxis("Mouse X");
            pitch -= speedV * Input.GetAxis("Mouse Y");

            transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
        }

    }

    ///returns the values of the target position made by keys
    private Vector3 ArrowKeyMovement()
    {
        Vector3 positionArrows = new Vector3();
        if (Input.GetKey(KeyCode.UpArrow))
        {
            positionArrows += new Vector3(0, 0, 1);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            positionArrows += new Vector3(0, 0, -1);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            positionArrows += new Vector3(-1, 0, 0);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            positionArrows += new Vector3(1, 0, 0);
        }

        return positionArrows;
    }

}
