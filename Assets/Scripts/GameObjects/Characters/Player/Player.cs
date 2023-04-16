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
    }

    protected override void LoadConfig()
    {
        PlayerConfig config = PlayerConfig.Instance;

        this.SetSpeed(config.Speed);

        this.cooldown = config.Cooldown;
    }

    public override void Attack(float elapsedTime)
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
        Vector3 barrelPos = this.tfGunBarrel.transform.position;
        BulletManager.Instance.ShootBulletOfType(type, barrelPos);
    }
}
