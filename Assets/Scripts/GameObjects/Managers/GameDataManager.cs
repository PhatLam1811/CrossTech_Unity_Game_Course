using System;
using UnityEngine;

public class GameDataManager : MonoSingleton<GameDataManager>
{
    public PlayerData playerData;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OpenApp()
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
                //Create New User;
                this.CreateNewPlayer();
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

    public void ClearAllPlayerData()
    {
        PlayerPrefs.DeleteAll();
        
        this.CreateNewPlayer();
    }

    public void ResetPlayerData()
    {
        if (this.playerData != null)
        {
            this.playerData.SetDefaultPlayerData();
            this.SavePlayerData();
        }
        else
        {
            Debug.LogError("Player data not found!");
            return;
        }
    }

    private void CreateNewPlayer()
    {
        this.playerData = new PlayerData();

        this.SavePlayerData();
    }

    // ==================================================

    public void UpdatePlayerHP(float dmgTaken)
    {
        this.playerData.health -= dmgTaken;
        this.SavePlayerData();
    }

    public void UpdatePlayerScore(int point)
    {
        this.playerData.score += point;
        this.SavePlayerData();
    }

    public void UpdatePlayerSpBullet1Amt(int remainingAmount)
    {
        this.playerData.spBullet1Amt = remainingAmount;
        this.SavePlayerData();
    }

    public void UpdatePlayerSpBullet2Amt(int remainingAmount)
    {
        this.playerData.spBullet2Amt = remainingAmount;
        this.SavePlayerData();
    }

    public void UpdatePlayerHighScore()
    {
        Score currentRun = new Score();
        currentRun.score = this.playerData.score;
        currentRun.ticks = DateTime.Now.Ticks;

        this.playerData.highScores.Add(currentRun);

        this.SavePlayerData();
    }
}
