using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using UnityEngine;

public class Follower : MonoBehaviour
{
    public float maxShotDelay;
    public float curShotDelay;

    public ObjectManager objectManager;

    public Vector3 followPos;
    public int followDelay;
    public Transform parent;
    public Queue<Vector3> parentPos;


    private void Awake()
    {
        parentPos = new Queue<Vector3>();
    }

    void Update()
    {
        Watch();
        Follow();
        Fire();
        Reload();
    }
   
    void Watch()
    {
        if (!parentPos.Contains(parent.position))
        parentPos.Enqueue(parent.position);

        if(parentPos.Count > followDelay)
        {
            followPos = parentPos.Dequeue();
        }else if(parentPos.Count < followDelay)
        {
            followPos = parent.position;
        }
       
    }
    void Reload()
    {
        curShotDelay += Time.deltaTime;

    }
    void Fire()
    {

        //버튼 안눌렸을때 
        if (!Input.GetButton("Fire1"))
            return;
        //장전이 되지 않았을떄 
        if (curShotDelay < maxShotDelay)
            return;

        GameObject bullet = objectManager.MakeObj("bulletFollower");
        bullet.transform.position = transform.position;

        Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
        rigid.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
        curShotDelay = 0;


    }
    void Follow()
    {
     transform.position = followPos;

    }
    

}
