using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Games/EnemyConfigs", fileName = "EnemyConfigs")]
public class EnemyConfigs : ScriptableObject
{
    #region Singleton
    private static EnemyConfigs _instance;

    public static EnemyConfigs Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameManager.Instance.GetResourceFile<EnemyConfigs>(GameDefine.ENEMY_CONFIGS_FILE_PATH);

                if (_instance == null)
                {
                    Debug.LogError("Couldn't load resource file of type : " + typeof(EnemyConfigs).ToString());
                }
            }
            return _instance;
        }
    }
    #endregion

    [SerializeField] private List<EnemyConfig> enemies;

    public List<EnemyConfig> Enemies { get => this.enemies; }
}

[System.Serializable]
public class EnemyConfig
{
    [SerializeField] private int typeId;

    [SerializeField] private float health;
    [SerializeField] private float speed;
    [SerializeField] private int point;

    // configurations for shootable enemies aka boss
    [SerializeField] private float cooldown;
    [SerializeField] private int bulletId;

    public int TypeId { get => this.typeId;}
    public float Health { get => this.health; }
    public float Speed { get => this.speed; }
    public int Point { get => this.point; }
    public float Cooldown { get => this.cooldown; }
    public int BulletId { get => this.bulletId; }
}
