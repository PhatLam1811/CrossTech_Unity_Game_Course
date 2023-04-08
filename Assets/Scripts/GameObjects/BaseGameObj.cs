using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseGameObj : MonoBehaviour
{
    protected Camera viewport;

    protected float speed;
    protected Vector3 movingVector;

    protected float damage;

    protected bool isGameOver;

    // Start is called before the first frame update
    protected void Start()
    {
        this.Init();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (!isGameOver)
        {
            float elapsedTime = Time.deltaTime;

            this.Move(elapsedTime);
        }
    }

    public void SetMovingVector(Vector3 movingVector) { this.movingVector = movingVector; }
    public void SetSpeed(float speed) { this.speed = speed; }
    public float GetDamageInflict() { return this.damage; }

    protected virtual void Init()
    {
        this.viewport = Camera.main;

        this.movingVector = Vector3.zero;
        this.speed = 0f;
        this.damage = 1f;

        GamePlayManager.Instance.LoadGameObjs(this);
        this.isGameOver = false;
    }

    public virtual void Move(float elapsedTime)
    {
        this.transform.position += this.movingVector * elapsedTime * this.speed;
    }

    public void GameOver()
    {
        this.isGameOver = true;
    }
}
