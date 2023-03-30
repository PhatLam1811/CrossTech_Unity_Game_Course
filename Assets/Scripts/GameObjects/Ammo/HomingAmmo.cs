using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingAmmo : BaseAmmo
{
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

        Move(elapsedTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        onCollided(collision.gameObject);
    }
}
