using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public TalkManager talkManager;
    public QuestManager questManager;
    public GameObject talkBox;
    public Text talkText;
    public Image portratImg;
    public GameObject scanObject;
    public int talkIndex;

    public bool isAction;


    private void Start()
    {
        Debug.Log(questManager.checkQuest());
    }

    public void Action(GameObject scObj)
    {
        scanObject = scObj;
        ObjectData objData =scObj.GetComponent<ObjectData>();
        Talk(objData.id, objData.isNpc);
        talkBox.SetActive(isAction);
     
    }

    void Talk(int id, bool isNpc)
    {
        //Set Talk Data
        int questTalkIndex = questManager.GetQuestTalkIndex(id);
        string talkData = talkManager.GetTalk(id+ questTalkIndex, talkIndex); 
        //End Talk
        if(talkData == null)
        {
            isAction = false;
            talkIndex = 0;
            questManager.checkQuest(id);
            Debug.Log(questManager.checkQuest(id));
            return;
        }


        //Continue Talk
        if (isNpc)
        {
            talkText.text = talkData.Split(':')[0];
          //Show Portrait
            portratImg.sprite = talkManager.GetPortrait(id, int.Parse(talkData.Split(':')[1]));
            portratImg.color = new Color(1, 1, 1, 1);
        }
        else
        {   
            //Hide Portrait
            talkText.text = talkData;
            portratImg.color = new Color(1, 1, 1, 0);

        }
        isAction = true; 
        talkIndex++;
    }
    // Update is called once per frame
    void Update()
    {
     
    }
}
