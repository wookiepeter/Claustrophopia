using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    [SerializeField]
    float movementSpeed;
    [SerializeField]
    BoxCollider collider;
    bool hin = true;
    bool her = false;
    // Use this for initialization
    void Start () {
        transform.position = new Vector3(1, 1, 1);

	}
	
	// Update is called once per frame
	void Update () {
        float xmovement = Input.GetAxis("Horizontal");
        float ymovement = Input.GetAxis("Vertical");
        // Input.mousePosition 
        // Mathf.
        if (hin && (Input.GetKey("n") || transform.eulerAngles.x>=60))
        {
            hin = false;
            her = true;
        }
        if (her && (Input.GetKey("b") || transform.eulerAngles.x <= -60))
        {
            hin = true;
            her = false;
        }
        if (hin==true) {
            //transform.position += new Vector3(0, 2 * movementSpeed * Time.deltaTime, 0);
            transform.Rotate(Vector3.right, 30 * movementSpeed * Time.deltaTime);
        }
        
        if (her == true) {
            //transform.position += new Vector3(0, -2 * movementSpeed * Time.deltaTime, 0);
            transform.Rotate(Vector3.right, -30 * movementSpeed * Time.deltaTime);
        }
            //transform.position += new Vector3(xmovement * movementSpeed * Time.deltaTime, ymovement * movementSpeed * Time.deltaTime, 0);
            //collider.size += new Vector3(xmovement * movementSpeed * Time.deltaTime, ymovement * movementSpeed * Time.deltaTime, 0);

        }
}
