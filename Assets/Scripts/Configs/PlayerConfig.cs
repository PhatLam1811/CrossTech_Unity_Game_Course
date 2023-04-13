using UnityEngine;

[CreateAssetMenu(menuName = "Games/PlayerConfig", fileName = "PlayerConfig")]
public class PlayerConfig : ScriptableObject
{
    #region Singleton
    private static PlayerConfig _instance;

    public static PlayerConfig Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameManager.Instance.GetResourceFile<PlayerConfig>(GameDefine.PLAYER_CONFIG_FILE_PATH);

                if (_instance == null)
                {
                    Debug.LogError("Couldn't load resource file of type : " + typeof(PlayerConfig).ToString());
                }
            }
            return _instance;
        }
    }
    #endregion

    [SerializeField] private float health;
    [SerializeField] private float speed;
    [SerializeField] private float cooldown;

    [SerializeField] private int defaultBulletId;

    [SerializeField] private int specialBullet1Id;
    [SerializeField] private int specialBullet2Id;

    [SerializeField] private int specialBullet1InitAmount;
    [SerializeField] private int specialBullet2InitAmount;

    public float Health { get => this.health; }
    public float Speed { get => this.speed; }
    public float Cooldown { get => this.cooldown; }

    public int DefaultBulletId { get => this.defaultBulletId; }
    
    public int SpecialBullet1Id { get => this.specialBullet1Id; }
    public int SpecialBullet2Id { get => this.specialBullet2Id; }
    
    public int SpecialBullet1InitAmount { get => this.specialBullet1InitAmount; }
    public int SpecialBullet2InitAmount { get => this.SpecialBullet1InitAmount; }
}
