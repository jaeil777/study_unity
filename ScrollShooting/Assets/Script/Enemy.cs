using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    public string enemyName;
    public float speed;
    public int health;

    public float maxShotDelay;
    public float curShotDelay;

    public Sprite[] sprites;
    SpriteRenderer spriteRenderer;

    public GameObject player;
    public GameObject bulletA;
    public GameObject bulletB;


    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();


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

            GameObject Bullet = Instantiate(bulletA, transform.position, transform.rotation);
            Rigidbody2D rigid = Bullet.GetComponent<Rigidbody2D>();

            Vector3 dirVec = player.transform.position -transform.position;
            rigid.AddForce(dirVec * 3, ForceMode2D.Impulse);
            curShotDelay = 0;

        }
        else if(enemyName == "L")
        {
            GameObject BulletR = Instantiate(bulletB, transform.position+Vector3.right*0.3f, transform.rotation);
            GameObject BulletL = Instantiate(bulletB, transform.position+Vector3.left*0.3f, transform.rotation);

            Rigidbody2D rigidL = BulletL.GetComponent<Rigidbody2D>();
            Rigidbody2D rigidR = BulletR.GetComponent<Rigidbody2D>();

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

    void OnHit(int dmg)
    {
        health -= dmg;
        spriteRenderer.sprite = sprites[1];
        Invoke("ReturnSprite", 0.1f);

        if(health <= 0)
        {
            Destroy(gameObject);
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
            Destroy(gameObject);
        }else if(collision.gameObject.tag == "PlayerBullet")
        {
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            OnHit(bullet.dmg);
            Destroy(collision.gameObject);
        }
    }

}
