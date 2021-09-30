using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeObjectManager : MonoBehaviour
{
    public GameObject KeyPrefab;
    GameObject[] Key;

    GameObject[] targetPool;

    private void Awake()
    {
        Key = new GameObject[5];

        Generate();
    }

    void Generate()
    {
        for(int i = 0; i < Key.Length; i++)
        {
            Key[i] = Instantiate(KeyPrefab);
            Key[i].SetActive(false);
        }
    }

    public GameObject MakeObj()
    {
        targetPool = Key;
        for(int i = 0; i <targetPool.Length; i++)
        {
            if(!targetPool[i].activeSelf)
            {
                targetPool[i].SetActive(true);
                return targetPool[i];
            }
        }
        return null;
    }

    public GameObject[] GetPool(string type)
    {
        targetPool = Key;
        return targetPool;
    }
}
