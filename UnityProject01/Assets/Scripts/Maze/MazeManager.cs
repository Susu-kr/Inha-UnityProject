using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
//  ���� �б⸦ ���� System.IO ���
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
    public GameObject dataController;
    // Object Pool
    public MazeObjectManager objectManager;

    public List<Spawn> spawnList;
    public int spawnIndex;
    public bool spawnEnd;

    public bool loadchk;

    private void Awake()
    {
        spawnList = new List<Spawn>();
        ReadSpawnFile();
    }

    public void ReadSpawnFile()
    {
        // #1. ���� �ʱ�ȭ
        spawnList.Clear();
        spawnIndex = 0;
        spawnEnd = false;

        // #2. ������ ���� �б�
        // TextAsset : �ؽ�Ʈ ���� ���� Ŭ����
        TextAsset textFile = Resources.Load("Stage 0") as TextAsset; // as ~  ���� ���� txt ������ �ƴϸ� NULL
        StringReader stringReader = new StringReader(textFile.text);  // ���� ���� ���ڿ� ������ �б�

        // #.3 ������ ������ ����
        while (stringReader != null)
        {
            string line = stringReader.ReadLine(); // ���پ� ��ȯ
            if (line == null) break;

            Spawn spawnData = new Spawn();
            spawnData.delay = float.Parse(line.Split(',')[0]); // split(',') ������ ���� ���ڷ� ���ڿ��� ������ �Լ�
            spawnData.point = int.Parse(line.Split(',')[1]);
            spawnList.Add(spawnData);
        }

        // #.4 �ؽ�Ʈ ���� �ݱ�
        stringReader.Close();

        //Debug.Log(spawnList.Count / 2);
        // #.5 ù��° ���� ������ ����
        nextSpawnDelay = true;
        loadchk = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!loadchk) ReadSpawnFile();
        curSpawnDelay += Time.deltaTime;

        if (curSpawnDelay > DelaySpawn && !spawnEnd && nextSpawnDelay)
        {
            SpawnKey();
            curSpawnDelay = 0;
        }
    }

    public void SpawnKey()
    {
        Debug.Log(spawnList[spawnIndex]);
        int keyPoint = spawnList[spawnIndex].point;
        GameObject key = objectManager.MakeObj();
        key.transform.position = spawnPoints[keyPoint].position;

        Key KeyLogic = key.GetComponent<Key>();
        KeyLogic.objectManager = objectManager;

        Diretion DLogic = Direction.GetComponent<Diretion>();
        DLogic.target = key;
        EnemyMecanim ELogic = enemy.GetComponent<EnemyMecanim>();
        ELogic.Target = key;

        // #. ������ �ε��� ����
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
        if (p_s >= (spawnList.Count / 2 + 1))
            GameOver();
    }

    public void UpdateEnemyScore(int e_s)
    {
        EnemyScore.text = "Enemy Score : " + e_s;
        if (e_s >= (spawnList.Count / 2 + 1))
            GameOver();
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
