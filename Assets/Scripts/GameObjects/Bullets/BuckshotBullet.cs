using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuckshotBullet : BaseBullet
{
    private const float explodingSpeed = 3f;

    private bool isExploded;
    private float countdown;

    [SerializeField] private GameObject pfBuckExplode;

    // Update is called once per frame
    protected override void Update()
    {
        if (!this.isGameOver)
        {
            float elapsedTime = Time.deltaTime;

            if (!isExploded) countdown -= elapsedTime;

            if (countdown <= 0f && !isExploded) Explode();

            base.Move(elapsedTime);
        }
    }

    protected override void Init()
    {
        base.Init();

        speed = 1f;
        isExploded = false;
        countdown = 2f;
    }

    private void Explode()
    {
        // generate 2 more buckshots
        var leftShot = Instantiate(pfBuckExplode, transform.position, Quaternion.identity);
        var rightShot = Instantiate(pfBuckExplode, transform.position, Quaternion.identity);

        Vector3 leftShotVector = new Vector3(x: -0.5f, y: 1f);
        Vector3 rightShotVector = new Vector3(x: 0.5f, y: 1f);

        // exploded bullets' moving vector
        leftShot.GetComponent<ExplodedBuckshot>().SetMovingVector(leftShotVector);
        rightShot.GetComponent<ExplodedBuckshot>().SetMovingVector(rightShotVector);

        // exploded bullets' moving speed
        leftShot.GetComponent<ExplodedBuckshot>().SetSpeed(explodingSpeed);
        rightShot.GetComponent<ExplodedBuckshot>().SetSpeed(explodingSpeed);

        // exploded bullets' rotation
        float rotateAngle = Vector3.Angle(movingVector, leftShotVector);

        leftShot.GetComponent<ExplodedBuckshot>().transform.Rotate(Vector3.forward, rotateAngle);
        rightShot.GetComponent<ExplodedBuckshot>().transform.Rotate(Vector3.forward, -rotateAngle);

        speed = explodingSpeed;
        isExploded = true;
    }
}
