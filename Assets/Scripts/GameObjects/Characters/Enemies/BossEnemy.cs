using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : BaseEnemy
{
    private const float atkInterval = 1.5f;

    private bool isAttacking;
    private float cooldown;

    [SerializeField] private GameObject pfAmmoType;
    [SerializeField] private GameObject pfGunBarrel;

    // Update is called once per frame
    protected override void Update()
    {
        float elapsedTime = GetElapsedTime();

        if (!isAttacking)
        {
            OnAppearing(elapsedTime);
        }
        else
        {
            OnAttacking(elapsedTime);
        }

        base.Move(elapsedTime);
    }

    protected override void Init()
    {
        base.Init();

        health = 10;
        speed = 1f;
        point = 5;
        movingVector = new Vector3(x: 0f, y: -1f);
        cooldown = atkInterval;
        isAttacking = false;
    }

    public void OnAttacking(float elapsedTime)
    {
        // switch to viewport's (main camera) normalized coordinate
        Vector3 worldPos = transform.position;
        Vector3 viewportPos = viewport.WorldToViewportPoint(worldPos);

        if (viewportPos.x > 0.9f) movingVector = new Vector3(x: -1f, y: 0f);
        if (viewportPos.x < 0f) movingVector = new Vector3(x: 1f, y: 0f);

        Attack(elapsedTime);
    }

    public override void Attack(float elapsedTime)
    {
        cooldown -= elapsedTime;

        if (cooldown <= 0.0f)
        {
            // Debug.Log(GetTypeName() + " shooting");

            Vector3 barrelPos = pfGunBarrel.transform.position;

            // shoot a bullet every 1s
            var obj = Instantiate(pfAmmoType, barrelPos, Quaternion.identity);

            obj.GetComponent<BaseBullet>().SetMovingVector(new Vector3(x: 0f, y: -1f));

            // reset cooldown
            cooldown = atkInterval;
        }
    }

    public void OnAppearing(float elapsedTime)
    {
        // switch to viewport's (main camera) normalized coordinate
        Vector3 worldPos = transform.position;
        Vector3 viewportPos = viewport.WorldToViewportPoint(worldPos);

        if (viewportPos.y <= 0.8f)
        {
            isAttacking = true;
            movingVector = new Vector3(x: 1f, y: 0f);
        }
    }
}
