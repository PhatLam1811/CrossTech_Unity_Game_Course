using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Games/BulletConfigs", fileName = "BulletConfigs")]
public class BulletConfigs : ScriptableObject
{
    #region Singleton
    private static BulletConfigs _instance;

    public static BulletConfigs Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameManager.Instance.GetResourceFile<BulletConfigs>(GameDefine.BULLET_CONFIG_FILE_PATH);

                if (_instance == null)
                {
                    Debug.LogError("Couldn't load resource file of type : " + typeof(BulletConfigs).ToString());
                }
            }
            return _instance;
        }
    }
    #endregion

    public List<BulletConfig> bulletConfigs;
}

[System.Serializable]
public class BulletConfig
{
    public int bulletId;

    public float speed;
    public float damage;

    // homing bullet configs
    public float homingSpeed;

    // buckshot bullet configs
    public float eplosionCountdown;
    public float explodedSpeed;
}
