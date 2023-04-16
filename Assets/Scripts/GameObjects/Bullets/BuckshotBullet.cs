using UnityEngine;

public class BuckshotBullet : BaseBullet
{
    private float explodedSpeed;
    private float countdown;
    private bool isExploded;

    [SerializeField] private GameObject pfBuckExplode;

    void Update()
    {
        if (!this.isGameOver)
        {
            float elapsedTime = Time.deltaTime;

            if (!this.isExploded)
            {
                this.countdown -= elapsedTime;

                if (this.countdown <= 0f) this.Explode();
            }

            base.Move(elapsedTime);
        }
    }

    protected override void Init()
    {
        base.Init();

        this.LoadConfig();
        
        this.isExploded = false;
    }

    protected override void LoadConfig()
    {
        BulletConfig config = BulletManager.Instance.GetBulletConfigOfType(GameDefine.BUCKSHOT_BULLET_ID);

        this.SetSpeed(config.speed);
        this.SetDamageInflict(config.damage);

        this.countdown = config.eplosionCountdown;
        this.explodedSpeed = config.explodedSpeed;
    }

    private void Explode()
    {
        // generate 2 more buckshots
        var leftShot = Instantiate(this.pfBuckExplode, this.transform.position, Quaternion.identity);
        var rightShot = Instantiate(this.pfBuckExplode, this.transform.position, Quaternion.identity);

        Vector3 leftShotVector = new Vector3(x: -0.5f, y: 1f);
        Vector3 rightShotVector = new Vector3(x: 0.5f, y: 1f);

        // exploded bullets' moving vector
        leftShot.GetComponent<ExplodedBuckshot>().SetMovingVector(leftShotVector);
        rightShot.GetComponent<ExplodedBuckshot>().SetMovingVector(rightShotVector);

        // exploded bullets' moving speed
        leftShot.GetComponent<ExplodedBuckshot>().SetSpeed(this.explodedSpeed);
        rightShot.GetComponent<ExplodedBuckshot>().SetSpeed(this.explodedSpeed);

        // exploded bullets' rotation
        float rotateAngle = Vector3.Angle(this.movingVector, leftShotVector);

        leftShot.GetComponent<ExplodedBuckshot>().transform.Rotate(Vector3.forward, rotateAngle);
        rightShot.GetComponent<ExplodedBuckshot>().transform.Rotate(Vector3.forward, -rotateAngle);

        this.speed = this.explodedSpeed;
        this.isExploded = true;
    }
}
