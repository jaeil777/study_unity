using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject[] enemyObjs;
    public Transform[] spawnPoints;
    Player playerLogic;
    public float maxSpawnDelay;
    public float curSpawnDelay;

    public GameObject player;
    public Text scoreText;
    public Image[] lifeImage;
    public GameObject gameOverSet;


    private void Awake()
    {
        playerLogic =player.GetComponent<Player>();
    }
    private void Update()
    {
        curSpawnDelay += Time.deltaTime;

        if (curSpawnDelay > maxSpawnDelay)
        {
            SpanEnemy();
            maxSpawnDelay = Random.Range(0.5f, 3f);
            curSpawnDelay = 0;

        }
   
        
        scoreText.text = string.Format("{0:n0}", playerLogic.score);
    }

    void SpanEnemy()
    {
        int ranEnemy = Random.Range(0, 3);
        int ranPoint = Random.Range(0, 9);
        GameObject enemy = Instantiate(enemyObjs[ranEnemy]
            , spawnPoints[ranPoint].position
            , spawnPoints[ranPoint].rotation);
        Rigidbody2D rigid = enemy.GetComponent<Rigidbody2D>();
        Enemy enemyLogic = enemy.GetComponent<Enemy>();
        enemyLogic.player = player;
        if (ranPoint == 6 || ranPoint == 8)
        { //Right Spawn
            enemy.transform.Rotate(Vector3.back * 90);
            rigid.velocity = new Vector2(enemyLogic.speed * (-1), -1);
        }
        else if (ranPoint == 5 || ranPoint == 7)
        {//Left Spawn
            enemy.transform.Rotate(Vector3.forward * 90);
            rigid.velocity = new Vector2(enemyLogic.speed, -1);
        }
        else
        { // Front Spawn
            rigid.velocity = new Vector2(0, enemyLogic.speed * (-1));
        }
    }

    public void RespawnPlayer()
    {
        Invoke("RespawnPlayerExe", 2f);

    }

    void RespawnPlayerExe()
    {
        player.transform.position = Vector3.down *3.5f;
        player.SetActive(true);
        playerLogic.isHit = false;
    }

    public void UpdateLifeIcon(int life)
    {
       
            for (int index = 0; index < 3; index++)
            {
                lifeImage[index].color = new Color(1, 1, 1, 0);
            }

            for (int index = 0; index < life; index++)
            {
                lifeImage[index].color = new Color(1, 1, 1, 1);
            }
        
      
    }

    public void GameOver()
    {
        gameOverSet.SetActive(true);
    }

    public void GameRty()
    {
        SceneManager.LoadScene(0);
    }
}

