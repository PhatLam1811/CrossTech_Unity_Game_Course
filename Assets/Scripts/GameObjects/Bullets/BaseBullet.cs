using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBullet : BaseGameObj
{
    protected int damage;

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out BaseEnemy enemy))
        {
            OnCollidedWithEnemy(enemy);
        }
    }

    public int GetDamage() { return damage; }

    protected override void Init()
    {
        base.Init();

        damage = 1;
        speed = 2f;
        movingVector = new Vector3(x: 0f, y: 1f);
    }

    public override void Move(float elapsedTime)
    {
        // switch to viewport's (main camera) normalized coordinate
        Vector3 worldPos = transform.position;
        Vector3 viewportPos = viewport.WorldToViewportPoint(worldPos);

        if (viewportPos.y >= 0f && viewportPos.y <= 1f &&   // 0f: viewport's bottom edge - 1f: viewport's top edge
            viewportPos.x >= 0f && viewportPos.x <= 1f)     // 0f: viewport's left edge - 1f: viewport's right edge
        {
            base.Move(elapsedTime);
        }
        else
        {
            // bullet disapear if out of camera view
            Destroy(gameObject);
        }
    }

    public virtual void OnCollidedWithEnemy(BaseEnemy enemy)
    {
        enemy.OnCollidedWithBullet(this);

        // bullet disapear after hitting an enemy
        Destroy(gameObject);
    }
}
