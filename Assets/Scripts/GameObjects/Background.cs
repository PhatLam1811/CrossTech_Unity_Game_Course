using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : BaseGameObj
{
    [SerializeField] private GameObject reloadCoord;

    protected override void Init()
    {
        base.Init();

        this.speed = 1f;
        this.movingVector = Vector3.down;
    }

    public override void Move(float elapsedTime)
    {
        // switch to viewport's (main camera) normalized coordinate
        Vector3 worldCoord = this.transform.position;
        Vector3 viewportCoord = this.viewport.WorldToViewportPoint(worldCoord);

        if (viewportCoord.y < -0.5f)
        {
            // reload if out of camera view
            this.Reload();
        }
        else
        {
            // scoll down if not out of camera view
            base.Move(elapsedTime);
        }
    }

    public Vector3 GetReloadCoord()
    {
        return this.reloadCoord.transform.position;
    }

    private void Reload()
    {
        this.transform.position = this.reloadCoord.transform.position;
    }
}
