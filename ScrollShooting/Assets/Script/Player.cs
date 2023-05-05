using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public float maxShotDelay;
    public float curShotDelay;

    public int power;
    public int maxPower;
    public int maxBoom;
    public int life;
    public int score;
    public int boom;

    public bool isTouchTop;
    public bool isTouchBottom;
    public bool isTouchLeft;
    public bool isTouchRight;
    Animator animator;

    public GameManager gameManager;
    public ObjectManager objectManager;
    public bool isHit;
    public bool isBoomTimes;
    // Update is called once per frame

    public GameObject bulletA;
    public GameObject bulletB;
    public GameObject boomEffect;
    private void Awake()
    {
            animator = GetComponent<Animator>();
    }
    void Update()
    {
        Move();
        Fire();
        Boom();
        Reload();
    }
    
    void Reload()
    {
        curShotDelay += Time.deltaTime;

    }
    void Boom()
    {
        if (!Input.GetButton("Jump"))
            return;
        if (isBoomTimes)
            return;
        if (boom == 0)
            return;

        boom--;
        //이펙트 보이기
        boomEffect.SetActive(true);
        gameManager.UpdateBoomIcon(boom);
        Invoke("OffBoomEffect", 4f);
        //적 지우기
        GameObject[] enemiesL = objectManager.GetPool("enemyL");
        GameObject[] enemiesM = objectManager.GetPool("enemyM");
        GameObject[] enemiesS = objectManager.GetPool("enemyS");
        for (int index = 0; index < enemiesL.Length; index++)
        {
            Enemy enemyLogic = enemiesL[index].GetComponent<Enemy>();
            enemyLogic.OnHit(1000);
        }
        for (int index = 0; index < enemiesM.Length; index++)
        {
            Enemy enemyLogic = enemiesM[index].GetComponent<Enemy>();
            enemyLogic.OnHit(1000);
        }
        for (int index = 0; index < enemiesS.Length; index++)
        {
            Enemy enemyLogic = enemiesS[index].GetComponent<Enemy>();
            enemyLogic.OnHit(1000);
        }
        //
        GameObject[] bulletsA = objectManager.GetPool("bulletEnemyA");
        GameObject[] bulletsB = objectManager.GetPool("bulletEnemyB");
        for (int index = 0; index < bulletsA.Length; index++)
        {
            bulletsA[index].SetActive(false);
        }
        for (int index = 0; index < bulletsB.Length; index++)
        {
            bulletsB[index].SetActive(false);
        }
        isBoomTimes = true;

    }
    void Fire()
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
                GameObject bullet = objectManager.MakeObj("bulletPlayerA"); 
                bullet.transform.position= transform.position;

                Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
                rigid.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                curShotDelay = 0;
                break;
            
            case 2:
                GameObject bulletR = objectManager.MakeObj("bulletPlayerA");
                bulletR.transform.position = transform.position + Vector3.right * 0.1f;

                GameObject bulletL = objectManager.MakeObj("bulletPlayerA"); ;
                bulletL.transform.position = transform.position + Vector3.left * 0.1f;

                Rigidbody2D rigidR = bulletR.GetComponent<Rigidbody2D>();
                Rigidbody2D rigidL = bulletL.GetComponent<Rigidbody2D>();

                rigidR.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                rigidL.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                curShotDelay = 0;
                break;
            case 3:
                GameObject bulletRR = objectManager.MakeObj("bulletPlayerA");
                bulletRR.transform.position = transform.position + Vector3.right * 0.25f;
            
                GameObject bulletCC = objectManager.MakeObj("bulletPlayerB");
                bulletCC.transform.position = transform.position;

                GameObject bulletLL = objectManager.MakeObj("bulletPlayerA");
                bulletLL.transform.position = transform.position + Vector3.left * 0.25f;

                Rigidbody2D rigidRR = bulletRR.GetComponent<Rigidbody2D>();
                Rigidbody2D rigidCC = bulletCC.GetComponent<Rigidbody2D>();
                Rigidbody2D rigidLL = bulletLL.GetComponent<Rigidbody2D>();

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
            gameManager.UpdateLifeIcon(life);
            gameObject.SetActive(false);
            if (life == 0)
            {

                gameManager.GameOver();

            }
            else if (life >0)
            {

                gameManager.RespawnPlayer();   
            }


        }else if (collision.gameObject.tag == "Item"){
            Item item = collision.gameObject.GetComponent<Item>();
            switch (item.type)
            {
                case "Coin":
                    score += 1000;
                    break;

                case "Power":
                    if(power == maxPower)
                    {
                        score += 500;
                    }else
                    power++;
                    break;
                case "Boom":
                    if (boom == maxBoom)
                    {
                        score += 500;
                    }
                    else
                    {
                        boom++;
                        gameManager.UpdateBoomIcon(boom);
                    }
                      
                    break;
            }
            collision.gameObject.SetActive(false);
        }
    }
    void OffBoomEffect()
    {
        boomEffect.SetActive(false);
        isBoomTimes =false;
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
