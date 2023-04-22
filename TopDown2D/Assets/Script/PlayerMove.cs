using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMove : MonoBehaviour
{
    public float Speed;
    float h;
    float v;
    Rigidbody2D rigid;
    bool isHorizonMove;
    Vector3 directionVector;
    GameObject scanObject;


    Animator anim;
    // Start is called before the first frame update
    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim =  GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");


        bool hDown = Input.GetButtonDown("Horizontal");
        bool vDown = Input.GetButtonDown("Vertical");
        bool hUp = Input.GetButtonUp("Horizontal");
        bool vUp = Input.GetButtonUp("Vertical");


  


        if (hDown)
        {
            isHorizonMove = true;
        }else if (vDown )
        {
            isHorizonMove = false;
        }
        else if (hUp || vUp)
        {
            isHorizonMove = h != 0;
        }


        //�ִϸ��̼�
        if (anim.GetInteger("hAxisRaw") != h&& anim.GetInteger("vAxisRaw") == 0)
        {
            anim.SetBool("isIdle", false);
            anim.SetInteger("hAxisRaw", (int)h);

        }else if (anim.GetInteger("vAxisRaw") != v && anim.GetInteger("hAxisRaw") == 0)
        {
            anim.SetBool("isIdle", false);
            anim.SetInteger("vAxisRaw", (int)v);
        }
     
        //�ִϸ��̼� idle ���¸� �ľ� 
        if(anim.GetInteger("hAxisRaw") == 0 && anim.GetInteger("vAxisRaw") == 0)
        {
            anim.SetBool("isIdle", true);
        }
        else
        {
            anim.SetBool("isIdle", false);
        }

        //���� ��ġ ���� 
        if (vDown && v == 1)
        {
     
            directionVector = Vector3.up;
        }
        else if (vDown && v == -1)
        {
       
            directionVector = Vector3.down;
        }
        else if (hDown && h == -1)
        {
         
            directionVector = Vector3.left;
        }
        else if (hDown && h == 1)
        {
         
            directionVector = Vector3.right;
        }

        //������Ʈ ��ĵ
        if (Input.GetButtonDown("Jump")&&scanObject !=  null)
        {
            Debug.Log("�̰Ŵ�" + scanObject.name);

        }
    }
    private void FixedUpdate()
    {


            Vector2 moveVec = isHorizonMove ? new Vector2(h, 0) : new Vector2(0, v);
            rigid.velocity = moveVec * Speed;

        Debug.DrawRay(rigid.position ,directionVector*0.7f ,new Color(0,1,0));
        RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, directionVector, 0.7f, LayerMask.GetMask("Object"));
        if(rayHit.collider != null)
        {
            scanObject = rayHit.collider.gameObject;

        }
        else { scanObject = null; }


    }
}
//Vector2 moveVec = isHorizonMove ? new Vector2(h, 0) : new Vector2(0, v);
//rigid.velocity = moveVec * Speed;