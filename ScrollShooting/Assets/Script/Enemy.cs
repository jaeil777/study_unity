using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour
{
   public ObjectManager objectManager;

    public string enemyName;
    public float speed;
    public int health;
    public int enemyScore;

    public float maxShotDelay;
    public float curShotDelay;

    public Sprite[] sprites;
    SpriteRenderer spriteRenderer;
    Animator animator;

    public GameObject player;
    public GameObject bulletA;
    public GameObject bulletB;
    public GameObject itemPower;
    public GameObject itemBoom;
    public GameObject itemCoin;

    public GameManager gameManager;

    public int patternIndex;
    public int curPatternCount;
    public int[] maxPatternCount;

    void Awake()
    {

        spriteRenderer = GetComponent<SpriteRenderer>();

        if(enemyName == "B")
        {

            animator = GetComponent<Animator>();
        }

    }

 void OnEnable()
    {
        CancelInvoke("Stop");
        switch (enemyName)
        {
            case "B":
                health = 2000;

                Invoke("Stop", 2.5f);

                break;
            case "L":
                health = 40;
                break;
            case "M":
                health = 10;
                break;
            case "S":
                health = 3;
                break;
        }
    }

    void Update()
    {
        if (enemyName == "B")
            return;
        Fire();
        Reload();
    }


    void Stop()
    {
        if (!gameObject.activeSelf) {

            return;
        }
        

        Invoke("Think", 2);
        Rigidbody2D rigid = GetComponent<Rigidbody2D>();
        rigid.velocity = Vector2.zero; ;
    }

    void Think()
    {
        if (!gameObject.activeSelf)
        {

            return;
        }

        patternIndex = patternIndex == 3 ? 0 : patternIndex + 1;
        curPatternCount = 0;
        switch(patternIndex)
        {
            case 0:
                FireFoward();
                break; 
            case 1:
                FireShot();
                break;
            case 2:
                FireArc();
                break;
            case 3:
                FireAround();
                break;


        }


    }
    void FireFoward()
    {
        if (!gameObject.activeSelf)
        {

            return;
        }
        //앞으로 네발 발사
        GameObject bulletR = objectManager.MakeObj("bulletBossB");
        bulletR.transform.position = transform.position + Vector3.right * 0.5f;
        GameObject bulletRR = objectManager.MakeObj("bulletBossB");
        bulletRR.transform.position = transform.position + Vector3.right * 0.8f;
        GameObject bulletL = objectManager.MakeObj("bulletBossB");
        bulletL.transform.position = transform.position + Vector3.left * 0.5f;
        GameObject bulletLL = objectManager.MakeObj("bulletBossB");
        bulletLL.transform.position = transform.position + Vector3.left * 0.8f;

        Rigidbody2D rigidL = bulletL.GetComponent<Rigidbody2D>();
        Rigidbody2D rigidLL = bulletLL.GetComponent<Rigidbody2D>();
        Rigidbody2D rigidR = bulletR.GetComponent<Rigidbody2D>();
        Rigidbody2D rigidRR = bulletRR.GetComponent<Rigidbody2D>();

        rigidR.AddForce(Vector2.down * 8, ForceMode2D.Impulse);
        rigidRR.AddForce(Vector2.down * 8, ForceMode2D.Impulse);
        rigidL.AddForce(Vector2.down * 8, ForceMode2D.Impulse);
        rigidLL.AddForce(Vector2.down * 8, ForceMode2D.Impulse);


        curPatternCount++;
        if (curPatternCount < maxPatternCount[patternIndex])
            Invoke("FireFoward", 2);
        else
        {
            Invoke("Think", 3);
        }
 
    }
    void FireShot()
    {
        if (!gameObject.activeSelf)
        {

            return;
        }

        for (int index =0; index <5;  index++)
        {
            GameObject bullet = objectManager.MakeObj("bulletBossA");
            bullet.transform.position = transform.position;

            Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();

            Vector2 dirVec = player.transform.position - transform.position;
            Vector2 ranVec = new Vector2(Random.Range(-0.5f, 0.5f), Random.Range(0f, 2f));
            dirVec += ranVec;
            rigid.AddForce(dirVec.normalized * 3, ForceMode2D.Impulse);

        }

        //플레이어 방향 샷건

        curPatternCount++;
        if (curPatternCount < maxPatternCount[patternIndex])
            Invoke("FireShot", 2);
        else
        {
            Invoke("Think", 3);
        }

    }
    void FireArc()
    {
        if (!gameObject.activeSelf)
        {

            return;
        }
        //부채모양으로발사
        GameObject bullet = objectManager.MakeObj("bulletBossB");
        bullet.transform.position = transform.position;
        bullet.transform.rotation = Quaternion.identity; 

        Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();

        Vector2 dirVec = new Vector2(Mathf.Sin(Mathf.PI*10*curPatternCount / maxPatternCount[patternIndex]), -1);
        rigid.AddForce(dirVec.normalized * 3, ForceMode2D.Impulse);


        curPatternCount++;
        if (curPatternCount < maxPatternCount[patternIndex])
            Invoke("FireArc", 0.15f);
        else
        {
            Invoke("Think", 3);
        }

    }
    void FireAround()
    {
        if (!gameObject.activeSelf)
        {

            return;
        }

        //원형태전체공격

        int roundNumA = 40;
        int roundNumB = 30;
        int roundNum = curPatternCount%2==0 ? roundNumA : roundNumB;
        for (int index = 0; index < roundNumA; index++)
        {
            GameObject bullet = objectManager.MakeObj("bulletBossB");
            bullet.transform.position = transform.position;
            bullet.transform.rotation = Quaternion.identity;

            Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();

            Vector2 dirVec = new Vector2(Mathf.Cos(Mathf.PI * 2 * index / roundNum),
                                         Mathf.Sin(Mathf.PI * 2 * index / roundNum));
            rigid.AddForce(dirVec.normalized * 1.5f, ForceMode2D.Impulse);

            Vector3 roVec = Vector3.forward * 360 * index / roundNum+ Vector3.forward*90;
            bullet.transform.Rotate(roVec);


        }
        curPatternCount++;
        if (curPatternCount < maxPatternCount[patternIndex])
            Invoke("FireAround", 1.5f);
        else
        {
            Invoke("Think", 3);
        }

    }

    private void Fire()
    {
      
        //장전이 되지 않았을떄 
        if (curShotDelay < maxShotDelay)
            return;

        if(enemyName == "S")
        {

            GameObject bullet = objectManager.MakeObj("bulletEnemyA");
            bullet.transform.position = transform.position;

            Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();

            Vector3 dirVec = player.transform.position -transform.position;
            rigid.AddForce(dirVec.normalized * 3, ForceMode2D.Impulse);
            curShotDelay = 0;

        }
        else if(enemyName == "L")
        {
            GameObject bulletR = objectManager.MakeObj("bulletEnemyB");
            bulletR.transform.position = transform.position + Vector3.right * 0.3f;
            GameObject bulletL = objectManager.MakeObj("bulletEnemyB");
            bulletL.transform.position = transform.position + Vector3.left * 0.3f;

            Rigidbody2D rigidL = bulletL.GetComponent<Rigidbody2D>();
            Rigidbody2D rigidR = bulletR.GetComponent<Rigidbody2D>();

            Vector3 dirVecR = player.transform.position - (transform.position+ Vector3.right*0.3f);
            Vector3 dirVecL = player.transform.position - (transform.position + Vector3.left * 0.3f);
            rigidR.AddForce(dirVecR.normalized * 4, ForceMode2D.Impulse);
            rigidL.AddForce(dirVecL.normalized * 4, ForceMode2D.Impulse);
            curShotDelay = 0;
        }

    }
    private void Reload()
    {
        curShotDelay += Time.deltaTime;

    }

    public void OnHit(int dmg)
    {
        if (health <= 0)
            return;


        health -= dmg;
        if(enemyName == "B")
        {
            animator.SetTrigger("OnHit");
        }
        else
        {
            spriteRenderer.sprite = sprites[1];
            Invoke("ReturnSprite", 0.1f);
        }
       

        if(health <= 0)
        {
            Player playerLogic = player.GetComponent<Player>();
            playerLogic.score += enemyScore;
            
            int ran = enemyName =="B" ? 0: Random.Range(0, 10);
            if(ran < 4)
            {

            }else if(ran < 7)
            {
              GameObject itemCoin=  objectManager.MakeObj("itemCoin");
                itemCoin.transform.position = transform.position;      
            }
            else if(ran < 9)
            {
                GameObject itemPower = objectManager.MakeObj("itemPower");
                itemPower.transform.position = transform.position;
            }
            else if(ran < 10)
            {
                GameObject itemBoom = objectManager.MakeObj("itemBoom");
                itemBoom.transform.position = transform.position;
            }
            gameObject.SetActive(false);
            transform.rotation = Quaternion.identity;
            gameManager.CallExplosion(transform.position, enemyName);

            if(enemyName == "B")
            {
                gameManager.StageEnd();
            }
        }
    }

    void ReturnSprite()
    {
        spriteRenderer.sprite = sprites[0];
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "BorderBullet" &&enemyName !="B")
        {
            gameObject.SetActive(false);
            transform.rotation = Quaternion.identity;
        }
        else if(collision.gameObject.tag == "PlayerBullet")
        {
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            OnHit(bullet.dmg);
           collision.gameObject.SetActive(false);
        }
    }

}
