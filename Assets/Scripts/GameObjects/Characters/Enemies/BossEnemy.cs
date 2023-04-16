using UnityEngine;

public class BossEnemy : BaseEnemy
{
    [SerializeField] private GameObject pfBullet;
    [SerializeField] private GameObject pfGunBarrel;

    private bool isAttacking;

    private float cooldown;
    private float interval;

    private int bulletId;

    // ==================================================

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

    // ==================================================

    #region Overrides
    protected override void Init()
    {
        base.Init();
        this.isAttacking = false;
    }

    protected override void LoadConfig()
    {
        EnemyConfig config = EnemyManager.Instance.GetEnemyConfigOfType(GameDefine.BOSS_ENEMY_ID);

        this.SetHealth(config.health);
        this.SetSpeed(config.speed);
        this.SetPoint(config.point);
        
        this.cooldown = config.cooldown;
        this.bulletId = config.bulletId;
    }

    public override void OnDefeated()
    {
        base.OnDefeated();
        GamePlayManager.Instance.GameOver();
    }
    #endregion

    // ==================================================

    private void Attack(float elapsedTime)
    {
        this.interval -= elapsedTime;

        if (this.interval <= 0.0f)
        {
            Vector3 barrelPos = this.pfGunBarrel.transform.position;

            BulletManager.Instance.ShootBulletOfType(bulletId, barrelPos);

            // shoot a bullet after cooldown
            //var obj = Instantiate(pfBullet, barrelPos, Quaternion.identity);

            //obj.GetComponent<BaseBullet>().SetMovingVector(new Vector3(x: 0f, y: -1f));

            // reset cooldown
            this.interval = this.cooldown;
        }
    }

    private void OnAppearing()
    {
        // switch to viewport's (main camera) normalized coordinate
        Vector3 viewportPos = GamePlayManager.Instance.ToViewportPos(this.transform.position);

        if (viewportPos.y <= 0.8f)
        {
            this.isAttacking = true;
            this.interval = this.cooldown;
            this.movingVector = Vector3.right;
        }
    }

    private void OnAttacking(float elapsedTime)
    {
        // switch to viewport's (main camera) normalized coordinate
        Vector3 viewportPos = GamePlayManager.Instance.ToViewportPos(this.transform.position);

        if (viewportPos.x > 0.9f) this.movingVector = new Vector3(x: -1f, y: 0f);
        if (viewportPos.x < 0f) this.movingVector = new Vector3(x: 1f, y: 0f);

        this.Attack(elapsedTime);
    }
}
