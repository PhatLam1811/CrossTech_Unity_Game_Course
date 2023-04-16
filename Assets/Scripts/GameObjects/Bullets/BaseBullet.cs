using UnityEngine;

public class BaseBullet : BaseGameObj
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out BaseEnemy enemy))
        {
            EnemyManager.Instance.OnCollidedWithBullet(enemy, this);

            // bullet disapear after hitting an enemy
            this.DestroySelf();
        }
    }

    protected override void Init()
    {
        base.Init();

        this.SetMovingVector(Vector3.up);
        this.SetSpeed(10f);
        this.SetDamageInflict(1f);
    }

    protected override void Move(float elapsedTime)
    {
        // switch to viewport's (main camera) normalized coordinate
        Vector3 viewportPos = GamePlayManager.Instance.ToViewportPos(this.transform.position);

        const float minRange = 0f;
        const float maxRange = 1f;

        if (viewportPos.y >= minRange && viewportPos.y <= maxRange &&   // 0f: viewport's bottom edge - 1f: viewport's top edge
            viewportPos.x >= minRange && viewportPos.x <= maxRange)     // 0f: viewport's left edge - 1f: viewport's right edge
        {
            base.Move(elapsedTime);
        }
        else
        {
            // bullet disapear if out of camera view
            this.DestroySelf();
        }
    }
}
