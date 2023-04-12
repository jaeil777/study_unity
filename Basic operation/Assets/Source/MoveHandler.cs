using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
            Debug.Log("키 눌려졋어요");
        if (Input.anyKey)
            Debug.Log("키 누르고있어요");
    }
}
