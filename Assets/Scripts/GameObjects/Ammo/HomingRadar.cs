using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingRadar : BaseGameObj, ICollidable
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var ammoCollider = GetComponentInParent<BoxCollider2D>();
        var radarCollider = GetComponent<CircleCollider2D>();

        Physics2D.IgnoreCollision(ammoCollider, radarCollider);

        Debug.Log(GetTypeName() + " collided");

        OnCollided(collision.gameObject);
    }

    public void OnCollided(GameObject collidedObj)
    {
        if (collidedObj.TryGetComponent<BaseEnemy>(out BaseEnemy collidedEnemy))
        {
            GetComponentInParent<HomingAmmo>().LockTarget(collidedEnemy);
            Destroy(gameObject);
        }
    }
}
