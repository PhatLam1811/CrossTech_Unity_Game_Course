using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAmmo : BaseGameObj, ICollidable
{
    // Start is called before the first frame update
    void Start()
    {
        viewport = Camera.main;

        speed = 2f;
        movingVector = new Vector3(x: 0f, y: 1f);
    }

    // Update is called once per frame
    void Update()
    {
        float elapsedTime = GetElapsedTime();

        Move(elapsedTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        onCollided(collision.gameObject);
    }

    public override void Move(float elapsedTime)
    {
        // switch to viewport's (main camera) normalized coordinate
        Vector3 worldCoord = transform.position;
        Vector3 viewportCoord = viewport.WorldToViewportPoint(worldCoord);

        if (viewportCoord.y >= 0f && viewportCoord.y <= 1f )
        {
            // move normally if not out of camera view
            base.Move(elapsedTime);
        }
        else
        {
            // disappear if out of camera view
            Destroy(gameObject);
        }
    }

    public void onCollided(GameObject collidedObj)
    {
        if (collidedObj.TryGetComponent<BaseEnemy>(out BaseEnemy collidedEnemy))
        {
            Destroy(gameObject);
            collidedEnemy.onCollided(gameObject);
        }
    }
}
