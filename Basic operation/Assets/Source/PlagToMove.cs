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
           Vector3.MoveTowards(transform.position, target, 0.01f); // 1f�� �ӵ��� ����ɵ�

       */



        //2.SmoothDamp
        /*
        Vector3 velo = Vector3.zero;

        transform.position =
            Vector3.SmoothDamp(transform.position, target, ref velo, 0.1f);  //�Ű����� (������ġ,��ǥ��ġ. �����ӵ� .�ӵ�)
                                                                             //�߰��ٰ� �ε巴�� ����
        //���� �����ټ��� ������
        */

        //3.Lerp (���� ����)
        /*
        transform.position =
           Vector3.Lerp(transform.position, target, 0.05f);
        //���� �ð��� ���̰� �� �Ű������� ����ؼ� �ӵ��� �����Ѵ�.
        */

        //4.SLerp (���� ��������)
        transform.position =
           Vector3.Slerp(transform.position, target, 0.05f); //���������� �̵��� slerp

    }
}
