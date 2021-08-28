using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionPopup : MonoBehaviour
{
    public Text titleText;
    public InputField inputText;

    public GameObject toggleObj;

    Toggle toggleTest;
    // Start is called before the first frame update
    void Start()
    {
        toggleTest = toggleObj.GetComponent<Toggle>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void onOKButton()
    {
        Debug.Log("onOKButton");
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
            titleText.text = "Toggle On";
            return;
        }
        titleText.text = "Toggle Off";
    }
}
