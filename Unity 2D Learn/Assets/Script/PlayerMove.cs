using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    public float maxSpeed;
    public float jumpPower;
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator anim;


    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();    
    }

    private void Update()
    {
        //점프
        if (Input.GetButtonUp("Jump")&& !anim.GetBool("isJumping"))
        {
            rigid.AddForce(Vector2.up*jumpPower, ForceMode2D.Impulse);
            anim.SetBool("isJumping", true);
            
        }
       

        //멈출떄 속도
        if (Input.GetButtonUp("Horizontal"))
        {
           
            rigid.velocity = new Vector2(rigid.velocity.normalized.x*0.5f, rigid.velocity.y);
        }

        //방향전환
        spriteRenderer.flipX = (Input.GetAxisRaw("Horizontal")== -1);

        //애니메이션 전환

        if (Mathf.Abs( rigid.velocity.x) < 0.3)
        {
            anim.SetBool("isWalking", false);
        }
        else 
        {
            anim.SetBool("isWalking", true);
        }

  

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse); //좌우방향을 사용해서 이동
       
        if (rigid.velocity.x > maxSpeed)
        {
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y); //  오른쪽 최대 속도
        }else if (rigid.velocity.x < maxSpeed * (-1))
        {
            rigid.velocity = new Vector2(maxSpeed * (-1), rigid.velocity.y); //왼쪽 최대속도
        }

        if(rigid.velocity.y < 0)
        {
            Debug.DrawRay(rigid.position, Vector3.down, new Color(0, 1, 0));

            RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, 1, LayerMask.GetMask("PlatForm"));

            if (rayHit.collider != null)
            {
                if (rayHit.distance < 0.7f)
                {
                    Debug.Log(rayHit.collider.name);
                    anim.SetBool("isJumping", false);
        
                }

            }
        }
        
        
    }
}
