using UnityEngine;

public class GamePlayManager : MonoSingleton<GamePlayManager>
{
    [SerializeField] private GameObject pfBackground;
    [SerializeField] private GameObject pfPlayer;

    public delegate void GameOverCallback();
    public delegate void ReplayGameCallback();

    public event GameOverCallback onGameOverCallback;
    public event ReplayGameCallback onGameReplayCallback;

    private Camera viewport;
    private Player player;

    private bool isGameOver;

    // ==================================================

    void Start()
    {
        this.isGameOver = true;
    }

    void Update()
    {
        this.ProcessInput(out Vector3 movingVector);

        if (!this.isGameOver)
        {
            float elapsedTime = Time.deltaTime;

            this.player.OnPlayerMove(elapsedTime, movingVector);
        }
    }

    public void StartGame()
    {
        // background
        Instantiate(this.pfBackground, Vector3.zero, Quaternion.identity);

        // viewport
        this.viewport = Camera.main;

        // player
        this.player = Instantiate(this.pfPlayer, Vector3.zero, Quaternion.identity).GetComponent<Player>();

        // others
        EnemyManager.Instance.StartGame();  // enemy
        BulletManager.Instance.StartGame(); // bullet
        GameUIManager.Instance.StartGame(   // UI elements
            PlayerData.Instance.health,
            PlayerData.Instance.score,
            PlayerData.Instance.specialBullet1Amount,
            PlayerData.Instance.specialBullet2Amount);

        // game over callback for GameDataManager
        this.onGameOverCallback -= GameDataManager.Instance.GameOver; // prevent duplicates
        this.onGameOverCallback += GameDataManager.Instance.GameOver;

        // game over callback for GameManager
        this.onGameOverCallback -= GameManager.Instance.GameOver; // prevent duplicates
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

        if (Input.GetKeyDown(KeyCode.C) && !this.isGameOver)
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

    public Player GetPlayer() { return this.player; }

    public int GetPlayerCurrentBullet()
    {
        return PlayerData.Instance.currentBulletId;
    }

    // ==================================================

    public Vector3 ToViewportPos(Vector3 worldPos)
    {
        return this.viewport.WorldToViewportPoint(worldPos);
    }

    // ==================================================

    public void OnPlayerCollidedWithBullet(Player player, EnemyBullet enemyBullet)
    {
        player.OnCollidedWithBullet(enemyBullet);
    }

    public void OnPlayerTakenDamage(float dmgTaken)
    {
        GameDataManager.Instance.UpdatePlayerHealth(dmgTaken);

        GameUIManager.Instance.OnPlayerHealthChange(PlayerData.Instance.health);

        if (PlayerData.Instance.health <= 0f) this.GameOver();
    }

    public void OnInvokeSpecialAtk1()
    {
        this.player.InvokeSpecialAtk(PlayerData.Instance.specialBullet1Id);

        int remainingAmt = PlayerData.Instance.specialBullet1Amount - 1;

        GameUIManager.Instance.OnOutOfSpBullets1(remainingAmt);

        GameDataManager.Instance.UpdatePlayerSpecialBullet1Amount(remainingAmt);
    }

    public void OnInvokeSpecialAtk2()
    {
        this.player.InvokeSpecialAtk(PlayerData.Instance.specialBullet2Id);

        int remainingAmt = PlayerData.Instance.specialBullet2Amount - 1;

        GameUIManager.Instance.OnOutOfSpBullets2(remainingAmt);

        GameDataManager.Instance.UpdatePlayerSpecialBullet2Amount(remainingAmt);
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
