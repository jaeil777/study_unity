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
        talkData.Add(100, new string[] { "그냥 책상이다. " });
        talkData.Add(200, new string[] { "무언가가 들어있는 상자이다. " });
        talkData.Add(1000, new string[] { "안녕!! 내이름은 루나야 :0 ", "여기에 처음왔구나? :1","저쪽으로 가봐~:2" });
        talkData.Add(1001, new string[] { "반갑다! 내이름은 루도 :0 ", "상자를 눌러보렴 :1" });

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
            string[] talks = talkData[id]; // 해당 id에 대한 대화 배열 가져오기
            if (talkIndex >= 0 && talkIndex < talks.Length) // talkIndex가 유효한 범위 내에 있는지 확인
            {
                return talks[talkIndex]; // 해당 대화 리턴
            }
            else
            {
               return null;
            }

        }
        else
        {
            Debug.LogError("Talk data not found for id: " + id); // 해당 id에 대한 대화 데이터가 없는 경우 에러 처리
        }

        return string.Empty;
    }

    public Sprite GetPortrait(int id, int portraitIndex)
    {
        return portraitData[id + portraitIndex];
    }

}
