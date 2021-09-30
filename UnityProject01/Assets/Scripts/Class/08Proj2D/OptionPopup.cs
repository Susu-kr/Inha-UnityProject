using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionPopup : MonoBehaviour
{
    public Text titleText;
    public InputField inputText;

    public GameObject toggleObj;
    public GameObject Sound;

    public AudioSource BG;
    Toggle toggleTest;
    // Start is called before the first frame update
    void Start()
    {
        toggleTest = toggleObj.GetComponent<Toggle>();
        BG = Sound.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onCloseButton()
    {
        Time.timeScale = 1.0f;
        gameObject.SetActive(false);
    }

    void onOKButton()
    {
        Time.timeScale = 1.0f;
        gameObject.SetActive(false);
    }

    public void onTextChanged()
    {
        titleText.text = inputText.text;
    }

    public void onTextEditEnd()
    {
        titleText.text = inputText.text;
    }

    public void onToggleTest()
    {
        if(toggleTest.isOn)
        {
            BG.Play();

            return;
        }
        BG.Stop();
    }
}
