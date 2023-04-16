using UnityEngine;

public class HomingBullet : BaseBullet
{
    private BaseEnemy target;
    private float homingSpeed;

    // ==================================================

    void Update()
    {
        if (!this.isGameOver)
        {
            float elapsedTime = Time.deltaTime;

            this.Move(elapsedTime);

            if (this.target) Homing();
        }
    }

    // ==================================================

    #region Overrides
    protected override void Init()
    {
        base.Init();

        this.LoadConfig();

        this.target = null;
    }

    protected override void LoadConfig()
    {
        BulletConfig config = BulletManager.Instance.GetBulletConfigOfType(GameDefine.HOMING_BULLET_ID);

        this.SetSpeed(config.speed);
        this.SetDamageInflict(config.damage);

        this.homingSpeed = config.homingSpeed;
    }
    #endregion

    // ==================================================

    public void SetTarget(BaseEnemy target)
    {
        this.target = target; // lock target found by radar
        this.speed = homingSpeed;
    }

    public void Homing()
    {
        // homing vector 
        movingVector = (target.transform.position - transform.position).normalized;

        // rotate angle
        float angle = Vector3.Angle(movingVector, Vector3.up);

        if (movingVector.x > 0f) angle = -angle; // rotate left or right?

        angle = Mathf.LerpAngle(0f, angle, Time.deltaTime * homingSpeed);

        transform.Rotate(Vector3.forward, angle);
    }
}
