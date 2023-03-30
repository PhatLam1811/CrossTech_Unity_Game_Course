using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : BaseGameObj, ICollidable
{
    // Start is called before the first frame update
    void Start()
    {
        viewport = Camera.main;

        speed = 3.0f;
        movingVector = new Vector3(x: 0f, y: -1f);
    }

    // Update is called once per frame
    void Update()
    {
        float elapsedTime = GetElapsedTime();

        Move(elapsedTime);
    }

    public override void Move(float elapsedTime)
    {
        // switch to viewport's (main camera) normalized coordinate
        Vector3 worldPos = transform.position;
        Vector3 viewportPos = viewport.WorldToViewportPoint(worldPos);

        if (viewportPos.y < 0f)
        {
            // disappear if out of camera view from the bottom
            Destroy(gameObject);
        }
        else
        {
            // move normally if not out of camera view
            base.Move(elapsedTime);
        }
    }

    public void OnCollided(GameObject collidedObj)
    {
        Destroy(gameObject);
    }
}
