using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public bool FreeMode = false;
    public Transform target;

    public float smoothTime = 0.3f;
    private Vector3 velocity = Vector3.zero;
    public Vector3 offset;
    public float turningRate = 30f;


    void Update()
    {

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

            Debug.Log("FreeMode");

        }
    }

}