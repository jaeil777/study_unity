using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public TalkManager talkManager;
    public GameObject talkBox;
    public Text talkText;
    public Image portratImg;
    public GameObject scanObject;
    public int talkIndex;

    public bool isAction;

    public void Action(GameObject scObj)
    {
        scanObject = scObj;
        ObjectData objData =scObj.GetComponent<ObjectData>();
        Talk(objData.id, objData.isNpc);
        talkBox.SetActive(isAction);
     
    }

    void Talk(int id, bool isNpc)
    {
   
        string talkData = talkManager.GetTalk(id, talkIndex); 
        if(talkData == null)
        {
            isAction = false;
            talkIndex = 0;
            return;
        }

        if (isNpc)
        {
            talkText.text = talkData.Split(':')[0];
            portratImg.sprite = talkManager.GetPortrait(id, int.Parse(talkData.Split(':')[1]));
            portratImg.color = new Color(1, 1, 1, 1);
        }
        else
        {
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
