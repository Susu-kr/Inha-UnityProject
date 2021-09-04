using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameTitle : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoNextScene()
    {
        GameManager.Instance.nextSceneName = "10NavyMeshAgent";
        SceneManager.LoadScene("14Loading");
    }

}
