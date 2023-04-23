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

        //Talk Data
        //�糪 :1000 �絵 :2000
        //å�� :100 ���� :200
        talkData.Add(100, new string[] { "�׳� å���̴�. " });
        talkData.Add(200, new string[] { "���𰡰� ����ִ� �����̴�. " });
        talkData.Add(1000, new string[] { "�ȳ�!! ���̸��� �糪�� :0 ", "���⿡ ó���Ա���? :1","�������� ����~:2" });
        talkData.Add(2000, new string[] { "�ݰ���! ���̸��� �絵 :0 ", "���ڸ� �������� :1" });

        //Quest Data
        talkData.Add(10+1000, new string[] { "��� :0 ", "�츮 ������ ������ ���� �˾�? :1", "�絵���� �����:2" });

        talkData.Add(11 + 2000, new string[] { "�� �츮������ ���� �����? :0 ", "�¾� ���� �糪���� ������� :1", "�׷��� ������ �� ������ ã���� :2" });

       
        talkData.Add(20 + 2000, new string[] { "������ ã���� ��Ź�Ұ� :1" });

        talkData.Add(20 + 5000, new string[] { "������ ã�Ҵ�" });
        talkData.Add(21 + 2000, new string[] { "ã���༭ ���� :2" });



        //Portrait Data
        portraitData.Add(1000 + 0, portraitArr[0]);
        portraitData.Add(1000 + 1, portraitArr[1]);
        portraitData.Add(1000 + 2, portraitArr[2]);
        portraitData.Add(1000 + 3, portraitArr[3]);

        portraitData.Add(2000 + 0, portraitArr[4]);
        portraitData.Add(2000 + 1, portraitArr[5]);
        portraitData.Add(2000 + 2, portraitArr[6]);
        portraitData.Add(2000 + 3, portraitArr[7]);
        
    }

    public string GetTalk(int id, int talkIndex)
    {
        if (!talkData.ContainsKey(id))
        {
            if(!talkData.ContainsKey(id-id%10))
               return GetTalk(id-id %100, talkIndex);
            else
               return GetTalk(id-id%10, talkIndex);

      
        }

        if(talkIndex == talkData[id].Length)
        {
            return null;
        }else
            return talkData[id][(int)talkIndex];
    }

    public Sprite GetPortrait(int id, int portraitIndex)
    {
        return portraitData[id + portraitIndex];
    }

}
