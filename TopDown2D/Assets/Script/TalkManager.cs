using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{

    Dictionary<int, string[]> talkData;
    Dictionary<int, Sprite> portraitData;

    public Sprite[] portraitArr;

    // Start is called before the first frame update
    void Awake()
    {
        talkData = new Dictionary<int, string[]>();
        portraitData = new Dictionary<int, Sprite>();
        GenerateData();
    }

    void GenerateData()
    {
        talkData.Add(100, new string[] { "�׳� å���̴�. " });
        talkData.Add(200, new string[] { "���𰡰� ����ִ� �����̴�. " });
        talkData.Add(1000, new string[] { "�ȳ�!! ���̸��� �糪�� :0 ", "���⿡ ó���Ա���? :1","�������� ����~:2" });
        talkData.Add(1001, new string[] { "�ݰ���! ���̸��� �絵 :0 ", "���ڸ� �������� :1" });

        portraitData.Add(1000 + 0, portraitArr[0]);
        portraitData.Add(1000 + 1, portraitArr[1]);
        portraitData.Add(1000 + 2, portraitArr[2]);
        portraitData.Add(1000 + 3, portraitArr[3]);

        portraitData.Add(1001 + 0, portraitArr[4]);
        portraitData.Add(1001 + 1, portraitArr[5]);
        portraitData.Add(1001 + 2, portraitArr[6]);
        portraitData.Add(1001 + 3, portraitArr[7]);

    }

    public string GetTalk(int id, int talkIndex)
    {
        if (talkData.ContainsKey(id))
        {
            string[] talks = talkData[id]; // �ش� id�� ���� ��ȭ �迭 ��������
            if (talkIndex >= 0 && talkIndex < talks.Length) // talkIndex�� ��ȿ�� ���� ���� �ִ��� Ȯ��
            {
                return talks[talkIndex]; // �ش� ��ȭ ����
            }
            else
            {
               return null;
            }

        }
        else
        {
            Debug.LogError("Talk data not found for id: " + id); // �ش� id�� ���� ��ȭ �����Ͱ� ���� ��� ���� ó��
        }

        return string.Empty;
    }

    public Sprite GetPortrait(int id, int portraitIndex)
    {
        return portraitData[id + portraitIndex];
    }

}
