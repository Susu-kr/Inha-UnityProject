using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineTest : MonoBehaviour
{
    IEnumerator enumerator;
    // Start is called before the first frame update
    void Start()
    {
        //enumerator = TestCoroutine();
        //StartCoroutine(enumerator);
        StartCoroutine("TestCoroutine");
        //StopCoroutine("TestCoroutine"); �ܺο��� �ߴ�
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator TestCoroutine()
    {
        int i = 0;
        while(true)
        {
            Debug.Log("TestCoroutine" + i.ToString());
            yield return null;
            i++;
            if (i > 1000) // ���ο��� �ߴ�
                yield break;
        }
    }
}
