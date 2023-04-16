using UnityEngine;

public class Player : BaseCharacter
{
    [SerializeField] private Transform tfGunBarrel;

    private float cooldown;

    // ==================================================

    void Update()
    {
        if (!this.isGameOver)
        {
            float elapsedTime = Time.deltaTime;

            this.Attack(elapsedTime);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // hit by an enemy
        if (collision.TryGetComponent(out BaseEnemy enemy))
        {
            EnemyManager.Instance.OnPlayerCollidedWithEnemy(enemy);

            this.OnTakenDamage(enemy.GetDamageInflict());
        }

        // hit by an enemie's bullet
        if (collision.TryGetComponent(out EnemyBullet bullet))
        {
            BulletManager.Instance.OnPlayerCollidedWithBullet(bullet);

            this.OnTakenDamage(bullet.GetDamageInflict());
        }
    }

    // ==================================================

    #region Overrides
    protected override void Init()
    {
        base.Init();
        this.LoadPlayerConfig();
    }

    public override void OnTakenDamage(float dmgTaken)
    {
        GamePlayManager.Instance.OnPlayerTakenDamage(dmgTaken);
    }
    #endregion

    // ==================================================

    private void LoadPlayerConfig()
    {
        PlayerConfig config = PlayerConfig.Instance;

        this.SetSpeed(config.Speed);

        this.cooldown = config.Cooldown;
    }

    public void Attack(float elapsedTime)
    {
        this.cooldown -= elapsedTime;

        // shoot a bullet after cooldown
        if (this.cooldown <= 0.0f)
        {
            Vector3 barrelPos = this.tfGunBarrel.position;
            int currentBullet = GamePlayManager.Instance.GetPlayerCurrentBullet();

            BulletManager.Instance.ShootBulletOfType(currentBullet, barrelPos);

            // reset cooldown
            this.cooldown = PlayerConfig.Instance.Cooldown;
        }
    }

    public void OnPlayerMove(float elapsedTime, Vector3 movingVector)
    {
        this.SetMovingVector(movingVector);
        base.Move(elapsedTime);
    }

    public void InvokeSpecialAtk(int bulletId)
    {
        BulletManager.Instance.ShootBulletOfType(bulletId, this.tfGunBarrel.position);
    }
}
