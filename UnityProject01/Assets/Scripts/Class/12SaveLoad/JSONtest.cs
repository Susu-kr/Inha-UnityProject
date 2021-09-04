using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using LitJson;
using System;

public class PlayerInfo
{
    public int ID;
    public string Name;
    public double Gold;

    public PlayerInfo(int id, string name, double gold)
    {
        ID = id;
        Name = name;
        Gold = gold;
    }
}

public class JSONtest : MonoBehaviour
{
    public List<PlayerInfo> playerInfoList = new List<PlayerInfo>();

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start");
        SavePlayerInfo();
        LoadPlayerInfo();
    }


    // Update is called once per frame
    void Update()
    {

    }
    private void SavePlayerInfo()
    {
        playerInfoList.Add(new PlayerInfo(1, "Name #1", 10001));
        playerInfoList.Add(new PlayerInfo(2, "Name #2", 10002));
        playerInfoList.Add(new PlayerInfo(3, "Name #3", 10003));
        playerInfoList.Add(new PlayerInfo(4, "Name #4", 10004));
        playerInfoList.Add(new PlayerInfo(5, "Name #5", 10005));

        JsonData infoJson = JsonMapper.ToJson(playerInfoList);
        File.WriteAllText(Application.dataPath + "/Resources/JSONData/PlayerInfoData.json", infoJson.ToString());
    }

    public void LoadPlayerInfo()
    {
        if(File.Exists(Application.dataPath + "/Resources/JSONData/PlayerInfoData.json"))
        {
            string jsonString = File.ReadAllText(Application.dataPath + "/Resources/JSONData/PlayerInfoData.json");
            Debug.Log(jsonString);

            JsonData playerData = JsonMapper.ToObject(jsonString);
            ParsingJsonPlayerInfo(playerData);
        }
    }

    private void ParsingJsonPlayerInfo(JsonData data)
    {
        for (int i = 0; i < data.Count; i++)
        {
            Debug.Log(data[i]["ID"].ToString() + " , " + data[i]["Name"] + " , " + data[i]["Gold"].ToString());
            int id = (int)data[i]["ID"];
            Debug.Log(id.ToString());
        }
    }
}