using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//  파일 읽기를 위한 System.IO 사용
using System.IO;

public class GameManager2D : MonoBehaviour
{
    // 스테이지 관리
    public int stage;
    public Animator stageAnim;
    public Animator clearAnim;
    public Animator fadeAnim;
    public Transform playerPos;

    // 적 생성
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

    // 적 출현에 관련된 변수 생성
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
        // #1. 변수 초기화
        spawnList.Clear();
        spawnIndex = 0;
        spawnEnd = false;

        // #2. 리스폰 파일 읽기
        // TextAsset : 텍스트 파일 에셋 클래스
        TextAsset textFile = Resources.Load("Stage " + stage) as TextAsset; // as ~  파일 검증 txt 파일이 아니면 NULL
        StringReader stringReader = new StringReader(textFile.text);  // 파일 내의 문자열 데이터 읽기

        // #.3 리스폰 데이터 생성
        while(stringReader != null)
        {
            string line = stringReader.ReadLine(); // 한줄씩 반환
            Debug.Log(line);
            if (line == null) break;

            Spawn spawnData = new Spawn();
            spawnData.delay = float.Parse(line.Split(',')[0]); // split(',') 지정한 구분 문자로 문자열을 나누는 함수
            spawnData.type = line.Split(',')[1];
            spawnData.point = int.Parse(line.Split(',')[2]);
            spawnList.Add(spawnData);
        }

        // #.4 텍스트 파일 닫기
        stringReader.Close();

        // #.5 첫번째 스폰 딜레이 적용
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
        scoreText.text = string.Format("{0:n0}",playerLogic.score); // 3자리마다 끊어주는 방법
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

        // #. 리스폰 인덱스 증가
        spawnIndex++;
        if(spawnIndex == spawnList.Count)
        {
            spawnEnd = true;
            return;
        }

        // #. 다음 리스폰 딜레이 갱신
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
