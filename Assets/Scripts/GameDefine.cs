using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameDefine
{
    // data keys
    public const string PLAYER_INFO_DATA = "PLAYER_INFO_DATA";

    // player
    public const float DEFAULT_PLAYER_POS_X = 0f;   // player start at screen's center
    public const float DEFAULT_PLAYER_POS_Y = 0f;
    public const float DEFAULT_PLAYER_POS_Z = 0f;
    public const float DEFAULT_PLAYER_SPEED = 3f;
    public const float DEFAULT_PLAYER_ATK_CD = 0.7f;
    public const float DEFAULT_PLAYER_HP = 10f;
    public const int DEFAULT_PLAYER_SCORE = 0;
    public const int DEFAULT_PLAYER_SP_BULLET_1_AMT = 5;
    public const int DEFAULT_PLAYER_SP_BULLET_2_AMT = 5;

    // bullet
    public const int DEFAULT_BULLET_TYPE = 0;
    public const int HOMING_BULLET_TYPE = 1;
    public const int BUCKSHOT_BULLET_TYPE = 2;
    public const int BOSS_BULLET_TYPE = 3;

    // enemy
    public const int BASE_ENEMY_TYPE = 0;
    public const int BOSS_ENEMY_TYPE = 1;
    public const float BASE_ENEMY_SPAWN_INTERVAL = 1f;  // spawn base enemy every 1 second

    // others
    public const int DEFAULT_AUTO_SAVE_INTERVAL = 30;   // auto save every 30 seconds
}
