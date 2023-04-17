using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallControl : MonoBehaviour
{

    Rigidbody rigid;
    
    // Start is called before the first frame update
    void Start()
    {

       rigid = GetComponent<Rigidbody>();
      //  rigid.AddForce(Vector3.up * 100, ForceMode.Impulse);

      }

      // Update is called once per frame
      void Update()
      {

      }

      void FixedUpdate()
      {


        /* rigid = GetComponent<Rigidbody>();
         rigid.velocity = new Vector3(2, 4, 3);
        */
        // 힘가하기 
        
        if (Input.GetButtonDown("Jump"))
        {
            rigid.AddForce(Vector3.up * 10, ForceMode.Impulse);
            Debug.Log(rigid.velocity);
        }
        Vector3 vec = new Vector3(Input.GetAxisRaw("Horizontal"),0, Input.GetAxisRaw("Vertical"));
        rigid.AddForce(vec, ForceMode.Impulse);
        


        // 회전력
       // rigid.AddTorque(Vector3.up);

    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "SafeZone")
        {
            rigid.AddForce(Vector3.up * 2, ForceMode.Impulse);
        }
    }
}
