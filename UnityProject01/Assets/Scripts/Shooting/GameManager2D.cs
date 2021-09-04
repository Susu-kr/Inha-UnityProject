using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//  ���� �б⸦ ���� System.IO ���
using System.IO;

public class GameManager2D : MonoBehaviour
{
    // �������� ����
    public int stage;
    public Animator stageAnim;
    public Animator clearAnim;
    public Animator fadeAnim;
    public Transform playerPos;

    // �� ����
    public string[] enemyObjs;
    public Transform[] spawnPoints;
    public float nextSpawnDelay;
    public float curSpawnDelay;

    public GameObject player;

    // UI
    public Text scoreText;
    public Image[] lifeImage;
    public Image[] boomImage;
    public GameObject gameOverSet;

    // Object Pool
    public ObjectManager objectManager;

    // �� ������ ���õ� ���� ����
    public List<Spawn> spawnList;
    public int spawnIndex;
    public bool spawnEnd;

    private void Awake()
    {
        enemyObjs = new string[] { "EnemyS", "EnemyL", "EnemyB" };
        spawnList = new List<Spawn>();
        StageStart();
    }
    
    public void StageStart()
    {
        // #.Stage UI Load
        stageAnim.SetTrigger("On");
        stageAnim.GetComponent<Text>().text = "Stage " + stage + "\nStart";
        clearAnim.GetComponent<Text>().text = "Stage " + stage + "\nClear!!";

        // #.Enemy Spawn File Read
        ReadSpawnFile();

        // #.Fade In
        fadeAnim.SetTrigger("In");
    }

    public void StageEnd()
    {
        // #. Clear UI Load
        clearAnim.SetTrigger("On");
        
        // #.Fade Out
        fadeAnim.SetTrigger("Out");

        // #.Player Reposition
        player.transform.position = playerPos.position;

        // #. Stage Increament
        stage++;
        if (stage > 2)
            Invoke("GameOver", 6);
        else
        {
            Invoke("StageStart", 5);
        }
    }

    void ReadSpawnFile()
    {
        // #1. ���� �ʱ�ȭ
        spawnList.Clear();
        spawnIndex = 0;
        spawnEnd = false;

        // #2. ������ ���� �б�
        // TextAsset : �ؽ�Ʈ ���� ���� Ŭ����
        TextAsset textFile = Resources.Load("Stage " + stage) as TextAsset; // as ~  ���� ���� txt ������ �ƴϸ� NULL
        StringReader stringReader = new StringReader(textFile.text);  // ���� ���� ���ڿ� ������ �б�

        // #.3 ������ ������ ����
        while(stringReader != null)
        {
            string line = stringReader.ReadLine(); // ���پ� ��ȯ
            Debug.Log(line);
            if (line == null) break;

            Spawn spawnData = new Spawn();
            spawnData.delay = float.Parse(line.Split(',')[0]); // split(',') ������ ���� ���ڷ� ���ڿ��� ������ �Լ�
            spawnData.type = line.Split(',')[1];
            spawnData.point = int.Parse(line.Split(',')[2]);
            spawnList.Add(spawnData);
        }

        // #.4 �ؽ�Ʈ ���� �ݱ�
        stringReader.Close();

        // #.5 ù��° ���� ������ ����
        nextSpawnDelay = spawnList[0].delay;
    }

    // Update is called once per frame
    void Update()
    {
        curSpawnDelay += Time.deltaTime;

        if(curSpawnDelay > nextSpawnDelay && !spawnEnd)
        {
            SpawnEnemy();
            curSpawnDelay = 0;
        }

        //#.UI Score Update
        Player2D playerLogic = player.GetComponent<Player2D>();
        scoreText.text = string.Format("{0:n0}",playerLogic.score); // 3�ڸ����� �����ִ� ���
    }

    void SpawnEnemy()
    {
        int enemyIndex = 0;
        switch(spawnList[spawnIndex].type)
        {
            case "S":
                enemyIndex = 0;
                break;
            case "L":
                enemyIndex = 1;
                break;
            case "B":
                enemyIndex = 2;
                break;
        }

        int enemyPoint = spawnList[spawnIndex].point;
        Debug.Log(enemyObjs[enemyIndex]);
        GameObject enemy = objectManager.MakeObj(enemyObjs[enemyIndex]);
        enemy.transform.position = spawnPoints[enemyPoint].position;

        Rigidbody2D rigid = enemy.GetComponent<Rigidbody2D>();
        Enemy2D enemyLogic = enemy.GetComponent<Enemy2D>();
        enemyLogic.player = player;
        enemyLogic.objectManager = objectManager;
        enemyLogic.gameManager = this;
        rigid.velocity = new Vector2(enemyLogic.speed * (-1), 0);

        // #. ������ �ε��� ����
        spawnIndex++;
        if(spawnIndex == spawnList.Count)
        {
            spawnEnd = true;
            return;
        }

        // #. ���� ������ ������ ����
        nextSpawnDelay = spawnList[spawnIndex].delay;
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

    public void CallExplosion(Vector3 pos, string type)
    {
        GameObject explosion = objectManager.MakeObj(type);
        Explosion explosionLogin = explosion.GetComponent<Explosion>();

        explosion.transform.position = pos;
        explosionLogin.StartExplosion();
    }

    public void GameOver()
    {
        gameOverSet.SetActive(true);
    }

    public void GameRetry()
    {
        SceneManager.LoadScene("ShootingGame");
    }
}
