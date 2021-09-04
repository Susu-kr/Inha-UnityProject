using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO;
using LitJson;
using System;

public class DataController : MonoBehaviour
{
    // #. �̱������� ����
    static GameObject _container;
    static GameObject Container
    {
        get
        {
            return _container;
        }
    }

    static DataController _instance;
    public static DataController Instance
    {
        get
        {
            if(!_instance)
            {
                _container = new GameObject();
                _container.name = "DataController";
                _instance = _container.AddComponent(typeof(DataController)) as DataController;
                DontDestroyOnLoad(_container);
            }
            return _instance;
        }
    }

    // #. ���� ������ �����̸� ����
    string GameDataFileName = "/Resources/JSONData/MazeData.json";

    // #. "���ϴ� �̸�(����).json"
    public GameData _gameData;
    public GameData gameData
    {
        get
        {
            // #. ������ ���۵Ǹ� �ڵ����� ����ǵ���
            if(_gameData == null)
            {
                LoadGameData();
                SaveGameData();
            }
            return _gameData;
        }
    }

    // #. GameData�� ������ ������
    public MazeManager mazeManager;
    public GameObject player;
    public GameObject enemy;


    void Start()
    {
        LoadGameData();
        SaveGameData();
    }

    // #. ����� ���� �ҷ�����
    public void LoadGameData()
    {
        string filePath = Application.dataPath + GameDataFileName;

        // #. ����� ������ �����ϴ°�?
        if(File.Exists(filePath))
        {
            print("�ҷ����� ����");
            string FromJsonData = File.ReadAllText(filePath);
            _gameData = JsonUtility.FromJson<GameData>(FromJsonData);
            LoadData();
        }

        // #. ����� ������ ���ٸ�
        else
        {
            print("���ο� ���� ����");
            _gameData = new GameData();
            mazeManager.loadchk = false;
        }
    }
    
    // #. ���� �����ϱ�
    public void SaveGameData()
    {
        SaveData();
        string ToJsonData = JsonUtility.ToJson(gameData);
        string filePath = Application.dataPath + GameDataFileName;


        // #. �̹� ����� ������ �ִٸ� �����
        File.WriteAllText(filePath, ToJsonData);

        // #. �ùٸ��� ����ƴ��� Ȯ��(�����Ӱ� ����)

    }

    // #. ������ �����ϸ� �ڵ�����ǵ���
    private void OnApplicationQuit()
    {
        SaveGameData();
    }


    public void SaveData()
    {
        MazePlayer p_s = player.GetComponent<MazePlayer>();
        gameData.playerScore = p_s.score;
        gameData.player_x = player.transform.position.x;
        gameData.player_z = player.transform.position.z;
        EnemyMecanim e_s = enemy.GetComponent<EnemyMecanim>();
        gameData.enemyScore = e_s.score;
        gameData.enemy_x = enemy.transform.position.x;
        gameData.enemy_z = enemy.transform.position.z;

        MazeManager Key = mazeManager.GetComponent<MazeManager>();
        gameData.spawnIndex = Key.spawnIndex - 1;
        gameData.spawnEnd = Key.spawnEnd;

        gameData.chk = true;

        Debug.Log(gameData);
    }

    public void LoadData()
    {
        MazePlayer p_s = player.GetComponent<MazePlayer>();
        p_s.score = gameData.playerScore;
        player.transform.position = new Vector3(gameData.player_x, 0.5f, gameData.player_z);

        EnemyMecanim e_s = enemy.GetComponent<EnemyMecanim>();
        e_s.score = gameData.enemyScore;
        enemy.transform.position = new Vector3(gameData.enemy_x, 0.5f, gameData.enemy_z);

        MazeManager Key = mazeManager.GetComponent<MazeManager>();
        Key.spawnIndex = gameData.spawnIndex;
        Key.spawnEnd = gameData.spawnEnd;

        Key.loadchk = gameData.chk;

        Key.UpdatePlayerScore(p_s.score);
        Key.UpdateEnemyScore(e_s.score);
    }

}
