using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    Rigidbody2D rigid;
    public int nextMove;
    float nextMoveTime;
    BoxCollider2D boxcolider;
    SpriteRenderer spriteRenderer;
    Animator anim;
    

    // Start is called before the first frame update
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxcolider = GetComponent<BoxCollider2D>();
       
        ChooseNextMove();
    }


    // Update is called once per frame
    void FixedUpdate()
    {

        //���� �⺻�̵�
        rigid.velocity = new Vector2(nextMove, rigid.velocity.y);

        //�ٴ� Ȯ��
        Vector2 frontVec = new Vector2(rigid.position.x + nextMove*0.2f, rigid.position.y);
        Debug.DrawRay(frontVec, Vector3.down, new Color(0, 1, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, 1, LayerMask.GetMask("PlatForm"));
        if (rayHit.collider == null)
        {
            Turn();
        }
    }
    
    void ChooseNextMove()
    {
        //���� ������ ����
        nextMove = Random.Range(-1,2);
        nextMoveTime = Random.Range(2f,5f);
      
        //������ȯ �ִϸ��̼�
        anim.SetInteger("WalkSpeed",nextMove);
        //������ȯ (����)
        if (nextMove != 0)
        {
            spriteRenderer.flipX = (nextMove == 1);
        }

        Invoke("ChooseNextMove", nextMoveTime);
    }
    
    void Turn()
    {
    
        nextMove *= -1;
        CancelInvoke("ChooseNextMove");
        Invoke("ChooseNextMove", nextMoveTime);
        if (nextMove != 0)
        {
            spriteRenderer.flipX = (nextMove == 1);
        }


    }

    public void OnDamaged()
    {
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);
        spriteRenderer.flipY = true;
        boxcolider.enabled = false;
        rigid.AddForce(Vector2.up * 5, ForceMode2D.Impulse);
        Invoke("DeAttack", 3);

    }
    void DeAttack()
    {
        gameObject.SetActive(false);
    }
    
}
