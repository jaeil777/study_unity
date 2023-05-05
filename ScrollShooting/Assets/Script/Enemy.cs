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

    public GameObject player;
    public GameObject bulletA;
    public GameObject bulletB;
    public GameObject itemPower;
    public GameObject itemBoom;
    public GameObject itemCoin;


void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();


    }

 void OnEnable()
    {
        switch (enemyName)
        {
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
  
        Fire();
        Reload();
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
        spriteRenderer.sprite = sprites[1];
        Invoke("ReturnSprite", 0.1f);

        if(health <= 0)
        {
            Player playerLogic = player.GetComponent<Player>();
            playerLogic.score += enemyScore;
            
            int ran = Random.Range(0, 10);
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
        }
    }

    void ReturnSprite()
    {
        spriteRenderer.sprite = sprites[0];
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "BorderBullet")
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
