using UnityEngine;

public class ExplodedBuckshot : BaseBullet
{
    protected override void Init()
    {
        this.viewport = Camera.main;
        this.damage = 1;

        GamePlayManager.Instance.onGameOverCallback += this.GameOver;
        GamePlayManager.Instance.onGameReplayCallback += this.OnReplayGame;

        this.isGameOver = false;
    }
}
