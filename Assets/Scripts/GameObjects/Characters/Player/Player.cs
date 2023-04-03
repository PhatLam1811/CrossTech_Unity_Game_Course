using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : BaseCharacter
{
    private const float atkInterval = 1f;

    private float cooldown;
    private int currentBullet;

    [SerializeField] private GameObject pfGunBarrel;
    [SerializeField] private List<GameObject> pfBullets;

    // Update is called once per frame
    protected override void Update()
    {
        float elapsedTime = GetElapsedTime();

        ProcessInput();

        Move(elapsedTime);

        Attack(elapsedTime);
    }

    void OnTriggerEnter2D(Collider2D collision) { }

    public void SetCurrentBullet(int currentBullet)
    {
        if (currentBullet < 0)
        {
            this.currentBullet = pfBullets.Count - 1;
        }
        else if (currentBullet >= pfBullets.Count)
        {
            this.currentBullet = 0;
        }
        else
        {
            this.currentBullet = currentBullet;
        }    
    }

    public void ProcessInput()
    {
        // ======================================
        // Move
        // ======================================
        // get normalized moving input directions
        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");

        SetMovingVector(new Vector3(x: hor, y: ver));

        if (Input.GetKeyDown(KeyCode.Q))
        {
            // previous type
            SetCurrentBullet(currentBullet - 1);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            // next type
            SetCurrentBullet(currentBullet + 1);
        }
    }

    protected override void Init()
    {
        base.Init();

        health = 10;
        currentBullet = 0;
        cooldown = atkInterval;
        speed = 3f;
    }

    public override void Attack(float elapsedTime)
    {
        cooldown -= elapsedTime;

        // shoot a bullet after cooldown
        if (cooldown <= 0.0f)
        {
            Vector3 barrelPos = pfGunBarrel.transform.position;

            Instantiate(pfBullets[currentBullet], barrelPos, Quaternion.identity);

            // reset cooldown
            cooldown = atkInterval;
        }
    }

    public virtual void OnColliedWithEnemy()
    {
        OnDamaged(1);
    }
}
