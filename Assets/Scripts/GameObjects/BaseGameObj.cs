using System;
using UnityEngine;

public class BaseGameObj : MonoBehaviour
{
    protected float speed;
    protected Vector3 movingVector;
    protected float damage;

    protected bool isGameOver;

    // ==================================================

    void Start()
    {
        this.Init();
    }

    void Update()
    {
        if (!isGameOver)
        {
            float elapsedTime = Time.deltaTime;

            this.Move(elapsedTime);
        }
    }

    // ==================================================

    #region Accessors
    public void SetMovingVector(Vector3 movingVector) { this.movingVector = movingVector; }
    public Vector3 GetMovingVector() { return this.movingVector; }

    public void SetSpeed(float speed) { this.speed = speed; }
    public float GetSpeed() { return this.speed; }

    public void SetDamageInflict(float damage) { this.damage = damage; }
    public float GetDamageInflict() { return this.damage; }
    #endregion

    // ==================================================

    #region Virtuals
    protected virtual void Init()
    {
        this.SetMovingVector(Vector3.zero);
        this.SetSpeed(GameDefine.DEFAULT_GAME_OBJECT_SPEED);
        this.SetDamageInflict(GameDefine.DEFAULT_GAME_OBJECT_DAMAGE);

        GamePlayManager.Instance.onGameOverCallback -= this.GameOver;
        GamePlayManager.Instance.onGameOverCallback += this.GameOver;

        GamePlayManager.Instance.onGameReplayCallback -= this.OnReplayGame;
        GamePlayManager.Instance.onGameReplayCallback += this.OnReplayGame;

        this.isGameOver = false;
    }

    protected virtual void LoadConfig() { }

    protected virtual void Move(float elapsedTime)
    {
        this.transform.position += this.movingVector * elapsedTime * this.speed;
    }
    #endregion

    // ==================================================

    protected void DestroySelf()
    {
        GamePlayManager.Instance.onGameOverCallback -= this.GameOver;
        GamePlayManager.Instance.onGameReplayCallback -= this.OnReplayGame;

        if (this.gameObject != null)
        {
            Destroy(this.gameObject);
        }
    }

    protected void GameOver()
    {
        this.isGameOver = true;
    }

    protected void OnReplayGame()
    {
        this.DestroySelf();
    }
}
