using System;
using System.Collections.Generic;
using UnityEngine;

public class GameDataManager : MonoSingleton<GameDataManager>
{
    public PlayerData playerData;

    public void StartGame()
    {
        this.LoadPlayerData();
        GamePlayManager.Instance.StartGame();
    }

    // ==================================================

    private void LoadPlayerData()
    {
        try
        {
            if (PlayerPrefs.HasKey(GameDefine.PLAYER_INFO_DATA))
            {
                string jsonData = PlayerPrefs.GetString(GameDefine.PLAYER_INFO_DATA);
                if (!string.IsNullOrEmpty(jsonData))
                {
                    this.playerData = JsonUtility.FromJson<PlayerData>(jsonData);
                    // this.userData.ParseDataNotFirstTime();

                    Debug.Log(jsonData);
                }
                else
                {
                    Debug.LogError("CAN NOT PARSE USER DATA: " + jsonData);
                    return;
                }
            }
            else
            {
                this.CreateNewPlayerData();
            }
        }

        catch (System.Exception e)
        {
            Debug.LogException(e);
        }
    }

    private void SavePlayerData()
    {
        string jsonData = JsonUtility.ToJson(this.playerData);
        PlayerPrefs.SetString(GameDefine.PLAYER_INFO_DATA, jsonData);
    }

    private void CreateNewPlayerData()
    {
        this.playerData = new PlayerData()
        {
            score = 0,
            highScores = new List<HighScore>()
        };

        this.LoadPlayerDefaultConfig();

        this.SavePlayerData();
    }

    private void LoadPlayerDefaultConfig()
    {
        PlayerConfig playerConfig = PlayerConfig.Instance;

        this.playerData.health = playerConfig.health;

        this.playerData.currentBulletId = playerConfig.defaultBulletId;

        this.playerData.specialBullet1Id = playerConfig.specialBullet1Id;
        this.playerData.specialBullet2Id = playerConfig.specialBullet2Id;

        this.playerData.specialBullet1Amount = playerConfig.specialBullet1InitAmount;
        this.playerData.specialBullet2Amount = playerConfig.specialBullet2InitAmount;

        this.playerData.score = 0;
    }

    public void ClearAllPlayerData()
    {
        PlayerPrefs.DeleteAll();
        this.CreateNewPlayerData();
    }

    public void ResetPlayerData()
    {
        if (this.playerData != null)
        {
            this.LoadPlayerDefaultConfig();
            this.SavePlayerData();
        }
        else
        {
            Debug.LogError("Player data not found!"); return;
        }
    }

    // ==================================================

    public void UpdatePlayerHealth(float dmgTaken)
    {
        this.playerData.health -= dmgTaken;
        this.SavePlayerData();
    }

    public void UpdatePlayerScore(int point)
    {
        this.playerData.score += point;
        this.SavePlayerData();
    }

    public void UpdatePlayerSpecialBullet1Amount(int remainingAmount)
    {
        this.playerData.specialBullet1Amount = remainingAmount;
        this.SavePlayerData();
    }

    public void UpdatePlayerSpecialBullet2Amount(int remainingAmount)
    {
        this.playerData.specialBullet2Amount = remainingAmount;
        this.SavePlayerData();
    }

    public void UpdatePlayerHighScores()
    {
        HighScore thisRunScore = new HighScore()
        {
            score = this.playerData.score,
            ticks = DateTime.Now.Ticks
        };

        this.playerData.highScores.Add(thisRunScore);

        this.SavePlayerData();
    }

    // ==================================================

    public void GameOver()
    {
        this.UpdatePlayerHighScores();

        this.ResetPlayerData();
    }
}
