using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingRadar : BaseGameObj
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out BaseEnemy enemy))
        {
            OnCollidedWithEnemy(enemy);
        }
    }

    public void OnCollidedWithEnemy(BaseEnemy enemy)
    {
        // lock target for homing bullet
        GetComponentInParent<HomingBullet>().SetTarget(enemy);

        // radar is no longer needed (can be fixed)
        Destroy(gameObject);
    }
}