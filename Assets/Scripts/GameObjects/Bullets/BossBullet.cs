using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : BaseBullet
{
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            OnCollidedWithPlayer(player);
        }
    }

    protected override void Init()
    {
        base.Init();

        movingVector = Vector3.down;
        transform.rotation = Quaternion.Euler(x: 0f, y: 0f, z: -180f);
    }

    public virtual void OnCollidedWithPlayer(Player player)
    {
        player.OnCollidedWithBullet(this);
        Destroy(gameObject);
    }
}
