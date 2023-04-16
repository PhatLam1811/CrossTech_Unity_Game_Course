using UnityEngine;

public class EnemyBullet : BaseBullet
{
    private void OnTriggerEnter2D(Collider2D collision) { }

    protected override void Init()
    {
        base.Init();

        this.SetMovingVector(Vector3.down);
    }

    protected override void LoadConfig()
    {
        BulletConfig config = BulletManager.Instance.GetBulletConfigOfType(GameDefine.BOSS_BULLET_ID);

        this.SetSpeed(config.speed);
        this.SetDamageInflict(config.damage);
    }

    public virtual void OnCollidedWithPlayer()
    {
        this.DestroySelf();
    }
}
