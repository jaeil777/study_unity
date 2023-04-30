using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] enemyObjs;
    public Transform[] spawnPoints;

    public float maxSpawnDelay;
    public float curSpawnDelay;

    public GameObject player;


    private void Update()
    {
        curSpawnDelay += Time.deltaTime;

        if (curSpawnDelay > maxSpawnDelay)
        {
            SpanEnemy();
            maxSpawnDelay = Random.Range(0.5f, 3f);
            curSpawnDelay = 0;

        }
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
    }
}

