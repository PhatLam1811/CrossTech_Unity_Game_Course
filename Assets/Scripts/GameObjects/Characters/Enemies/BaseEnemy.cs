using UnityEngine;

public class BaseEnemy : BaseCharacter
{
    protected float health;
    protected int point;

    protected override void Init()
    {
        base.Init();

        this.LoadConfig();

        this.movingVector = Vector3.down;
    }

    protected override void LoadConfig()
    {
        EnemyConfig config = EnemyManager.Instance.GetConfigOfType(GameDefine.BASE_ENEMY_ID);

        this.health = config.Health;
        this.point = config.Point;
        this.speed = config.Speed;
    }

    public override void Move(float elapsedTime)
    {
        // switch to viewport's (main camera) normalized coordinate
        Vector3 viewportPos = GamePlayManager.Instance.ToViewportPos(this.transform.position);

        if (viewportPos.y < 0f) // 0f: viewport's bottom edge
        {
            this.DestroySelf();
        }
        else
        {
            base.Move(elapsedTime);
        }
    }

    public override void OnTakenDamage(float dmgTaken)
    {
        this.health -= dmgTaken;

        if (this.health <= 0f) this.OnDefeated();
    }

    public virtual void OnCollidedWithPlayer(Player player, float dmgTaken)
    {
        this.OnTakenDamage(dmgTaken);
    }

    public virtual void OnDefeated()
    {
        GamePlayManager.Instance.OnDefeatEnemy(this.point);

        this.DestroySelf();
    }
}
