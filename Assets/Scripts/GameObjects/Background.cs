using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : BaseGameObj
{
    [SerializeField] private Transform bufferTransform;

    private Vector3 reloadPosition;

    private void Awake()
    {
        if (this.transform == this.bufferTransform)
        {
            // detach buffer to stop it from chasing its parent
            // the buffer will move in sync with the background (speed = 1.5f)
            this.transform.SetParent(null, true);
        }
    }

    protected override void Init()
    {
        base.Init();

        this.speed = 1.5f;
        this.movingVector = Vector3.down;
        this.reloadPosition = this.bufferTransform.position;
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

    private void Reload()
    {
        this.transform.position = this.reloadPosition;
    }
}
