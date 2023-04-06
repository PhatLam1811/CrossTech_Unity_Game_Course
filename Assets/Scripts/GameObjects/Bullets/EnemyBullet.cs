using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : BaseBullet
{
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            GamePlayManager.Instance.OnPlayerCollidedWithBullet(player, this, this.damage);

            this.OnCollidedWithPlayer(player);
        }
    }

    protected override void Init()
    {
        base.Init();
        this.movingVector = Vector3.down;
        this.transform.rotation = Quaternion.Euler(x: 0f, y: 0f, z: -180f);
    }

    public virtual void OnCollidedWithPlayer(Player player)
    {
        Destroy(this.gameObject);
    }
}
