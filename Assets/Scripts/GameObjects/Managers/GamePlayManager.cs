using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayManager : MonoSingleton<GamePlayManager>
{
    [SerializeField] private GameObject pfBackground;
    [SerializeField] private GameObject pfPlayer;

    private GameObject background1;
    private GameObject background2;

    private GameObject player;

    private bool isPlaying = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.ProcessInput(out Vector3 movingVector);

        if (this.isPlaying)
        {
            float elapsedTime = Time.deltaTime;

            this.player.GetComponent<Player>().Move(elapsedTime, movingVector);
        }
    }

    public void StartGame()
    {
        // backgrounds
        this.background1 = Instantiate(this.pfBackground, Vector3.zero, Quaternion.identity);
        Vector3 bg2InitCoord = this.background1.GetComponent<Background>().GetReloadCoord();
        
        this.background2 = Instantiate(this.pfBackground, bg2InitCoord / 2, Quaternion.identity);

        // player
        this.player = Instantiate(this.pfPlayer, PlayerData.Instance.position, Quaternion.identity);

        // others
        EnemyManager.Instance.StartGame();
        BulletManager.Instance.StartGame();

        GameUIManager.Instance.StartGame(
            PlayerData.Instance.health,
            PlayerData.Instance.score,
            PlayerData.Instance.spBullet1Amt,
            PlayerData.Instance.spBullet2Amt);

        this.isPlaying = true;
    }

    // ==================================================

    public void ProcessInput(out Vector3 movingVector)
    {
        // get normalized moving input directions
        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");

        movingVector = new Vector3(x: hor, y: ver);

        if (Input.GetKeyDown(KeyCode.C))
        {
            Debug.Log("Clear all data!!!!");

            GameDataManager.Instance.ClearAllPlayerData();

            this.GameOver();
        }
    }

    public int GetPlayerCurrentBullet()
    {
        return PlayerData.Instance.currentBullet;
    }

    // ==================================================

    public void OnPlayerCollidedWithBullet(Player player, EnemyBullet enemyBullet, float dmgTaken)
    {
        player.OnCollidedWithBullet(enemyBullet, dmgTaken);
    }

    public void OnPlayerTakenDamage(float dmgTaken)
    {
        GameDataManager.Instance.UpdatePlayerHP(dmgTaken);

        GameUIManager.Instance.OnPlayerHealthChange(PlayerData.Instance.health);

        if (PlayerData.Instance.health <= 0f) this.GameOver();
    }

    public void OnInvokeSpecialAtk1()
    {
        this.player.GetComponent<Player>().InvokeSpecialAtk(PlayerData.Instance.spBullet1Type);

        int remainingAmt = PlayerData.Instance.spBullet1Amt - 1;

        GameUIManager.Instance.OnOutOfSpBullets1(remainingAmt);

        GameDataManager.Instance.UpdatePlayerSpBullet1Amt(remainingAmt);
    }

    public void OnInvokeSpecialAtk2()
    {
        this.player.GetComponent<Player>().InvokeSpecialAtk(PlayerData.Instance.spBullet2Type);

        int remainingAmt = PlayerData.Instance.spBullet2Amt - 1;

        GameUIManager.Instance.OnOutOfSpBullets2(remainingAmt);

        GameDataManager.Instance.UpdatePlayerSpBullet2Amt(remainingAmt);
    }

    public void OnDefeatEnemy(int point)
    {
        GameDataManager.Instance.UpdatePlayerScore(point);

        GameUIManager.Instance.OnPlayerScoreChange(PlayerData.Instance.score);
    }

    // ==================================================

    public void GameOver()
    {
        GameDataManager.Instance.UpdatePlayerHighScore();
        GameDataManager.Instance.ResetPlayerData();

        EnemyManager.Instance.GameOver();
        BulletManager.Instance.GameOver();
        this.player.GetComponent<Player>().GameOver();

        this.isPlaying = false;

        int count = 0;

        Debug.Log("Game Over!!! Here are your results!!!");

        foreach (int score in PlayerData.Instance.highScores)
        {
            Debug.Log(String.Format("{0}: {1}", ++count, score));
        }
    }
}
