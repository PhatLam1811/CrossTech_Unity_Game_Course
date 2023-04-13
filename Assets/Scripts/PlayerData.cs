using System.Collections.Generic;

[System.Serializable]
public class PlayerData
{
    public float health;

    public int currentBulletId;

    public int specialBullet1Id;
    public int specialBullet2Id;

    public int specialBullet1Amount;
    public int specialBullet2Amount;

    public int score;
    public List<HighScore> highScores;

    public static PlayerData Instance => GameDataManager.Instance.playerData;
}

[System.Serializable]
public class HighScore
{
    public int score;
    public long ticks;
}
