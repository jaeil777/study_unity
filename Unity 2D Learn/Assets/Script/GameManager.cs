using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public int stageIndex;
    public int totalscore;
    public int stagescore;
    public int health;

    public PlayerMove player;
    public GameObject[] stages;

    public Text UITotalScore;
    public Text UIStageScore;
    public Text UIStage;
    public Image[] UIHealth;



    private void Update()
    {
        UITotalScore.text = "TOTAL"+(totalscore + stagescore).ToString();
        UIStageScore.text = "STAGE"+ stagescore.ToString();
    }
    public void NextStage()
    {
        //다음스테이지
        if(stageIndex+1 < stages.Length)
        {
   
            stages[stageIndex].SetActive(false);
            stageIndex++;
            stages[stageIndex].SetActive(true);
            PlayerReposition();
            UIStage.text = "STAGE" + (stageIndex + 1);
        }
        else
        {
            Time.timeScale = 0;
            
            Debug.Log("게임클리어");
        }
        

        totalscore += stagescore;
        stagescore = 0; 
    }
    public void HealthDown()
    {
        if(health > 1)
        {
            health--;
            UIHealth[health].color = new Color(1, 1, 1, 0f);
        }
        else
        {
            player.OnDie();

        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            HealthDown();
            PlayerReposition();
        }
    }

    void PlayerReposition()
    {
        player.transform.position = new Vector3(-6, 2.5f, 0);
        player.VelocityZero();
    }
}
