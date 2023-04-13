using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : BaseEnemy
{
    private const float atkInterval = 1.5f;

    private bool isAttacking;
    private float cooldown;

    [SerializeField] private GameObject pfBullet;
    [SerializeField] private GameObject pfGunBarrel;

    // Update is called once per frame
    protected override void Update()
    {
        if (!this.isGameOver)
        {
            float elapsedTime = Time.deltaTime;

            if (!isAttacking)
            {
                OnAppearing();
            }
            else
            {
                OnAttacking(elapsedTime);
            }

            base.Move(elapsedTime);
        }
    }

    protected override void Init()
    {
        base.Init();

        this.health = 20f;
        this.speed = 1f;
        this.point = 500;
        this.movingVector = Vector3.down;
        this.cooldown = atkInterval;
        this.isAttacking = false;
    }

    public override void Attack(float elapsedTime)
    {
        cooldown -= elapsedTime;

        if (cooldown <= 0.0f)
        {
            // Debug.Log(GetTypeName() + " shooting");

            Vector3 barrelPos = pfGunBarrel.transform.position;

            // shoot a bullet every 1s
            var obj = Instantiate(pfBullet, barrelPos, Quaternion.identity);

            obj.GetComponent<BaseBullet>().SetMovingVector(new Vector3(x: 0f, y: -1f));

            // reset cooldown
            cooldown = atkInterval;
        }
    }

    public override void OnDefeated()
    {
        base.OnDefeated();

        GamePlayManager.Instance.GameOver();
    }

    public void OnAppearing()
    {
        // switch to viewport's (main camera) normalized coordinate
        Vector3 worldPos = transform.position;
        Vector3 viewportPos = viewport.WorldToViewportPoint(worldPos);

        if (viewportPos.y <= 0.8f)
        {
            isAttacking = true;
            movingVector = new Vector3(x: 1f, y: 0f);
        }
    }

    public void OnAttacking(float elapsedTime)
    {
        // switch to viewport's (main camera) normalized coordinate
        Vector3 worldPos = this.transform.position;
        Vector3 viewportPos = this.viewport.WorldToViewportPoint(worldPos);

        if (viewportPos.x > 0.9f) this.movingVector = new Vector3(x: -1f, y: 0f);
        if (viewportPos.x < 0f) this.movingVector = new Vector3(x: 1f, y: 0f);

        this.Attack(elapsedTime);
    }
}
