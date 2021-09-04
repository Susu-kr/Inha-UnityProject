using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable] // #. ����ȭ
public class GameData
{
    // #. �÷��̾��� Key ����
    public int playerScore;
    public float player_x;
    public float player_z;

    // #. ���� Key ����
    public int enemyScore;
    public float enemy_x;
    public float enemy_z;

    // #. ���� Key�� ������
    public int spawnIndex;
    public bool spawnEnd;
    public bool chk = false;
}


