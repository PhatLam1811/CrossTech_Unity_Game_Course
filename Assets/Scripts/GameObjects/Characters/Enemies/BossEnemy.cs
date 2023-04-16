using UnityEngine;

public class BossEnemy : BaseEnemy
{
    [SerializeField] private GameObject pfBullet;
    [SerializeField] private GameObject pfGunBarrel;

    private bool isAttacking;

    private float atkCooldown;
    private float atkInterval;

    private int bulletId;

    void Update()
    {
        if (!this.isGameOver)
        {
            float elapsedTime = Time.deltaTime;

            if (!this.isAttacking)
            {
                this.OnAppearing();
            }
            else
            {
                this.OnAttacking(elapsedTime);
            }

            base.Move(elapsedTime);
        }
    }

    protected override void Init()
    {
        base.Init();
        this.isAttacking = false;
    }

    protected override void LoadConfig()
    {
        EnemyConfig config = EnemyManager.Instance.GetConfigOfType(GameDefine.BOSS_ENEMY_ID);

        this.health = config.Health;
        this.speed = config.Speed;
        this.point = config.Point;
        this.atkCooldown = config.Cooldown;
        this.bulletId = config.BulletId;
    }

    public override void Attack(float elapsedTime)
    {
        this.atkInterval -= elapsedTime;

        if (this.atkInterval <= 0.0f)
        {
            Vector3 barrelPos = this.pfGunBarrel.transform.position;

            // shoot a bullet every 1s
            var obj = Instantiate(pfBullet, barrelPos, Quaternion.identity);

            obj.GetComponent<BaseBullet>().SetMovingVector(new Vector3(x: 0f, y: -1f));

            // reset cooldown
            this.atkInterval = this.atkCooldown;
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
        Vector3 viewportPos = GamePlayManager.Instance.ToViewportPos(this.transform.position);

        if (viewportPos.y <= 0.8f)
        {
            this.isAttacking = true;
            this.atkInterval = this.atkCooldown;
            this.movingVector = Vector3.right;
        }
    }

    public void OnAttacking(float elapsedTime)
    {
        // switch to viewport's (main camera) normalized coordinate
        Vector3 viewportPos = GamePlayManager.Instance.ToViewportPos(this.transform.position);

        if (viewportPos.x > 0.9f) this.movingVector = new Vector3(x: -1f, y: 0f);
        if (viewportPos.x < 0f) this.movingVector = new Vector3(x: 1f, y: 0f);

        this.Attack(elapsedTime);
    }
}
