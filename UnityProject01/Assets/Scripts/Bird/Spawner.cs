using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject ObstaclePrefab;
    public float Interval = 2.0f;
    public float range = 3.0f;

    IEnumerator Start() // �����ð� ��� ����
    {
        while(true)
        {
            if(FlappyBird.Instance.score % 10 == 0 && FlappyBird.Instance.score != 0)
            {
                if(Interval >= 1.3f && range >= 2.3f)
                {
                    Interval -= 0.1f;
                    range -= 0.1f;
                }
            }
            transform.position = new Vector3(transform.position.x,
                Random.Range(-range, range), transform.position.z);
            GameObject obj = Instantiate(ObstaclePrefab, transform.position, transform.rotation);
            Destroy(obj, 5.0f);
            yield return new WaitForSeconds(Interval); // sleep �� ���� Interval ���� ȣ��x
        }
    }


}
