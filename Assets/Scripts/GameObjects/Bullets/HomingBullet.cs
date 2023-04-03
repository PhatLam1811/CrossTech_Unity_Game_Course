using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingBullet : BaseBullet
{
    private const float homingSpeed = 3f;

    private BaseEnemy target;

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        if (target) Homing();
    }

    protected override void Init()
    {
        base.Init();

        target = null;
    }

    public void SetTarget(BaseEnemy target)
    {
        this.target = target; // lock target found by radar
        speed = homingSpeed;
    }

    public void Homing()
    {
        // homing vector 
        movingVector = (target.transform.position - transform.position).normalized;

        // rotate angle
        float angle = Vector3.Angle(movingVector, Vector3.up);

        if (movingVector.x > 0f) angle = -angle; // rotate left or right?

        angle = Mathf.LerpAngle(0f, angle, Time.deltaTime * homingSpeed);

        transform.Rotate(Vector3.forward, angle);
    }
}
