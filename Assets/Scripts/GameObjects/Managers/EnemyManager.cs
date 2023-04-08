using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoSingleton<EnemyManager>
{
    [SerializeField] private List<GameObject> pfEnemies;

    private bool isGameOver;

    private bool isBossAppeared;
    private float baseEnemyCD;

    private float untilBossCount;
    private Camera viewport;

    // Start is called before the first frame update
    void Start()
    {
        this.Init();
    }

    // Update is called once per frame
    void Update()
    {
        if (!this.isGameOver)
        {
            float elapsedTime = Time.deltaTime;

            this.SpawnEnemies(elapsedTime);
        }
    }

    public void StartGame()
    {
        this.isGameOver = false;
    }

    private void Init()
    {
        this.viewport = Camera.main;
        this.isBossAppeared = false;
        this.baseEnemyCD = GameDefine.BASE_ENEMY_SPAWN_INTERVAL;
        this.untilBossCount = 15; // boss appear after the 15th base enemy
        this.isGameOver = false;
    }

    // ==================================================

    private void SpawnEnemies(float elapsedTime)
    {
        this.baseEnemyCD -= elapsedTime;

        if (this.baseEnemyCD <= 0f)
        {
            Vector3 generatedPos = RandomizePosition();

            Instantiate(this.pfEnemies[GameDefine.BASE_ENEMY_TYPE], generatedPos, Quaternion.identity);

            if (this.untilBossCount > 0)
            {
                this.untilBossCount -= 1;
            }
            else
            {
                if (!this.isBossAppeared)
                {
                    this.SpawnBoss();
                    this.isBossAppeared = true;
                }
            }

            // reset cooldown
            this.baseEnemyCD = GameDefine.BASE_ENEMY_SPAWN_INTERVAL;
        }
    }

    private void SpawnBoss()
    {
        // set up x, y in viewport coordinate
        float yViewportPos = 1.1f;
        float xViewportPos = 0.5f;

        // change from viewport to world coordinate
        Vector3 viewportPos = new Vector3(x: xViewportPos, y: yViewportPos);
        Vector3 worldPos = this.viewport.ViewportToWorldPoint(viewportPos);

        // reset depth value (viewport's default depth is -10)
        worldPos.z = 0f;

        Instantiate(this.pfEnemies[GameDefine.BOSS_ENEMY_TYPE], worldPos, Quaternion.identity);
    }

    private Vector3 RandomizePosition()
    {
        // set up x, y in viewport coordinate
        float yViewportPos = 1.1f;
        float xViewportPos = Random.Range(0.1f, 0.9f);

        // change from viewport to world coordinate
        Vector3 viewportPos = new Vector3(x: xViewportPos, y: yViewportPos);
        Vector3 worldPos = this.viewport.ViewportToWorldPoint(viewportPos);

        // reset depth value (viewport's default depth is -10)
        worldPos.z = 0f;

        return worldPos;
    }

    // ==================================================

    public void OnCollidedWithPlayer(BaseEnemy enemy, Player player, float dmgTaken)
    {
        enemy.OnCollidedWithPlayer(player, dmgTaken);
    }

    public void OnCollidedWithBullet(BaseEnemy enemy, BaseBullet bullet, float dmgTaken)
    {
        enemy.OnCollidedWithBullet(bullet, dmgTaken);
    }

    public void GameOver()
    {
        this.isGameOver = true;
    }
}
