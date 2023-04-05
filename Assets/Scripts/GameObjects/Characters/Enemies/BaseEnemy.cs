using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : BaseCharacter
{
    protected int point;

    public int GetPoint() { return point; }

    protected override void Init()
    {
        base.Init();

        health = 1;
        point = 1;
        speed = 3f;
        movingVector = new Vector3(x: 0f, y: -1f);
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

    public virtual void OnCollidedWithPlayer()
    {
        OnDamaged(1);
    }
}
