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
    float camSens = 0.25f; //how sensitive it with mouse
    private Vector3 lastMouse = new Vector3(0, 0, 0);

    public TMP_InputField field1;
    public TMP_InputField field2;
    public TMP_InputField field3;
     
    private bool UI_active = false;

    void Update()
    {
        ///check if ui is active and set boolean
        if(field1.GetComponent<TMP_InputField>().isFocused || field2.GetComponent<TMP_InputField>().isFocused || field3.GetComponent<TMP_InputField>().isFocused)
        {
            UI_active = true;
        }
        else
        {
            UI_active = false;
        }
        ///if ui not active, activate/deactivate free mode by pressing f
        if (UI_active == false && Input.GetKeyDown("f"))
        {
            FreeMode = !FreeMode;
            target.position = transform.position;
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
            if (UI_active == false && Input.GetKeyDown("f"))
            {
                Vector3 new_targetPosition = WASDKeyMovements();
                new_targetPosition = new_targetPosition * Time.deltaTime * 5.0f;
                target.Translate(new_targetPosition);
            }
                
        }
        else
        {
            ///mouse  camera angle 
            lastMouse = Input.mousePosition - lastMouse;
            lastMouse = new Vector3(-lastMouse.y * camSens, lastMouse.x * camSens, 0);
            lastMouse = new Vector3(transform.eulerAngles.x + lastMouse.x, transform.eulerAngles.y + lastMouse.y, 0);
            transform.eulerAngles = lastMouse;
            lastMouse = Input.mousePosition;

            ///keyboard commands
            Vector3 p = WASDKeyMovements();
            p = p * Time.deltaTime * 5.0f;
            Vector3 newPosition = transform.position;
            transform.Translate(p);
            newPosition.x = transform.position.x;
            newPosition.z = transform.position.z;
            transform.position = newPosition;
        }

    }

    ///returns the values of the target position by WASD keys
    private Vector3 WASDKeyMovements()
    { 
        Vector3 p_WASD = new Vector3();
        if (Input.GetKey(KeyCode.W))
        {
            p_WASD += new Vector3(0, 0, 1);
        }
        if (Input.GetKey(KeyCode.S))
        {
            p_WASD += new Vector3(0, 0, -1);
        }
        if (Input.GetKey(KeyCode.A))
        {
            p_WASD += new Vector3(-1, 0, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            p_WASD += new Vector3(1, 0, 0);
        }
        return p_WASD;
    }

}