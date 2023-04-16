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

    public List<EnemyConfig> enemyConfigs;
}

[System.Serializable]
public class EnemyConfig
{
    public int enemyId;

    public float health;
    public float speed;
    public int point;

    // configurations for shootable enemies aka boss
    public float cooldown;
    public int bulletId;
}
