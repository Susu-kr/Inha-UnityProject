using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager2D : MonoBehaviour
{
    public string[] enemyObjs;
    public Transform[] spawnPoints;
    public float maxSpawnDelay;
    public float curSpawnDelay;

    public GameObject player;

    // UI
    public Text scoreText;
    public Image[] lifeImage;
    public Image[] boomImage;
    public GameObject gameOverSet;

    // Object Pool
    public ObjectManager objectManager;

    private void Awake()
    {
        enemyObjs = new string[] { "EnemyL", "EnemyS" };
    }
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

        //#.UI Score Update
        Player2D playerLogic = player.GetComponent<Player2D>();
        scoreText.text = string.Format("{0:n0}",playerLogic.score); // 3자리마다 끊어주는 방법
    }

    void SpawnEnemy()
    {
        // 랜덤하게 적을 생성
        int randomEnemy = Random.Range(0, 2); // 큰 적, 작은 적
        int randomPoint = Random.Range(0, 5); // 스폰되는 위치
        GameObject enemy = objectManager.MakeObj(enemyObjs[randomEnemy]);
        enemy.transform.position = spawnPoints[randomPoint].position;
        enemy.transform.rotation = spawnPoints[randomPoint].rotation;

        Enemy2D enemyLogic = enemy.GetComponent<Enemy2D>();
        enemyLogic.player = player;
        enemyLogic.objectManager = objectManager;
    }

    public void UpdateLifeIcon(int life)
    {
        //#. UI Life Init Disable
        for (int index = 0; index < 3; index++)
        {
            lifeImage[index].color = new Color(1, 1, 1, 0);
        }
        //#. UI Life Active
        for (int index = 0; index < life; index++)
        {
            lifeImage[index].color = new Color(1, 1, 1, 1);
        }
    }

    public void UpdateBoomIcon(int boom)
    {
        //#. UI Boom Init Disable
        for (int index = 0; index < 3; index++)
        {
            boomImage[index].color = new Color(1, 1, 1, 0);
        }
        //#. UI Boom Active
        for (int index = 0; index < boom; index++)
        {
            boomImage[index].color = new Color(1, 1, 1, 1);
        }
    }

    public void RespawnPlayer()
    {
        Invoke("RespawnPlayerExe", 2f);
    }

    void RespawnPlayerExe()
    {
        player.transform.position = Vector3.left * 285.0f;
        player.SetActive(true);
        Player2D playerLogic = player.GetComponent<Player2D>();
        playerLogic.isHit = false;
    }

    public void GameOver()
    {
        gameOverSet.SetActive(true);
    }

    public void GameRetry()
    {
        SceneManager.LoadScene("08Proj2D");
    }
}
