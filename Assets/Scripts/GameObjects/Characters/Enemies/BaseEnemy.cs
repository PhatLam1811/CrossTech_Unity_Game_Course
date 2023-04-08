using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : BaseCharacter
{
    protected float health;
    protected int point;

    public float GetHealth() { return health; }
    public int GetPoint() { return point; }

    protected override void Init()
    {
        base.Init();

        this.health = 1f;
        this.point = 1;
        this.speed = 3f;
        this.movingVector = Vector3.down;
    }

    public override void Move(float elapsedTime)
    {
        // switch to viewport's (main camera) normalized coordinate
        Vector3 worldPos = transform.position;
        Vector3 viewportPos = viewport.WorldToViewportPoint(worldPos);

        if (viewportPos.y < 0f) // 0f: viewport's bottom edge
        {
            Destroy(gameObject);
        }
        else
        {
            base.Move(elapsedTime);
        }
    }

    public override void OnTakenDamage(float dmgTaken)
    {
        this.health -= dmgTaken;

        if (this.health <= 0f) this.OnDefeated();
    }

    public virtual void OnCollidedWithPlayer(Player player, float dmgTaken)
    {
        this.OnTakenDamage(dmgTaken);
    }

    public virtual void OnDefeated()
    {
        GamePlayManager.Instance.OnDefeatEnemy(this.point);

        GamePlayManager.Instance.UnloadGameObjs(this);

        Destroy(this.gameObject);
    }
}
