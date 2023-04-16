using UnityEngine;

public class BaseBullet : BaseGameObj
{
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out BaseEnemy enemy))
        {
            EnemyManager.Instance.OnCollidedWithBullet(enemy, this, this.damage);

            // bullet disapear after hitting an enemy
            this.DestroySelf();
        }
    }

    protected override void Init()
    {
        base.Init();

        this.damage = 1;
        this.speed = 10f;
        this.movingVector = Vector3.up;
    }

    public override void Move(float elapsedTime)
    {
        // switch to viewport's (main camera) normalized coordinate
        Vector3 viewportPos = GamePlayManager.Instance.ToViewportPos(this.transform.position);

        if (viewportPos.y >= 0f && viewportPos.y <= 1f &&   // 0f: viewport's bottom edge - 1f: viewport's top edge
            viewportPos.x >= 0f && viewportPos.x <= 1f)     // 0f: viewport's left edge - 1f: viewport's right edge
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
