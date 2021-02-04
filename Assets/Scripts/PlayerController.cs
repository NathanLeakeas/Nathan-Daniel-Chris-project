using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed=1;
    public float sprintSpeedMult=1.25f;
    public float verticalSensitivity = 1;
    public float horizontalSensitivity = 1;
    private float camRotationX;
    private CharacterController character;
    public Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        character = GetComponent<CharacterController>();
        //camRotationX = cam.transform.eulerAngles.x;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        direction = transform.rotation*direction;
        character.Move((direction * Time.deltaTime * speed)-new Vector3(0,9.81f,0)*Time.deltaTime);

        Vector3 horizontalRotation = new Vector3(0, Input.GetAxis("Mouse X") * horizontalSensitivity, 0);
        transform.Rotate(horizontalRotation);

        //Cam limit attempt: Restrict via manual angle input/clamping
        /*float verticalRotation = -Input.GetAxis("Mouse Y")*verticalSensitivity;
        camRotationX = Mathf.Clamp(camRotationX,90f,-90f);
        cam.transform.rotation = Quaternion.Euler(camRotationX,cam.transform.rotation.eulerAngles.y, cam.transform.rotation.eulerAngles.z);*/

        //Cam Limit attempt: Restrict via if statement
        Vector3 verticalRotation = new Vector3(-Input.GetAxis("Mouse Y")*verticalSensitivity, 0, 0);
        //Debug.Log("Camera X Rotation: " + cam.transform.localEulerAngles.x);
        /*if ((cam.transform.localEulerAngles.x >=0 && cam.transform.localEulerAngles.x<=90)|| (cam.transform.localEulerAngles.x >= 270 && cam.transform.localEulerAngles.x <= 360))
        {
            Debug.Log("Should stop rotation");*/
            cam.transform.Rotate(verticalRotation);
        //}
        

        //jump code here


    }
}
