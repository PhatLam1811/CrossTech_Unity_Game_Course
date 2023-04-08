using System.Collections.Generic;
using System;

[Serializable]
public class PlayerData
{
    public float health;
    public int currentBullet;

    public int spBullet1Type;
    public int spBullet2Type;

    public int spBullet1Amt;
    public int spBullet2Amt;

    public int score;               // current score
    public List<Score> highScores;    // overall high scores

    public static PlayerData Instance => GameDataManager.Instance.playerData;

    public PlayerData()
    {
        this.SetDefaultPlayerData();

        this.highScores = new List<Score>();
    }

    public void SetDefaultPlayerData()
    {
        this.health = GameDefine.DEFAULT_PLAYER_HP;
        this.currentBullet = GameDefine.DEFAULT_BULLET_TYPE;

        this.spBullet1Type = GameDefine.HOMING_BULLET_TYPE;
        this.spBullet2Type = GameDefine.BUCKSHOT_BULLET_TYPE;

        this.spBullet1Amt = GameDefine.DEFAULT_PLAYER_SP_BULLET_1_AMT;
        this.spBullet2Amt = GameDefine.DEFAULT_PLAYER_SP_BULLET_2_AMT;

        this.score = GameDefine.DEFAULT_PLAYER_SCORE;
    }
}

[Serializable]
public class Score
{
    public int score;
    public long ticks;
}
