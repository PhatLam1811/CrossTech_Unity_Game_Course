using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Games/PlayerConfig", fileName = "PlayerConfig")]
public class PlayerConfig : ScriptableObject
{
    private const string PLAYER_CONFIG_FILE_PATH = "Configs/PlayerConfig";

    #region Singleton
    private static PlayerConfig _instance;

    public static PlayerConfig Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameManager.Instance.GetResourceFile<PlayerConfig>(PLAYER_CONFIG_FILE_PATH);

                if (_instance == null)
                {
                    Debug.LogError("Couldn't load resource file of type : " + typeof(PlayerConfig).ToString());
                }
            }
            return _instance;
        }
    }
    #endregion

    public float health;
    public float speed;
    public float cooldown;

    public int defaultBulletId;
    
    public int specialBullet1Id;
    public int specialBullet2Id;
    
    public int specialBullet1InitAmount;
    public int specialBullet2InitAmount;
}
