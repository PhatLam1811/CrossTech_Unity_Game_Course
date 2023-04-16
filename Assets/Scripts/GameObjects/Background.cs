using UnityEngine;

public class Background : BaseGameObj
{
    [SerializeField] private Transform tfBuffer;

    private Vector3 reloadPos;

    private void Awake()
    {
        if (this.transform == this.tfBuffer)
        {
            // detach buffer to stop it from chasing its parent
            // the buffer will move in sync with the background (speed = 1.5f)
            this.transform.SetParent(null, true);
        }
    }

    protected override void Init()
    {
        base.Init();

        this.SetMovingVector(Vector3.down);
        this.SetSpeed(1.5f);

        this.reloadPos = this.tfBuffer.position;
    }

    protected override void Move(float elapsedTime)
    {
        // switch to viewport's (main camera) normalized coordinate
        Vector3 viewportPos = GamePlayManager.Instance.ToViewportPos(this.transform.position);

        if (viewportPos.y < -0.5f)
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
        this.transform.position = this.reloadPos;
    }
}
