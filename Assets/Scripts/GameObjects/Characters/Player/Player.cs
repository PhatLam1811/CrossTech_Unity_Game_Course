using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : BaseCharacter
{
    [SerializeField] private GameObject pfGunBarrel;

    private float atkCooldown;
    private float atkInterval;

    // Update is called once per frame
    protected override void Update()
    {
        if (!this.isGameOver)
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

        this.LoadConfig();

        this.atkInterval = this.atkCooldown;
    }

    public override void Attack(float elapsedTime)
    {
        this.atkInterval -= elapsedTime;

        // shoot a bullet after cooldown
        if (this.atkInterval <= 0.0f)
        {
            Vector3 barrelPos = this.pfGunBarrel.transform.position;
            int currentBullet = GamePlayManager.Instance.GetPlayerCurrentBullet();

            BulletManager.Instance.ShootBulletOfType(currentBullet, barrelPos);

            // reset cooldown
            this.atkInterval = this.atkCooldown;
        }
    }

    public override void OnTakenDamage(float dmgTaken)
    {
        GamePlayManager.Instance.OnPlayerTakenDamage(dmgTaken);
    }

    // ==================================================

    private void LoadConfig()
    {
        PlayerConfig config = PlayerConfig.Instance;

        this.speed = config.speed;
        this.atkCooldown = config.cooldown;
    }

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
}
