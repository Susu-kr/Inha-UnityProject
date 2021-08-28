using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUI_Test : MonoBehaviour
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
        GUI.TextArea(new Rect(200, 50, 100, 30), "Text test");
        GUI.TextField(new Rect(200, 100, 100, 30), "Text test2");
        GUI.Box(new Rect(200, 150, 100, 30), "Text test3");

        GUILayout.Label("Click Button");
        if (GUI.Button(new Rect(200, 200, 50, 30), "¹ß»ç"))
        {
            // To do
            Debug.Log("¾À ÀüÈ¯");
            GameManager2.Instance.ChangeScene("02Ray");
        }

    }
}