using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TypeEffect : MonoBehaviour
{
    public GameObject EndCursor;
    public int charPerSecond;
    string targetMsg;
    Text msgText;
    int index;
    float interval;

    private void Awake()
    {
        msgText = GetComponent<Text>();
    }
    public void SetMsg(string msg)
    {
        targetMsg = msg;
        EffectStart();
    }


    void EffectStart()
    {
        msgText.text = "";
        index = 0;
        EndCursor.SetActive(false);

        interval = 1.0f / charPerSecond;
        Invoke("Effecting" , interval);
    }

    void Effecting()
    {
        if(msgText.text == targetMsg) {
            EffectEnd();
            return;
        }

        msgText.text += targetMsg[index];     //문자열도 배열처럼 접근 가능 
        index++;
        Invoke("Effecting", interval);


    }

    void EffectEnd()
    {
        EndCursor.SetActive(true);
    }

}
