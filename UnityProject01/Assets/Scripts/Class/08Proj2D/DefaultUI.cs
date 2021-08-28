using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DefaultUI : MonoBehaviour
{
    public Text txtScore;
    public Image imgHPBar;

    public GameObject popupObj;


    // Start is called before the first frame update
    void Start()
    {
        ShowScore(100);
        ShowHPBar(50);
        popupObj.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowScore(int score)
    {
        txtScore.text = "Score : <color=#0000ff> " + score.ToString() + "</color>";
    }

    public void ShowHPBar(int hp)
    {
        imgHPBar.fillAmount = (float)hp / (float)100;
    }

    void onOptionButton()
    {
        popupObj.SetActive(!popupObj.activeSelf);
        if (popupObj.activeSelf) Time.timeScale = 0.0f;
        else Time.timeScale = 1.0f;
    }
}
