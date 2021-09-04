using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO;
using LitJson;
using System;

public class DataController : MonoBehaviour
{
    // #. 싱글톤으로 선언
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

    // #. 게임 데이터 파일이름 설정
    string GameDataFileName = "/Resources/JSONData/MazeData.json";

    // #. "원하는 이름(영문).json"
    public GameData _gameData;
    public GameData gameData
    {
        get
        {
            // #. 게임이 시작되면 자동으로 실행되도록
            if(_gameData == null)
            {
                LoadGameData();
                SaveGameData();
            }
            return _gameData;
        }
    }

    // #. GameData에 저장할 데이터
    public MazeManager mazeManager;
    public GameObject player;
    public GameObject enemy;


    void Start()
    {
        LoadGameData();
        SaveGameData();
    }

    // #. 저장된 게임 불러오기
    public void LoadGameData()
    {
        string filePath = Application.dataPath + GameDataFileName;

        // #. 저장된 게임이 존재하는가?
        if(File.Exists(filePath))
        {
            print("불러오기 성공");
            string FromJsonData = File.ReadAllText(filePath);
            _gameData = JsonUtility.FromJson<GameData>(FromJsonData);
            LoadData();
        }

        // #. 저장된 게임이 없다면
        else
        {
            print("새로운 파일 생성");
            _gameData = new GameData();
            mazeManager.loadchk = false;
        }
    }
    
    // #. 게임 저장하기
    public void SaveGameData()
    {
        SaveData();
        string ToJsonData = JsonUtility.ToJson(gameData);
        string filePath = Application.dataPath + GameDataFileName;


        // #. 이미 저장된 파일이 있다면 덮어쓰기
        File.WriteAllText(filePath, ToJsonData);

        // #. 올바르게 저장됐는지 확인(자유롭게 변형)

    }

    // #. 게임을 종료하면 자동저장되도록
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
