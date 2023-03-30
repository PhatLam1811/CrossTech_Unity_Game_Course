using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuckshotAmmo : BaseAmmo
{
    bool isExploded;
    float explodedCountdown;

    [SerializeField] GameObject pfBuckExplode;
 
    // Start is called before the first frame update
    void Start()
    {
        viewport = Camera.main;

        speed = 1f;
        movingVector = new Vector3(x: 0f, y: 1f);

        isExploded = false;
        explodedCountdown = 2f;
    }

    // Update is called once per frame
    void Update()
    {
        float elapsedTime = GetElapsedTime();

        if (!isExploded) explodedCountdown -= elapsedTime;

        if (explodedCountdown <= 0f && !isExploded) Explode();

        base.Move(elapsedTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnCollided(collision.gameObject);   
    }

    private void Explode()
    {
        // generate 2 more buckshots
        var leftShot = Instantiate(pfBuckExplode, transform.position, Quaternion.identity);
        var rightShot = Instantiate(pfBuckExplode, transform.position, Quaternion.identity);

        Vector3 leftShotVector = new Vector3(x: -0.5f, y: 1f);
        Vector3 rightShotVector = new Vector3(x: 0.5f, y: 1f);

        leftShot.GetComponent<BaseAmmo>().SetMovingVector(leftShotVector);
        rightShot.GetComponent<BaseAmmo>().SetMovingVector(rightShotVector);

        leftShot.GetComponent<BaseAmmo>().SetSpeed(3f);
        rightShot.GetComponent<BaseAmmo>().SetSpeed(3f);
        
        speed = 3f;
        isExploded = true;
    }
}
