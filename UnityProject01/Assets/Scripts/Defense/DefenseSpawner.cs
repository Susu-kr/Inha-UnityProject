using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseSpawner : MonoBehaviour
{
    public GameObject ObstaclePrefab;
    public float Interval = 2.0f;
    public float x_range = 23.0f;
    public float z_range = 18.0f;


    IEnumerator Start() // 일정시간 계속 생성
    {
        while (true)
        {
            transform.position = new Vector3(Random.Range(-x_range, x_range),
                transform.position.y, Random.Range(-z_range, z_range));
            Instantiate(ObstaclePrefab, transform.position, transform.rotation);
            yield return new WaitForSeconds(Interval); // sleep 과 동일 Interval 동안 호출x
        }
    }
}
