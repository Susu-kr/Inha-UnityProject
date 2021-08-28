using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DefenseGameManager : MonoBehaviour
{
    private static DefenseGameManager sInstance;
    public static DefenseGameManager Instance
    {
        get
        {
            if (sInstance == null)
            {
                GameObject newGameObj = new GameObject("_DefenseGameManager");
                sInstance = newGameObj.AddComponent<DefenseGameManager>();
            }
            return sInstance;
        }
    }

    public bool isDeath = false;
    public int score = 0;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
