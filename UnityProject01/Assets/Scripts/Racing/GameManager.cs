using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager sInstance;
    public static GameManager Instance
    {
        get
        {
            if (sInstance == null)
            {
                GameObject newGameObj = new GameObject("_GameManager");
                sInstance = newGameObj.AddComponent<GameManager>();
            }
            return sInstance;
        }
    }
    private float GameTime;
    //public Text GameTimeText;
    public int min;
    public int sec;
    public int msec;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    private void Update()
    {
        GameTime += Time.deltaTime;
    }

    public void EndTimeChk(GameObject Car)
    {
        min = (int)GameTime / 60;
        sec = (int)GameTime % 60;
        msec = (int)(GameTime * 100 % 100);
       //GameTimeText.text = Car.name + " : " + GameTime;
        Debug.Log(Car.name + " = " + min + ":" + sec + ":" + msec);
        ChangeScene("End");

    }

    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
