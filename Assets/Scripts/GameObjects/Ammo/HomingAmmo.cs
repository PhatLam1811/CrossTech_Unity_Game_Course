using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingAmmo : BaseAmmo
{
    BaseEnemy target;

    // Start is called before the first frame update
    void Start()
    {
        viewport = Camera.main;

        speed = 2f;
        movingVector = new Vector3(x: 0f, y: 1f);
    }

    // Update is called once per frame
    void Update()
    {   
        float elapsedTime = GetElapsedTime();

        Chase();

        Move(elapsedTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var ammoCollider = GetComponent<BoxCollider2D>();
        var radarCollider = GetComponentInChildren<CircleCollider2D>();

        if (radarCollider != null) Physics2D.IgnoreCollision(ammoCollider, radarCollider);

        OnCollided(collision.gameObject);
    }

    public void LockTarget(BaseEnemy target)
    {
        this.target = target;
        speed = 3f;
    }

    public void Chase()
    {
        if (target == null) return;

        movingVector = target.transform.position - transform.position;
    }
}
