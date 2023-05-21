using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
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
    SpriteRenderer spriteRenderer;

    public GameManager gameManager;
    public ObjectManager objectManager;
    public bool isHit;
    public bool isBoomTimes;
    // Update is called once per frame

    public GameObject bulletA;
    public GameObject bulletB;
    public GameObject boomEffect;

    public GameObject[] followers;

    public bool respawnTime;


    public bool[] joyControl;
    public bool isControl;

    public bool isButtonA;
    public bool isButtonB;
    private void Awake()
    {
            animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        Unbeattable();
        Invoke("Unbeattable", 3);

    }

    void Unbeattable()
    {
        respawnTime = !respawnTime;
        if(respawnTime)
        {
            respawnTime = true;
            spriteRenderer.color = new Color(1, 1, 1, 0.5f);

            for(int index=0; index<followers.Length; index++)
            {
                followers[index].GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);
            }
        }
        else
        {
            respawnTime=false;
            spriteRenderer.color = new Color(1, 1, 1, 1);
            for (int index = 0; index < followers.Length; index++)
            {
                followers[index].GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            }
        }
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
        if (!isButtonB)
            return;
       // if (!Input.GetButton("Jump"))
         //   return;
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
        GameObject[] enemiesB = objectManager.GetPool("enemyB");
        for (int index = 0; index < enemiesL.Length; index++)
        {
            if (enemiesL[index].activeSelf)
            {
                Enemy enemyLogic = enemiesL[index].GetComponent<Enemy>();
                enemyLogic.OnHit(1000);
            }
           
        
        }
        for (int index = 0; index < enemiesM.Length; index++)
        {
            if (enemiesM[index].activeSelf)
            {
                Enemy enemyLogic = enemiesM[index].GetComponent<Enemy>();
                enemyLogic.OnHit(1000);
            }
            
        }
        for (int index = 0; index < enemiesS.Length; index++)
        {
            if (enemiesS[index].activeSelf)
            {
                Enemy enemyLogic = enemiesS[index].GetComponent<Enemy>();
                enemyLogic.OnHit(1000);
            }
        }
        for (int index = 0; index < enemiesB.Length; index++)
        {
            if (enemiesB[index].activeSelf)
            {
                Enemy enemyLogic = enemiesB[index].GetComponent<Enemy>();
                enemyLogic.OnHit(1000);
            }
        }
        //
        GameObject[] bulletsA = objectManager.GetPool("bulletEnemyA");
        GameObject[] bulletsB = objectManager.GetPool("bulletEnemyB");
        GameObject[] bulletBossA = objectManager.GetPool("bulletBossA");
        GameObject[] bulletBossB = objectManager.GetPool("bulletBossB");
        for (int index = 0; index < bulletsA.Length; index++)
        {
            bulletsA[index].SetActive(false);
        }
        for (int index = 0; index < bulletsB.Length; index++)
        {
            bulletsB[index].SetActive(false);
        }
        for (int index = 0; index < bulletBossA.Length; index++)
        {
            bulletBossA[index].SetActive(false);
        }
        for (int index = 0; index < bulletBossB.Length; index++)
        {
            bulletBossB[index].SetActive(false);
        }
        isBoomTimes = true;
        isButtonB = false;
    }

    public void ButtonADown()
    {
        isButtonA = true;
    }

    public void ButtonAUp()
    {
        isButtonA = false;
    }

    public void ButtonBDown()
    {
        isButtonB = true;
    }
    void Fire()
    {
        if (!isButtonA)
            return;
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
            default:
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
    public void JoyPanel(int type)
    {
        for(int index =0; index<9; index++)
        {
            joyControl[index] = index == type;
        }
    }

    public void JoyDown()
    {
        isControl = true;
    }
    public void JoyUp()
    {
        isControl = false;
    }
    void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        if(joyControl[0]) { h = -1; v = 1; }
        if (joyControl[1]) { h = 0; v = 1; }
        if (joyControl[2]) { h = 1; v = 1; }
        if (joyControl[3]) { h = -1; v = 0; }
        if (joyControl[4]) { h = 0; v = 0; }
        if (joyControl[5]) { h = 1; v = 0; }
        if (joyControl[6]) { h = -1; v = -1; }
        if (joyControl[7]) { h = 0; v = -1; }
        if (joyControl[8]) { h = 1; v = -1; }





        if ((isTouchRight && h == 1) || (isTouchLeft && h == -1)|| !isControl)
        {
            h = 0;
        }

        if ((isTouchTop && v == 1) || (isTouchBottom && v == -1)|| !isControl)
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

            if (respawnTime)
                return;
            if (isHit)
                return;
            isHit = true;
            life--;
            gameManager.UpdateLifeIcon(life);
            gameManager.CallExplosion(transform.position, "P");
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
                    }
                    else
                    {
                        power++;
                        AddFlower();
                    }
                    
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

    void AddFlower()
    {
        if(power == 4)
        {
            followers[0].SetActive(true);
        } else if (power == 5)
            
            {
                followers[1].SetActive(true);
            }
        else if (power == 6)

        {
            followers[2].SetActive(true);
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
