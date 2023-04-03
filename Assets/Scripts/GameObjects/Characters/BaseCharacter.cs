using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCharacter : BaseGameObj
{
    protected int health;

    protected override void Init()
    {
        base.Init();

        health = 10;
    }

    public virtual void Attack(float elapsedTime) { }

    public virtual void OnDamaged(int dmg)
    {
        health -= dmg;

        if (health <= 0f) Destroy(this.gameObject);
    }

    public virtual void OnCollidedWithBullet(BaseBullet bullet)
    {
        OnDamaged(bullet.GetDamage());
    }
}
