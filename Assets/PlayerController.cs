using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public Vector2 sensitivity = new Vector2(5, 3);
    public float maxYAngle = 80f;
    private Vector2 currentRotation = new Vector2();

    // Use this for initialization
    void Start () {
        Cursor.lockState = CursorLockMode.Locked;
    }
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey(KeyCode.W))
        {
            this.GetComponent<Rigidbody>().MovePosition(this.transform.forward + this.transform.position);

            //this.gameObject.GetComponent<CharacterController>().Move(this.transform.forward);
        }

        if(Input.GetKey(KeyCode.A))
        {
            this.gameObject.transform.Rotate(Vector3.up, -1);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            this.gameObject.transform.Rotate(Vector3.up, 1);
        }


        currentRotation.x += Input.GetAxis("Mouse X") * sensitivity.x;
        currentRotation.y -= Input.GetAxis("Mouse Y") * sensitivity.y;
        currentRotation.x = Mathf.Repeat(currentRotation.x, 360);
        currentRotation.y = Mathf.Clamp(currentRotation.y, -maxYAngle, maxYAngle);
        this.transform.localRotation = Quaternion.Euler(0, currentRotation.x, 0);
        Camera.main.transform.localRotation = Quaternion.Euler(currentRotation.y, 0, 0);
    }
}
