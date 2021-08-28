using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScene : MonoBehaviour
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
        GUILayout.Label("R A C I N G");
        GUI.TextArea(new Rect(Screen.width / 2 - 75, Screen.height / 2 - 15, 150, 30),
            "Score = " + GameManager.Instance.min + " : " + GameManager.Instance.sec + " : " + GameManager.Instance.msec);
        if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height / 2 + 30, 100, 30), "Restart"))
        {
            // To do
            GameManager.Instance.ChangeScene("Start");
        }
    }
}
