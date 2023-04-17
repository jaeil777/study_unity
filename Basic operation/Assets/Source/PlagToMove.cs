using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlagToMove : MonoBehaviour
{

    Vector3 target = new Vector3(0, 1.5f, 3);



    // Update is called once per frame
    void Update()
    {
        // MoveToTowards
        /* 
       transform.position =
           Vector3.MoveTowards(transform.position, target, 0.01f); // 1f는 속도라 보면될듯

       */



        //2.SmoothDamp
        /*
        Vector3 velo = Vector3.zero;

        transform.position =
            Vector3.SmoothDamp(transform.position, target, ref velo, 0.1f);  //매개변수 (현재위치,목표위치. 참조속도 .속도)
                                                                             //쭉가다가 부드럽게 멈춤
        //값을 적게줄수록 빨라짐
        */

        //3.Lerp (선형 보간)
        /*
        transform.position =
           Vector3.Lerp(transform.position, target, 0.05f);
        //감속 시간의 차이가 남 매개변수에 비례해서 속도가 증가한다.
        */

        //4.SLerp (구면 선형보간)
        transform.position =
           Vector3.Slerp(transform.position, target, 0.05f); //포물선으로 이동함 slerp

    }
}
