using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseEnd : MonoBehaviour
{
    Animation anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponentInChildren<Animation>();
        anim.wrapMode = WrapMode.Once;
        anim.CrossFade("diehard", 0.3f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnGUI()
    {
        GUI.Label(new Rect(Screen.width / 8, 2 * Screen.height / 5, 100, 100), "Score : " + DefenseGameManager.Instance.score);
        if (GUI.Button(new Rect(Screen.width / 8, 3 * Screen.height / 5, 100, 30), "ReStart"))
        {
            // To do
            DefenseGameManager.Instance.ChangeScene("Defense_Start");
        }
    }
}
