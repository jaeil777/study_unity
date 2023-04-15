using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CookieHandler : MonoBehaviour
{

    public GameObject[] objects;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PopUp()
    {
        if (gameObject.activeSelf == true)
        {
            gameObject.SetActive(false);
            Debug.Log("사이드메뉴 끔");
        }
        else if (gameObject.activeSelf == false)
        {
            gameObject.SetActive(true);
            Debug.Log("사이드메뉴 켬");
        }
    }

    public void TurnOnObject(GameObject selectedObject)
    {
        foreach (GameObject obj in objects) // 모든 오브젝트를 순회하며
        {
            if (obj == selectedObject) // 선택된 오브젝트일 경우 켜기
            {
                obj.SetActive(true);
            }
            else // 선택되지 않은 오브젝트일 경우 끄기
            {
                obj.SetActive(false);
            }
        }
    }

   
}
