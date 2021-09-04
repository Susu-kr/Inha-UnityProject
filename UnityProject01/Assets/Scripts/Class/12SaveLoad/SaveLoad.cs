using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[System.Serializable]
public class SaveInformation
{
    public string name;
    public float posX;
    public float posY;
    public float posZ;
}

public class SaveLoad : MonoBehaviour
{
    public float data;

    //[Obsolete("사용가능한 기능은 TestObj2 입니다.", false)] // 
    //void TestObs()
    //{

    //}

    void Start()
    {
        //TestObs();
        
    }
    // 저장된 경로
    // 컴퓨터\HKEY_CURRENT_USER\SOFTWARE\Unity\UnityEditor\DefaultCompany\UnityProject01
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            if(PlayerPrefs.HasKey("ID"))
            {
                string getID = PlayerPrefs.GetString("ID");
                Debug.Log(string.Format("ID : {0}", getID));
            }
            else
            {
                Debug.Log("ID 없음");
            }
        }
        if(Input.GetKeyDown(KeyCode.B))
        {
            string setID = "PlayerID";
            PlayerPrefs.SetString("ID", setID);
            Debug.Log("Save ID : " + setID);
        }
        if(Input.GetKeyDown(KeyCode.C))
        {
            PlayerPrefs.SetInt("Score", 33);
            PlayerPrefs.SetFloat("Exp", 44.4f);
            int getScore = 0;
            if (PlayerPrefs.HasKey("Score"))
                getScore = PlayerPrefs.GetInt("Score");
            float getExp = 0.0f;
            if (PlayerPrefs.HasKey("Exp"))
                getExp = PlayerPrefs.GetFloat("Exp");

            Debug.Log(getScore.ToString());
            Debug.Log(getExp.ToString());
        }

        if(Input.GetKeyDown(KeyCode.D))
        {
            int getScore = PlayerPrefs.GetInt("Score", 100);
            float getExp = PlayerPrefs.GetFloat("Exp", 100.0f);
            string getID = PlayerPrefs.GetString("ID", "NONE");

            Debug.Log(getScore);
            Debug.Log(getExp);
            Debug.Log(getID);
        }

        if(Input.GetKeyDown(KeyCode.F))
        {
            PlayerPrefs.DeleteKey("ID");
            PlayerPrefs.DeleteKey("Score");
            PlayerPrefs.DeleteKey("Exp");

            //PlayerPrefs.DeleteAll();
        }

        if(Input.GetKeyDown(KeyCode.S))
        {
            SaveBinary();
        }
        if(Input.GetKeyDown(KeyCode.Z))
        {
            PlayerPrefs.Save();
        }
    }

    // 일일 미션 참여 여부정도? 최대 1MB가 넘어가면 에러 발생

    void SaveBinary()
    {
        SaveInformation setInfo = new SaveInformation();
        setInfo.name = "newUser";
        setInfo.posX = 0.0f;
        setInfo.posY = 4.5f;
        setInfo.posZ = 5.5f;

        BinaryFormatter formatter = new BinaryFormatter();
        MemoryStream memoryStream = new MemoryStream();
        formatter.Serialize(memoryStream, setInfo);
        byte[] bytes = memoryStream.GetBuffer();
        String memStr = Convert.ToBase64String(bytes);

        Debug.Log(memStr);
        PlayerPrefs.SetString("SaveInformation", memStr);

        string getInfos = PlayerPrefs.GetString("SaveInformation");
        Debug.Log(getInfos);

        byte[] getBytes = Convert.FromBase64String(getInfos);
        MemoryStream getMemStream = new MemoryStream(getBytes);

        BinaryFormatter formatter2 = new BinaryFormatter();
        SaveInformation loadInformation = (SaveInformation)formatter2.Deserialize(getMemStream);
        Debug.Log(loadInformation);
        Debug.Log(loadInformation.name);
    }
}
