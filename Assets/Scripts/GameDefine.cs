public static class GameDefine
{
    // data keys
    public const string PLAYER_INFO_DATA = "PLAYER_INFO_DATA";

    // path
    public const string HIGHSCORE_DIALOG_PATH = "UI Elements/Highscore Dialog";
    public const string START_DIALOG_PATH = "UI Elements/Start Dialog";
    public const string ENEMY_CONFIGS_FILE_PATH = "Configs/EnemyConfigs";
    public const string PLAYER_CONFIG_FILE_PATH = "Configs/PlayerConfig";
    public const string BULLET_CONFIG_FILE_PATH = "Configs/BulletConfigs";

    // bullet
    public const int DEFAULT_BULLET_ID = 0;
    public const int HOMING_BULLET_ID = 1;
    public const int BUCKSHOT_BULLET_ID = 2;
    public const int BOSS_BULLET_ID = 3;

    // enemy
    public const int BASE_ENEMY_ID = 0;
    public const int BOSS_ENEMY_ID = 1;

    // others
    public const int DEFAULT_AUTO_SAVE_INTERVAL = 30;   // auto save every 30 seconds
    public const float DEFAULT_GAME_OBJECT_SPEED = 0f;
    public const float DEFAULT_GAME_OBJECT_DAMAGE = 0f;
}
