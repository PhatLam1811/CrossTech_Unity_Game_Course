using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayManager : MonoSingleton<GamePlayManager>
{
    [SerializeField] private GameObject pfBackground;
    [SerializeField] private GameObject pfPlayer;

    private GameObject player;

    private bool isGameOver;

    public delegate void GameOverCallback();
    public event GameOverCallback onGameOverCallback;

    public delegate void ReplayGameCallback();
    public event ReplayGameCallback onGameReplayCallback;

    // Start is called before the first frame update
    void Start()
    {
        this.isGameOver = true;
    }

    // Update is called once per frame
    void Update()
    {
        this.ProcessInput(out Vector3 movingVector);

        if (!this.isGameOver)
        {
            float elapsedTime = Time.deltaTime;

            this.player.GetComponent<Player>().Move(elapsedTime, movingVector);
        }
    }

    public void StartGame()
    {
        // background
        Instantiate(this.pfBackground, Vector3.zero, Quaternion.identity);

        // player
        this.player = Instantiate(this.pfPlayer, Vector3.zero, Quaternion.identity);

        // others
        EnemyManager.Instance.StartGame();  // enemy
        BulletManager.Instance.StartGame(); // bullet
        GameUIManager.Instance.StartGame(   // UI elements
            PlayerData.Instance.health,
            PlayerData.Instance.score,
            PlayerData.Instance.spBullet1Amt,
            PlayerData.Instance.spBullet2Amt);

        // Add callback on game over for GameDataManager & GameManager
        this.onGameOverCallback += GameDataManager.Instance.GameOver;
        this.onGameOverCallback += GameManager.Instance.GameOver;

        this.isGameOver = false;
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
            this.ClearAllGameData();
        }
    }

    private void ClearAllGameData()
    {
        Debug.Log("Clear all data!!!!");

        GameDataManager.Instance.ClearAllPlayerData();

        this.GameOver();
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
        this.isGameOver = true;

        // invoke GameOver() for all game objects
        this.onGameOverCallback?.Invoke();

        Debug.Log("Game Over!!!");
    }

    public void ReplayGame()
    {
        this.onGameReplayCallback?.Invoke();

        this.StartGame();

        Debug.Log("Replay Game!!!");
    }
}
