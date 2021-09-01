using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
//  파일 읽기를 위한 System.IO 사용
using System.IO;
public class MazeManager : MonoBehaviour
{
    public string[] keyObjs;

    public Transform[] spawnPoints;
    public GameObject player;
    public GameObject enemy;
    public GameObject Direction;
    public bool nextSpawnDelay;
    public float DelaySpawn;
    public float curSpawnDelay;


    public Text PlayerScore;
    public Text EnemyScore;
    public GameObject gameOverSet;


    // Object Pool
    public MazeObjectManager objectManager;

    public List<Spawn> spawnList;
    public int spawnIndex;
    public bool spawnEnd;

    private void Awake()
    {
        spawnList = new List<Spawn>();
        ReadSpawnFile();
    }

    void ReadSpawnFile()
    {
        // #1. 변수 초기화
        spawnList.Clear();
        spawnIndex = 0;
        spawnEnd = false;

        // #2. 리스폰 파일 읽기
        // TextAsset : 텍스트 파일 에셋 클래스
        TextAsset textFile = Resources.Load("Stage 0") as TextAsset; // as ~  파일 검증 txt 파일이 아니면 NULL
        StringReader stringReader = new StringReader(textFile.text);  // 파일 내의 문자열 데이터 읽기

        // #.3 리스폰 데이터 생성
        while (stringReader != null)
        {
            string line = stringReader.ReadLine(); // 한줄씩 반환
            Debug.Log(line);
            if (line == null) break;

            Spawn spawnData = new Spawn();
            spawnData.delay = float.Parse(line.Split(',')[0]); // split(',') 지정한 구분 문자로 문자열을 나누는 함수
            spawnData.point = int.Parse(line.Split(',')[1]);
            spawnList.Add(spawnData);
        }

        // #.4 텍스트 파일 닫기
        stringReader.Close();

        Debug.Log(spawnList.Count / 2);
        // #.5 첫번째 스폰 딜레이 적용
        nextSpawnDelay = true;
    }

    // Update is called once per frame
    void Update()
    {
        curSpawnDelay += Time.deltaTime;

        if (curSpawnDelay > DelaySpawn && !spawnEnd && nextSpawnDelay)
        {
            SpawnKey();
            curSpawnDelay = 0;
        }

        CheckEnd();
    }

    void CheckEnd()
    {
        MazePlayer PLogic = player.GetComponent<MazePlayer>();
        EnemyMecanim ELogic = enemy.GetComponent<EnemyMecanim>();
        if((ELogic.score >= (spawnList.Count / 2 + 1)) || (PLogic.score >= (spawnList.Count / 2 + 1)))
  
        {
            GameOver();
        }
    }

    void SpawnKey()
    {
        int keyPoint = spawnList[spawnIndex].point;
        GameObject key = objectManager.MakeObj();
        key.transform.position = spawnPoints[keyPoint].position;


        Key KeyLogic = key.GetComponent<Key>();
        KeyLogic.objectManager = objectManager;

        Diretion DLogic = Direction.GetComponent<Diretion>();
        DLogic.target = key;
        EnemyMecanim ELogic = enemy.GetComponent<EnemyMecanim>();
        ELogic.Target = key;

        // #. 리스폰 인덱스 증가
        spawnIndex++;
        if (spawnIndex == spawnList.Count)
        {
            spawnEnd = true;
            return;
        }
        DelaySpawn = spawnList[0].delay;
        nextSpawnDelay = false;
    }

    public void UpdatePlayerScore(int p_s)
    {
        PlayerScore.text = "Player Score : " + p_s;
    }

    public void UpdateEnemyScore(int e_s)
    {
        EnemyScore.text = "Enemy Score : " + e_s;
    }

    public void GameOver()
    {
        gameOverSet.SetActive(true);
    }

    public void GameRetry()
    {
        SceneManager.LoadScene("10NavyMeshAgent");
    }
}
