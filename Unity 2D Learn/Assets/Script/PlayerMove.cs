using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMove : MonoBehaviour
{
    public GameManager gameManager;
    public float maxSpeed;
    public float jumpPower;
   
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    CapsuleCollider2D capsuleCollider;
    Animator anim;


    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        anim = GetComponent<Animator>();    
    }

    private void Update()
    {
        //����
        if (Input.GetButtonUp("Jump")&& !anim.GetBool("isJumping"))
        {
            rigid.AddForce(Vector2.up*jumpPower, ForceMode2D.Impulse);
            anim.SetBool("isJumping", true);
            
        }
       

        //���⋚ �ӵ�
        if (Input.GetButtonUp("Horizontal"))
        {
           
            rigid.velocity = new Vector2(rigid.velocity.normalized.x*0.5f, rigid.velocity.y);
        }

        //������ȯ
        if (Input.GetButton("Horizontal"))
        {
            spriteRenderer.flipX = (Input.GetAxisRaw("Horizontal") == -1);

        }

        //�ִϸ��̼� ��ȯ

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
        rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse); //�¿������ ����ؼ� �̵�
       
        if (rigid.velocity.x > maxSpeed)
        {
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y); //  ������ �ִ� �ӵ�
        }else if (rigid.velocity.x < maxSpeed * (-1))
        {
            rigid.velocity = new Vector2(maxSpeed * (-1), rigid.velocity.y); //���� �ִ�ӵ�
        }

        if(rigid.velocity.y < 0)
        {
            Debug.DrawRay(rigid.position, Vector3.down, new Color(0, 1, 0));

            RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, 1, LayerMask.GetMask("PlatForm"));

            if (rayHit.collider != null)
            {
                if (rayHit.distance < 0.7f)
                {
                   // Debug.Log(rayHit.collider.name);
                    anim.SetBool("isJumping", false);
        
                }

            }
        }
        
        
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag =="Trap" || collision.gameObject.tag == "Monster")
        {
            if(rigid.velocity.y<0 && transform.position.y> collision.transform.position.y+0.3 && collision.gameObject.tag == "Monster")
            {
                Onattack(collision.gameObject);
            }
            else
            {
                OnDamaged(collision.transform.position);

            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Coin")
        {
            bool isBronze = collision.gameObject.name.Contains("Bronze");
            bool isSilver = collision.gameObject.name.Contains("Silver");
            bool isGold = collision.gameObject.name.Contains("Gold");
            Debug.Log(isBronze.ToString() + isSilver.ToString()+ isGold.ToString());
            if (isBronze)
                gameManager.stagescore += 50;
            else if (isSilver)
                gameManager.stagescore += 100;
            else if (isGold)
                gameManager.stagescore += 300;
            collision.gameObject.SetActive(false);



        }
        else if (collision.tag == "Finish")
        {
            gameManager.NextStage();
        }
    }

    void OnDamaged(Vector2 targetPos)
    {
        gameObject.layer = 10;
        gameManager.HealthDown();
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);
        int dirc = transform.position.x - targetPos.x > 0 ? 1 : -1;
        rigid.AddForce(new Vector2(dirc, 1)*15, ForceMode2D.Impulse);

        Invoke("OffDamaged", 1);

        //�ǰ� �ִϸ��̼�
        anim.SetTrigger("Damaged");
    }
    void OffDamaged()
    {
        gameObject.layer = 7;
        spriteRenderer.color = new Color(1, 1, 1, 1);

    }
    void Onattack(GameObject monster)
    {

        //�� ĳ���Ϳ� ������ �ݹ߷�
        rigid.AddForce(Vector2.up*5,ForceMode2D.Impulse);

        //�������� ���� �ൿ 
        EnemyMove enemyMove =  monster.GetComponent<EnemyMove>();
        enemyMove.OnDamaged();
    }
    public void OnDie()
    {
        //���ļ���
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);
        //���ٷ� ��������
        spriteRenderer.flipY = true;
        //�ݶ��̴� ���� 
        capsuleCollider.enabled = false;
        //���� ����Ʈ ����
        rigid.AddForce(Vector2.up * 5, ForceMode2D.Impulse);
        
        
    }
    public void OnRvive()
    {
        //���ļ���
        spriteRenderer.color = new Color(1, 1, 1);
        //�ٽõ���
        spriteRenderer.flipY = false;
        //�ݶ��̴���
        capsuleCollider.enabled = true;
      


    }

    public void VelocityZero()
    {
        rigid.velocity = Vector2.zero;
    }

}
