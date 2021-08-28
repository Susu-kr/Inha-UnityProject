using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager2D : MonoBehaviour
{
    public GameObject[] enemyObjs;
    public Transform[] spawnPoints;
    public float maxSpawnDelay;
    public float curSpawnDelay;

    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        curSpawnDelay += Time.deltaTime;

        if(curSpawnDelay > maxSpawnDelay)
        {
            SpawnEnemy();
            maxSpawnDelay = Random.Range(0.5f, 3.0f);
            curSpawnDelay = 0;
        }
    }

    void SpawnEnemy()
    {
        // �����ϰ� ���� ����
        int randomEnemy = Random.Range(0, 2); // ū ��, ���� ��
        int randomPoint = Random.Range(0, 5); // �����Ǵ� ��ġ
        GameObject enemy = Instantiate(enemyObjs[randomEnemy], spawnPoints[randomPoint].position, spawnPoints[randomPoint].rotation);
        Enemy2D enemyLogic = enemy.GetComponent<Enemy2D>();
        enemyLogic.player = player;
    }


    public void RespawnPlayer()
    {
        Invoke("RespawnPlayerExe", 2f);
    }

    void RespawnPlayerExe()
    {
        player.transform.position = Vector3.left * 285.0f;
        player.SetActive(true);
    }
}
