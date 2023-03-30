using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuckExplode : BaseAmmo
{
    // Start is called before the first frame update
    void Start()
    {
        viewport = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(movingVector + " & " + speed + " & " + transform.position);

        float elapsedTime = GetElapsedTime();

        base.Move(elapsedTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnCollided(collision.gameObject);
    }
}
