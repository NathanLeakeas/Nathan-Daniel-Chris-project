using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed=1;
    public float sprintSpeedMult=1.25f;
    public float verticalSensitivity = 2;
    public float horizontalSensitivity = 1;
    private float camRotationX;
    private CharacterController character;
    public Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        character = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        direction = transform.rotation*direction;
        character.Move((direction * Time.deltaTime * speed)-new Vector3(0,9.81f,0)*Time.deltaTime);

        Vector3 horizontalRotation = new Vector3(0, Input.GetAxis("Mouse X") * horizontalSensitivity, 0);
        transform.Rotate(horizontalRotation);


        camRotationX -= Input.GetAxis("Mouse Y") * verticalSensitivity;
        camRotationX = Mathf.Clamp(camRotationX, -89f, 89f);
        Debug.Log(camRotationX);
        cam.transform.eulerAngles = new Vector3(camRotationX, transform.eulerAngles.y, 0.0f);




    }

}
