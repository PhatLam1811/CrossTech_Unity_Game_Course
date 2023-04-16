using UnityEngine;

public class EnemyBullet : BaseBullet
{
    private void OnTriggerEnter2D(Collider2D collision) { }

    protected override void Init()
    {
        base.Init();

        this.SetMovingVector(Vector3.down);
        
        this.transform.rotation = Quaternion.Euler(x: 0f, y: 0f, z: -180f);
    }

    public virtual void OnCollidedWithPlayer()
    {
        this.DestroySelf();
    }
}
