using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Player : BaseCharacter
{
    private float cooldown;

    [SerializeField] private GameObject pfGunBarrel;

    // Update is called once per frame
    protected override void Update()
    {
        if (this.isPlaying)
        {
            float elapsedTime = Time.deltaTime;

            this.Attack(elapsedTime);
        }
    }

    void OnTriggerEnter2D(Collider2D collision) 
    { 
        if (collision.TryGetComponent(out BaseEnemy enemy))
        {
            EnemyManager.Instance.OnCollidedWithPlayer(enemy, this, damage);

            this.OnTakenDamage(enemy.GetDamageInflict());
        }
    }

    // ==================================================

    protected override void Init()
    {
        base.Init();

        this.cooldown = GameDefine.DEFAULT_PLAYER_ATK_CD;
        this.speed = GameDefine.DEFAULT_PLAYER_SPEED;
    }

    public override void Attack(float elapsedTime)
    {
        this.cooldown -= elapsedTime;

        // shoot a bullet after cooldown
        if (this.cooldown <= 0.0f)
        {
            Vector3 barrelPos = this.pfGunBarrel.transform.position;
            int currentBullet = GamePlayManager.Instance.GetPlayerCurrentBullet();

            BulletManager.Instance.ShootBulletOfType(currentBullet, barrelPos);

            // reset cooldown
            this.cooldown = GameDefine.DEFAULT_PLAYER_ATK_CD;
        }
    }

    public override void OnTakenDamage(float dmgTaken)
    {
        GamePlayManager.Instance.OnPlayerTakenDamage(dmgTaken);
    }

    // ==================================================

    public void Move(float elapsedTime, Vector3 movingVector)
    {
        this.movingVector = movingVector;
        base.Move(elapsedTime);
    }

    public void InvokeSpecialAtk(int type)
    {
        Vector3 barrelPos = this.pfGunBarrel.transform.position;
        BulletManager.Instance.ShootBulletOfType(type, barrelPos);
    }

    public void GameOver()
    {
        this.isPlaying = false;
    }
}
