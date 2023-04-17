using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlMove : MonoBehaviour
{
    private float xRotate, yRotate, xRotateMove, yRotateMove;
    public float rotateSpeed = 500.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += new Vector3(0.0f, 0.0f, 0.1f);
        }
        // s->뒤
        if (Input.GetKey(KeyCode.S))
        {
            transform.position -= new Vector3(0.0f, 0.0f, 0.1f);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position -= new Vector3(0.1f, 0.0f, 0.0f);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += new Vector3(0.1f, 0.0f, 0.0f);
        }

        if (Input.GetMouseButton(0)) // 클릭한 경우
        {
            xRotateMove = -Input.GetAxis("Mouse Y") * Time.deltaTime * rotateSpeed;
            yRotateMove = Input.GetAxis("Mouse X") * Time.deltaTime * rotateSpeed;

            yRotate = transform.eulerAngles.y + yRotateMove;
            //xRotate = transform.eulerAngles.x + xRotateMove; 
            xRotate = xRotate + xRotateMove;

            xRotate = Mathf.Clamp(xRotate, -90, 90); // 위, 아래 고정

            transform.eulerAngles = new Vector3(xRotate, yRotate, 0);
        }
    }
}
