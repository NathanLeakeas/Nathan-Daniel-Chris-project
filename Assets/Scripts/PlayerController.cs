using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed=1;
    public float sprintSpeedMult=1.25f;
    public float verticalSensitivity = 1;
    public float horizontalSensitivity = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        transform.Translate(direction * Time.deltaTime * speed);

        Vector3 rotation = new Vector3(0, Input.GetAxis("Mouse X") * horizontalSensitivity, 0);
        transform.Rotate(rotation);

        //jump code here


    }
}
