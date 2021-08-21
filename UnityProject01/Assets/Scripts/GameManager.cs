using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager sInstance;
    public static GameManager Instance
    {
        get
        {
            if(sInstance == null)
            {
                GameObject newGameObj = new GameObject("_GameManager");
                sInstance = newGameObj.AddComponent<GameManager>();
            }
            return sInstance;
        }
    }

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    //
    public long gameScore;
    public float gameStartTime;
    public float gameEndTime;
    public string playerName;
}
