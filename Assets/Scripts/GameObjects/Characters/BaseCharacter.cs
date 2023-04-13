public class BaseCharacter : BaseGameObj
{
    protected virtual void LoadConfig() { }

    public virtual void Attack(float elapsedTime) { }

    public virtual void OnTakenDamage(float dmgTaken) { }

    public virtual void OnCollidedWithBullet(BaseBullet bullet, float dmgTaken)
    {
        this.OnTakenDamage(dmgTaken);
    }
}
