using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseStart : MonoBehaviour
{
    Animation anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponentInChildren<Animation>();
        anim.wrapMode = WrapMode.Loop;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnGUI()
    {
        GUI.Label(new Rect(Screen.width / 8,  2 * Screen.height / 5, 100, 100), "Defense Game");
        if (GUI.Button(new Rect(Screen.width / 8, 3 * Screen.height / 5, 100, 30), "Start"))
        {
            // To do
            StartCoroutine("StartGame");

        }
    }

    IEnumerator StartGame()
    {
        anim.CrossFade("victory", 0.3f);
        float delayTime = anim.GetClip("victory").length - 0.3f;
        yield return new WaitForSeconds(delayTime);
        DefenseGameManager.Instance.ChangeScene("Defense_Game");

    }
}
