using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject talkBox;
    public Text talkText;
    public GameObject scanObject;
    public bool isAction;

    public void Action(GameObject scObj)
    {
        if (isAction)
        {
            isAction = false;
     
        }
        else
        {
            isAction = true;
   
            scanObject = scObj;
            talkText.text = "�̰��� �̸��� " + scanObject.name + "�̾�";
        }
        talkBox.SetActive(isAction);
     
    }
    // Update is called once per frame
    void Update()
    {
     
    }
}
