using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PysicsEventHandler : MonoBehaviour
{

    MeshRenderer mesh;
    Material mat;
    // Start is called before the first frame update
    void Start()
    {
        mesh = GetComponent<MeshRenderer>();
        mat = mesh.material;

    }


    private void OnCollisionEnter(Collision collision) // 충돌 했을때
    {
        if(collision.gameObject.name == "MyBall")
        mat.color = new Color(1,180,1);


    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.name == "MyBall")
            mat.color = new Color(120,0 , 120);

    }
    private void OnCollisionStay(Collision collision)
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
