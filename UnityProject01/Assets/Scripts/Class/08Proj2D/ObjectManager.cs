using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    /* 
        오브젝트 풀링 -> 최적화
        : 미리 생성해둔 풀에서 활성화/비활성화
        Instantiate, Destroy 생성, 삭제시 조각난  메모리가 쌓임
        가비지컬렉트 : 쌓인 조각난 메모리를 비우는 기술
    */
    // 프리펩 변수 할당
    public GameObject enemyLPrefab;
    public GameObject enemySPrefab;
    public GameObject itemCoinPrefab;
    public GameObject itemPowerPrefab;
    public GameObject itemBoomPrefab;
    public GameObject bulletPlayerAPrefab;
    public GameObject bulletPlayerBPrefab;
    public GameObject bulletPlayerCPrefab;
    public GameObject bulletEnemyAPrefab;
    public GameObject bulletEnemyBPrefab;

    GameObject[] EnemyL;
    GameObject[] EnemyS;

    GameObject[] ItemCoin;
    GameObject[] ItemPower;
    GameObject[] ItemBoom;

    GameObject[] BulletPlayerA;
    GameObject[] BulletPlayerB;
    GameObject[] BulletPlayerC;
    GameObject[] BulletEnemyA;
    GameObject[] BulletEnemyB;

    GameObject[] targetPool;

    void Awake()
    {
        EnemyL = new GameObject[10];
        EnemyS = new GameObject[10];

        ItemCoin = new GameObject[20];
        ItemPower = new GameObject[10];
        ItemBoom = new GameObject[10];

        BulletPlayerA = new GameObject[100];
        BulletPlayerB = new GameObject[100];
        BulletPlayerC = new GameObject[100];

        BulletEnemyA = new GameObject[100];
        BulletEnemyB = new GameObject[100];

        Generate();
    }

    void Generate()
    {
        // #1.Enemy
        for(int index = 0; index < EnemyL.Length; index++)
        {
            EnemyL[index] = Instantiate(enemyLPrefab);
            EnemyL[index].SetActive(false);
        }
        for (int index = 0; index < EnemyS.Length; index++)
        {
            EnemyS[index] = Instantiate(enemySPrefab);
            EnemyS[index].SetActive(false);
        }
        // #2.Item
        for (int index = 0; index < ItemCoin.Length; index++)
        {
            ItemCoin[index] = Instantiate(itemCoinPrefab);
            ItemCoin[index].SetActive(false);
        }
        for (int index = 0; index < ItemPower.Length; index++)
        {
            ItemPower[index] = Instantiate(itemPowerPrefab);
            ItemPower[index].SetActive(false);
        }
        for (int index = 0; index < ItemBoom.Length; index++)
        {
            ItemBoom[index] = Instantiate(itemBoomPrefab);
            ItemBoom[index].SetActive(false);
        }
        // #3.Player Bullet
        for (int index = 0; index < BulletPlayerA.Length; index++)
        {
            BulletPlayerA[index] = Instantiate(bulletPlayerAPrefab);
            BulletPlayerA[index].SetActive(false);
        }
        for (int index = 0; index < BulletPlayerB.Length; index++)
        {
            BulletPlayerB[index] = Instantiate(bulletPlayerBPrefab);
            BulletPlayerB[index].SetActive(false);
        }
        for (int index = 0; index < BulletPlayerC.Length; index++)
        {
            BulletPlayerC[index] = Instantiate(bulletPlayerCPrefab);
            BulletPlayerC[index].SetActive(false);
        }
        // #4.Enemy Bullet
        for (int index = 0; index < BulletEnemyA.Length; index++)
        {
            BulletEnemyA[index] = Instantiate(bulletEnemyAPrefab);
            BulletEnemyA[index].SetActive(false);
        }
        for (int index = 0; index < BulletEnemyB.Length; index++)
        {
            BulletEnemyB[index] = Instantiate(bulletEnemyBPrefab);
            BulletEnemyB[index].SetActive(false);
        }
    }

    // # 풀 활용
    public GameObject MakeObj(string type)
    {
        switch (type)
        {
            case "EnemyL":
                targetPool = EnemyL;
                break;
            case "EnemyS":
                targetPool = EnemyS;
                break;
            case "ItemCoin":
                targetPool = ItemCoin;
                break;
            case "ItemPower":
                targetPool = ItemPower;
                break;
            case "ItemBoom":
                targetPool = ItemBoom;
                break;
            case "BulletPlayerA":
                targetPool = BulletPlayerA;
                break;
            case "BulletPlayerB":
                targetPool = BulletPlayerB;
                break;
            case "BulletPlayerC":
                targetPool = BulletPlayerC;
                break;
            case "BulletEnemyA":
                targetPool = BulletEnemyA;
                break;
            case "BulletEnemyB":
                targetPool = BulletEnemyB;
                break;
        }

        for (int index = 0; index < targetPool.Length; index++)
        {
            if (!targetPool[index].activeSelf)
            {
                targetPool[index].SetActive(true);
                return targetPool[index];
            }
        }
        return null;
    }

    public GameObject[] GetPool(string type)
    {
        switch (type)
        {
            case "EnemyL":
                targetPool = EnemyL;
                break;
            case "EnemyS":
                targetPool = EnemyS;
                break;
            case "ItemCoin":
                targetPool = ItemCoin;
                break;
            case "ItemPower":
                targetPool = ItemPower;
                break;
            case "ItemBoom":
                targetPool = ItemBoom;
                break;
            case "BulletPlayerA":
                targetPool = BulletPlayerA;
                break;
            case "BulletPlayerB":
                targetPool = BulletPlayerB;
                break;
            case "BulletPlayerC":
                targetPool = BulletPlayerC;
                break;
            case "BulletEnemyA":
                targetPool = BulletEnemyA;
                break;
            case "BulletEnemyB":
                targetPool = BulletEnemyB;
                break;
        }
        return targetPool;
    }
}
