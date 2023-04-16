using UnityEngine;

public class BaseEnemy : BaseCharacter
{
    protected float health;
    protected int point;

    protected override void Init()
    {
        base.Init();

        this.LoadConfig();

        this.SetMovingVector(Vector3.down);
    }

    // ==================================================

    #region Accessors
    public void SetHealth(float health) { this.health = health; }
    public float GetHealth() { return this.health; }

    public void SetPoint(int point) { this.point = point; }
    public int GetPoint() { return this.point; }
    #endregion

    // ==================================================

    protected override void LoadConfig()
    {
        EnemyConfig config = EnemyManager.Instance.GetConfigOfType(GameDefine.BASE_ENEMY_ID);

        this.SetHealth(config.Health);
        this.SetPoint(config.Point);
        this.SetSpeed(config.Speed);
    }

    protected override void Move(float elapsedTime)
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

    public virtual void OnCollidedWithPlayer(Player player, float dmgTaken)
    {
        this.OnTakenDamage(dmgTaken);
    }

    public override void OnTakenDamage(float dmgTaken)
    {
        this.health -= dmgTaken;

        if (this.health <= 0f) this.OnDefeated();
    }

    public virtual void OnDefeated()
    {
        GamePlayManager.Instance.OnDefeatEnemy(this.point);

        this.DestroySelf();
    }
}
