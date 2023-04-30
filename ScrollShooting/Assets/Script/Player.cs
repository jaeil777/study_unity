using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public float power;
    public float maxShotDelay;
    public float curShotDelay;

    public int life;
    public int score;

    public bool isTouchTop;
    public bool isTouchBottom;
    public bool isTouchLeft;
    public bool isTouchRight;
    Animator animator;

    public GameManager manager;
    public bool isHit;
    // Update is called once per frame

    public GameObject bulletA;
    public GameObject bulletB;
    private void Awake()
    {
            animator = GetComponent<Animator>();
    }
    void Update()
    {
        Move();
        Fire();
        Reload();
    }

    private void Reload()
    {
        curShotDelay += Time.deltaTime;

    }

    private void Fire()
    {
    
        //버튼 안눌렸을때 
        if (!Input.GetButton("Fire1"))
            return;
        //장전이 되지 않았을떄 
        if (curShotDelay < maxShotDelay)
            return;

        //여러발
        switch (power)
        {
            case 1:
                GameObject Bullet = Instantiate(bulletA, transform.position, transform.rotation);
                Rigidbody2D rigid = Bullet.GetComponent<Rigidbody2D>();
                rigid.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                curShotDelay = 0;
                break;
            
            case 2:
                GameObject BulletR = Instantiate(bulletA, transform.position+Vector3.right*0.1f, transform.rotation);
                GameObject BulletL = Instantiate(bulletA, transform.position + Vector3.left * 0.1f, transform.rotation);

                Rigidbody2D rigidR = BulletR.GetComponent<Rigidbody2D>();
                Rigidbody2D rigidL = BulletL.GetComponent<Rigidbody2D>();

                rigidR.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                rigidL.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                curShotDelay = 0;
                break;
            case 3:
                GameObject BulletRR = Instantiate(bulletA, transform.position + Vector3.right * 0.25f, transform.rotation);
                GameObject BulletCC = Instantiate(bulletB, transform.position, transform.rotation);
                GameObject BulletLL = Instantiate(bulletA, transform.position + Vector3.left * 0.25f, transform.rotation);

                Rigidbody2D rigidRR = BulletRR.GetComponent<Rigidbody2D>();
                Rigidbody2D rigidCC = BulletCC.GetComponent<Rigidbody2D>();
                Rigidbody2D rigidLL = BulletLL.GetComponent<Rigidbody2D>();

                rigidRR.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                rigidCC.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                rigidLL.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                curShotDelay = 0;
                break;
        }
        
        //한발
/*
            GameObject Bullet = Instantiate(bulletA, transform.position, transform.rotation);
            Rigidbody2D rigid = Bullet.GetComponent<Rigidbody2D>();
            rigid.AddForce(Vector2.up * 10, ForceMode2D.Impulse);

    */
    }

    void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");
        if ((isTouchRight && h == 1) || (isTouchLeft && h == -1))
        {
            h = 0;
        }
        float v = Input.GetAxisRaw("Vertical");
        if ((isTouchTop && v == 1) || (isTouchBottom && v == -1))
        {
            v = 0;
        }
        Vector3 curPos = transform.position;
        Vector3 nextPos = new Vector3(h, v, 0) * speed * Time.deltaTime;
        transform.position = curPos + nextPos;

        if (Input.GetButtonDown("Horizontal") || Input.GetButtonUp("Horizontal"))
        {
            
            animator.SetInteger("Input", (int)h);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag =="Border") {
        switch(collision.gameObject.name) {
                case "Top":
                    isTouchTop = true;
                    break;
                case "Bottom":
                    isTouchBottom = true;
                    break;
                case "Right":
                    isTouchRight = true; 
                    break;
                case "Left":
                    isTouchLeft = true; 
                    break;
            }
        }else if(collision.gameObject.tag =="Enemy" || collision.gameObject.tag =="EnemyBullet"){

            if (isHit)
                return;
            isHit = true;
            life--;
            manager.UpdateLifeIcon(life);
            gameObject.SetActive(false);
            if (life == 0)
            {
              
                manager.GameOver();

            }
            else if (life >0)
            {
             
                manager.RespawnPlayer();   
            }


        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Border")
        {
            switch (collision.gameObject.name)
            {
                case "TOP":
                    isTouchTop = false;
                    break;
                case "Bottom":
                    isTouchBottom = false;
                    break;
                case "Right":
                    isTouchRight = false;
                    break;
                case "Left":
                    isTouchLeft = false;
                    break;
            }
        }
    }

}
