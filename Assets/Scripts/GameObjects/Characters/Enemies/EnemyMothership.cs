using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMothership : BaseEnemy
{
    private const float baseEnemyInterval = 1f;

    private float baseEnemyCD;

    [SerializeField] private GameObject pfBaseEnemy;

    // Update is called once per frame
    protected override void Update()
    {
        float elapsedTime = GetElapsedTime();

        GenerateBaseEnemy(elapsedTime);
    }

    protected override void Init()
    {
        base.Init();

        baseEnemyCD = baseEnemyInterval;
    }

    private void GenerateBaseEnemy(float elapsedTime)
    {
        baseEnemyCD -= elapsedTime;

        if (baseEnemyCD < 0f)
        {
            Vector3 generatedPos = RandomizePosition();
            
            // generate a base enemy every 0.5s at a random location
            Instantiate(pfBaseEnemy, generatedPos, Quaternion.identity);

            // reset cooldown
            baseEnemyCD = baseEnemyInterval;
        }
    }

    private Vector3 RandomizePosition()
    {
        // set up x, y in viewport coordinate
        float yViewportPos = 1.1f;
        float xViewportPos = Random.Range(0.1f, 0.9f);

        // change from viewport to world coordinate
        Vector3 viewportPos = new Vector3(x: xViewportPos, y: yViewportPos);
        Vector3 worldPos = viewport.ViewportToWorldPoint(viewportPos);

        // reset depth value (viewport's default depth is -10)
        worldPos.z = 0f;

        return worldPos;
    }
}
