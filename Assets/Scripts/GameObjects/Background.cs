using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : BaseGameObj
{
    [SerializeField] private GameObject reloadCoord;

    protected override void Init()
    {
        base.Init();

        speed = 1f;
        movingVector = Vector3.down;
    }

    public override void Move(float elapsedTime)
    {
        // switch to viewport's (main camera) normalized coordinate
        Vector3 worldCoord = transform.position;
        Vector3 viewportCoord = viewport.WorldToViewportPoint(worldCoord);

        if (viewportCoord.y < -0.5f)
        {
            // reload if out of camera view
            Reload();
        }
        else
        {
            // scoll down if not out of camera view
            base.Move(elapsedTime);
        }
    }

    void Reload()
    {
        transform.position = reloadCoord.transform.position;
    }
}
