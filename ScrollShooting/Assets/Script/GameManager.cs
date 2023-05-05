using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using System.Runtime.InteropServices.ComTypes;

public class GameManager : MonoBehaviour
{
    public string[] enemyObjs;
    public Transform[] spawnPoints;
    Player playerLogic;
    public float nextSpawnDelay;
    public float curSpawnDelay;

    public GameObject player;
    public Text scoreText;
    public Image[] lifeImage;
    public Image[] boomImage;
    public GameObject gameOverSet;
    public ObjectManager objectManager;


    public List<Spawn> spawnList;
    public int spawnIndex;
    public bool spawnEnd;
    private void Awake()
    {
        spawnList = new List<Spawn>();
        enemyObjs = new string[] {"enemyS" , "enemyM" , "enemyL" };
        playerLogic =player.GetComponent<Player>();
        ReadSpawnFile();


    }
    void ReadSpawnFile()
    {
        //초기화
       spawnList.Clear();
       spawnIndex = 0;
        spawnEnd = false;
        // 리스폰 파일읽기
        TextAsset textFile = Resources.Load("Stage0") as TextAsset;
        StringReader stringReader = new StringReader(textFile.text);
        // 리스폰 데이터 생성

        while (stringReader != null)
        {
            string line = stringReader.ReadLine();
            Debug.Log(line);
            if (line == null)
            {
                break;
            }


            Spawn spawnData = new Spawn();
            spawnData.delay = float.Parse(line.Split(',')[0]);
            spawnData.type = line.Split(',')[1];
            spawnData.point = int.Parse(line.Split(',')[2]);
            spawnList.Add(spawnData);
        }
        stringReader.Close();

        nextSpawnDelay = spawnList[0].delay;
    }
    private void Update()
    {
        curSpawnDelay += Time.deltaTime;

        if (curSpawnDelay > nextSpawnDelay && !spawnEnd)
        {
            SpanEnemy();
            
            curSpawnDelay = 0;

        }
   
        
        scoreText.text = string.Format("{0:n0}", playerLogic.score);
    }

    void SpanEnemy()
    {
        int enemyIndex = 0;
        switch(spawnList[spawnIndex].type)
        {
            case "S":
                enemyIndex = 0; 
                break;

            case "M":
                enemyIndex = 1;
                break;

            case "L":
                enemyIndex = 2;
                break;
        }
        int enemyPoint = spawnList[spawnIndex].point; 

        GameObject enemy = objectManager.MakeObj(enemyObjs[enemyIndex]);
        enemy.transform.position = spawnPoints[enemyPoint].position;
        Rigidbody2D rigid = enemy.GetComponent<Rigidbody2D>();
        Enemy enemyLogic = enemy.GetComponent<Enemy>();
        enemyLogic.player = player;
        enemyLogic.objectManager = objectManager;
        if (enemyPoint == 6 || enemyPoint == 8)
        { //Right Spawn
            enemy.transform.Rotate(Vector3.back * 90);
            rigid.velocity = new Vector2(enemyLogic.speed * (-1), -1);
        }
        else if (enemyPoint == 5 || enemyPoint == 7)
        {//Left Spawn
            enemy.transform.Rotate(Vector3.forward * 90);
            rigid.velocity = new Vector2(enemyLogic.speed, -1);
        }
        else
        { // Front Spawn
            rigid.velocity = new Vector2(0, enemyLogic.speed * (-1));
        }
        spawnIndex++;
        if(spawnIndex == spawnList.Count)
        {
            spawnEnd = true;
            return;
        }

        nextSpawnDelay = spawnList[spawnIndex].delay;
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
    public void UpdateBoomIcon(int boom)
    {

        for (int index = 0; index < 3; index++)
        {
            boomImage[index].color = new Color(1, 1, 1, 0);
        }

        for (int index = 0; index < boom; index++)
        {
            boomImage[index].color = new Color(1, 1, 1, 1);
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

