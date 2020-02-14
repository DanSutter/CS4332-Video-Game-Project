using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Keyboard Variables
    public float speed = 5.0f;
    public float gravity = 9.8f;

    private CharacterController C_Controller;
    private Vector3 moveDir = Vector3.zero;


    //Mouse variables
    public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
    public RotationAxes axes = RotationAxes.MouseXAndY;
    public float sensitivityX = 10f;
    public float sensitivityY = 10f;

    public float minX = -360f;
    public float maxX = 360f;

    public float minY = -60f;
    public float maxY = 60f;

    float rotX = 0f;
    float rotY = 0f;

    private List<float> rotArrayX = new List<float>();
    float rotAvgX = 0f;

    private List<float> rotArrayY = new List<float>();
    float rotAvgY = 0f;

    public float frameCount = 20;

    Quaternion originalRotation;

    public GameObject pauseMenu;

    void Start()
    {
        //Keyboard
        C_Controller = GetComponent<CharacterController>();

        //Mouse
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb)
            rb.freezeRotation = true;
        originalRotation = transform.localRotation;
    }
    void Update()
    {
        //Keyboard Movement
        if (C_Controller.isGrounded)
        {
            moveDir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDir = transform.TransformDirection(moveDir);
            moveDir *= speed;
        }

        moveDir.y -= gravity * Time.deltaTime;
        C_Controller.Move(moveDir * Time.deltaTime);

        //If to stop camera movement
        if (!Input.GetKey(KeyCode.LeftControl))
        {
            //Mouse Movement 
            if (axes == RotationAxes.MouseXAndY)
            {
                rotAvgY = 0f;
                rotAvgX = 0f;

                rotY += Input.GetAxis("Mouse Y") * sensitivityY;
                rotX += Input.GetAxis("Mouse X") * sensitivityX;

                rotArrayY.Add(rotY);
                rotArrayX.Add(rotX);

                if (rotArrayY.Count >= frameCount)
                {
                    rotArrayY.RemoveAt(0);
                }
                if (rotArrayX.Count >= frameCount)
                {
                    rotArrayX.RemoveAt(0);
                }

                for (int j = 0; j < rotArrayY.Count; j++)
                {
                    rotAvgY += rotArrayY[j];
                }
                for (int i = 0; i < rotArrayX.Count; i++)
                {
                    rotAvgX += rotArrayX[i];
                }

                rotAvgY /= rotArrayY.Count;
                rotAvgX /= rotArrayX.Count;

                rotAvgY = ClampAngle(rotAvgY, minY, maxY);
                rotAvgX = ClampAngle(rotAvgX, minX, maxX);

                Quaternion yQuaternion = Quaternion.AngleAxis(rotAvgY, Vector3.left);
                Quaternion xQuaternion = Quaternion.AngleAxis(rotAvgX, Vector3.up);

                transform.localRotation = originalRotation * xQuaternion * yQuaternion;
            }
            else if (axes == RotationAxes.MouseX)
            {
                rotAvgX = 0f;

                rotX += Input.GetAxis("Mouse X") * sensitivityX;

                rotArrayX.Add(rotX);

                if (rotArrayX.Count >= frameCount)
                {
                    rotArrayX.RemoveAt(0);
                }
                for (int i = 0; i < rotArrayX.Count; i++)
                {
                    rotAvgX += rotArrayX[i];
                }
                rotAvgX /= rotArrayX.Count;

                rotAvgX = ClampAngle(rotAvgX, minX, maxX);

                Quaternion xQuaternion = Quaternion.AngleAxis(rotAvgX, Vector3.up);
                transform.localRotation = originalRotation * xQuaternion;
            }
            else
            {
                rotAvgY = 0f;

                rotY += Input.GetAxis("Mouse Y") * sensitivityY;

                rotArrayY.Add(rotY);

                if (rotArrayY.Count >= frameCount)
                {
                    rotArrayY.RemoveAt(0);
                }
                for (int j = 0; j < rotArrayY.Count; j++)
                {
                    rotAvgY += rotArrayY[j];
                }
                rotAvgY /= rotArrayY.Count;

                rotAvgY = ClampAngle(rotAvgY, minY, maxY);

                Quaternion yQuaternion = Quaternion.AngleAxis(rotAvgY, Vector3.left);
                transform.localRotation = originalRotation * yQuaternion;
            }
        }

        // open pause menu
        if (Input.GetKey("escape"))
        {

            pauseMenu.SetActive(true);
        }
    }

    

    public static float ClampAngle(float angle, float min, float max)
    {
        angle = angle % 360;
        if ((angle >= -360f) && (angle <= 360f))
        {
            if (angle < -360f)
            {
                angle += 360f;
            }
            if (angle > 360f)
            {
                angle -= 360f;
            }
        }
        return Mathf.Clamp(angle, min, max);
    }
}
