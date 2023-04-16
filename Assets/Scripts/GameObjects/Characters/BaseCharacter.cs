public class BaseCharacter : BaseGameObj
{
    public virtual void OnTakenDamage(float dmgTaken) { }

    public virtual void OnCollidedWithBullet(BaseBullet bullet)
    {
        float damageTaken = bullet.GetDamageInflict();

        this.OnTakenDamage(damageTaken);
    }
}
