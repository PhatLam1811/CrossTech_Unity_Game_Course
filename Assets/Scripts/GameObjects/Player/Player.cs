using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : BaseGameObj, ICollidable
{
    float cooldown;

    [SerializeField] GameObject pfAmmoType;
    [SerializeField] GameObject pfGunBarrel;

    // Start is called before the first frame update
    void Start()
    {
        speed = 3.0f;
        cooldown = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        float elapsedTime = GetElapsedTime();

        ProcessInput();

        Move(elapsedTime);
        Shoot(elapsedTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        onCollided(collision.gameObject);
    }

    public void ProcessInput()
    {
        // ======================================
        // Move
        // ======================================
        // get normalized moving input directions
        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");

        movingVector = new Vector3(x: hor, y: ver);
    }

    public void Shoot(float elapsedTime)
    {
        cooldown -= elapsedTime;

        if (cooldown <= 0.0f)
        {
            // Debug.Log(GetTypeName() + " shooting");

            Vector3 barrelPos = pfGunBarrel.transform.position;

            // shoot a bullet every 1s
            Instantiate(pfAmmoType, barrelPos, Quaternion.identity);

            // reset cooldown
            cooldown = 1f;
        }
    }

    public void onCollided(GameObject collidedObj)
    {
        if (collidedObj.TryGetComponent<BaseEnemy>(out BaseEnemy collidedEnemy))
        {
            // destroy both objs if collided with an enemy
            Destroy(gameObject);
            collidedEnemy.onCollided(gameObject);
        }
    }
}
