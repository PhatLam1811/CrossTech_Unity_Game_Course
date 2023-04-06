using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCharacter : BaseGameObj
{
    public virtual void Attack(float elapsedTime) { }

    public virtual void OnTakenDamage(float dmgTaken) { }

    public virtual void OnCollidedWithBullet(BaseBullet bullet, float dmgTaken)
    {
        this.OnTakenDamage(dmgTaken);
    }
}
