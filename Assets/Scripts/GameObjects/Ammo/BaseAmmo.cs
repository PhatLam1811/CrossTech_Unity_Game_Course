using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAmmo : BaseGameObj
{
    Camera mainCam;

    // Start is called before the first frame update
    void Start()
    {
        mainCam = Camera.main;
        movingVector = new Vector3(x: 0f, y: 1f);
        speed = 2f;
    }

    // Update is called once per frame
    void Update()
    {
        float elapsedTime = Time.deltaTime;

        Move(elapsedTime);
    }

    public override void Move(float elapsedTime)
    {
        // switch to viewport's (main camera) normalized coordinate
        Vector3 worldCoord = transform.position;
        Vector3 viewportCoord = mainCam.WorldToViewportPoint(worldCoord);

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
}
