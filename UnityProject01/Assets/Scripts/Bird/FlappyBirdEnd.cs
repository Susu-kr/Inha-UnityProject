using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlappyBirdEnd : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(Screen.width / 2 - 20, Screen.height / 2 - 75, 100, 30), "Score : " + FlappyBird.Instance.score);
        if (GUI.Button(new Rect(Screen.width / 2 - 40, Screen.height / 2 + 100, 100, 30), "Restart"))
        {
            // To do
            FlappyBird.Instance.ChangeScene("04-01 Flappy Bird Start");
        }
    }
}
