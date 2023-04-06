using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public Vector3 position;

    public float health;
    public int currentBullet;

    public int spBullet1Type;
    public int spBullet2Type;

    public int spBullet1Amt;
    public int spBullet2Amt;

    public int score;               // current score
    public List<int> highScores;    // overall high scores

    public static PlayerData Instance => GameDataManager.Instance.playerData;

    public PlayerData()
    {
        this.SetDefaultPlayerData();

        this.highScores = new List<int>();
    }

    public void SetDefaultPlayerData()
    {
        this.position = Vector3.zero;

        this.health = GameDefine.DEFAULT_PLAYER_HP;
        this.currentBullet = GameDefine.DEFAULT_BULLET_TYPE;

        this.spBullet1Type = GameDefine.HOMING_BULLET_TYPE;
        this.spBullet2Type = GameDefine.BUCKSHOT_BULLET_TYPE;

        this.spBullet1Amt = GameDefine.DEFAULT_PLAYER_SP_BULLET_1_AMT;
        this.spBullet2Amt = GameDefine.DEFAULT_PLAYER_SP_BULLET_2_AMT;

        this.score = GameDefine.DEFAULT_PLAYER_SCORE;
    }
}
