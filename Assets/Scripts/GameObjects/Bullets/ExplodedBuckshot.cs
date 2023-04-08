using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodedBuckshot : BaseBullet
{
    protected override void Init() 
    { 
        viewport = Camera.main;
        damage = 1;

        GamePlayManager.Instance.onGameOverCallback += this.GameOver;
        this.isGameOver = false;
    }
}
