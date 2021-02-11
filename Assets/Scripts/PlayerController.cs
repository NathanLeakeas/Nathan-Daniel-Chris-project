using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed=3f;
    public float sprintSpeedMult=2.25f;
    public float verticalSensitivity = 5f;
    public float horizontalSensitivity = 4f;
    private float camRotationX;
    private CharacterController character;
    public Camera cam;
    private float yVelocity;
    public float jumpVelocity = 4f;
    private float currentSpeedMultiplier;

    // Start is called before the first frame update
    void Start()
    {
        character = GetComponent<CharacterController>();
        yVelocity = 0;
        currentSpeedMultiplier = speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (character.isGrounded)
        {
            yVelocity = 0;
        }
        
        if (Input.GetButtonDown("Jump"))
        {
            if (character.isGrounded)
            {
                yVelocity = jumpVelocity;
            }
        }
        if (Input.GetButton("Sprint"))
        {
            currentSpeedMultiplier = sprintSpeedMult * speed;
            Debug.Log("Shift Key Down");
        }
        else
        {
            currentSpeedMultiplier = speed;
        }

        Vector3 direction = new Vector3(Input.GetAxis("Horizontal")*currentSpeedMultiplier, yVelocity, Input.GetAxis("Vertical")*currentSpeedMultiplier);
        direction = transform.rotation*direction;
        character.Move((direction * Time.deltaTime));

        Vector3 horizontalRotation = new Vector3(0, Input.GetAxis("Mouse X") * horizontalSensitivity, 0);
        transform.Rotate(horizontalRotation);


        camRotationX -= Input.GetAxis("Mouse Y") * verticalSensitivity;
        camRotationX = Mathf.Clamp(camRotationX, -89f, 89f);
        cam.transform.eulerAngles = new Vector3(camRotationX, transform.eulerAngles.y, 0.0f);


        yVelocity -= 9.8f*Time.deltaTime;

    }

}
