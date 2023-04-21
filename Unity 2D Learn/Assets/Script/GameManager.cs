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
    public Text UIClear;
    public Button UIRetryButton;
    public Button UIRestartButton;
    public Image[] UIHealth;



    private void Update()
    {
        UITotalScore.text = "TOTAL"+(totalscore + stagescore).ToString();
        UIStageScore.text = "STAGE"+ stagescore.ToString();
    }

    public void OnClickUIRestartButton()
    {
        stages[stageIndex].SetActive(false);
        stageIndex = 0;
        health = 3;
        for (int i = 0; i < UIHealth.Length; i++)
        {
            UIHealth[i].color = new Color(1, 1, 1);
        }
        stages[stageIndex].SetActive(true);
        UIStage.text = "STAGE" + (stageIndex + 1);
        UIRetryButton.gameObject.SetActive(false);
        UIRestartButton.gameObject.SetActive(false);
        UIClear.gameObject.SetActive(false);
        if(Time.timeScale == 0)
            Time.timeScale = 1f;
        player.OnRvive();
        PlayerReposition();

    }

    public void OnClickUIRetryButton()
    {

        health = 3;
        for (int i = 0; i < UIHealth.Length; i++)
        {
            UIHealth[i].color = new Color(1, 1, 1);
        }
        UIRetryButton.gameObject.SetActive(false);
        UIRestartButton.gameObject.SetActive(false);
        UIClear.gameObject.SetActive(false);
        if (Time.timeScale == 0)
            Time.timeScale = 1f;
        player.OnRvive();
        PlayerReposition();

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
            UIStage.gameObject.SetActive(false);
            UIClear.text = "STAGE CLEAR";
            UIClear.gameObject.SetActive(true);
            UIRetryButton.gameObject.SetActive(true);
            UIRestartButton.gameObject.SetActive(true);
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
            UIClear.text = "STAGE FAIELD";
            UIClear.gameObject.SetActive(true);
            UIRetryButton.gameObject.SetActive(true);
            UIRestartButton.gameObject.SetActive(true);
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
