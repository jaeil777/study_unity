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
            Debug.Log("���̵�޴� ��");
        }
        else if (gameObject.activeSelf == false)
        {
            gameObject.SetActive(true);
            Debug.Log("���̵�޴� ��");
        }
    }

    public void TurnOnObject(GameObject selectedObject)
    {
        foreach (GameObject obj in objects) // ��� ������Ʈ�� ��ȸ�ϸ�
        {
            if (obj == selectedObject) // ���õ� ������Ʈ�� ��� �ѱ�
            {
                obj.SetActive(true);
            }
            else // ���õ��� ���� ������Ʈ�� ��� ����
            {
                obj.SetActive(false);
            }
        }
    }

   
}
