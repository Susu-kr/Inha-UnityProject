using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable] // #. 직렬화
public class GameData
{
    // #. 플레이어의 Key 개수
    public int playerScore;
    public float player_x;
    public float player_z;

    // #. 적의 Key 개수
    public int enemyScore;
    public float enemy_x;
    public float enemy_z;

    // #. 남은 Key의 정보들
    public int spawnIndex;
    public bool spawnEnd;
    public bool chk = false;
}


