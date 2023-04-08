using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoSingleton<BulletManager>
{
    [SerializeField] private List<GameObject> pfBullets;

    private bool isGameOver;

    public void StartGame()
    {
        this.isGameOver = false;
    }

    public void GameOver()
    {
        this.isGameOver = true;
    }

    public void ShootBulletOfType(int type, Vector3 pos)
    {
        if (!this.isGameOver)
        {
            Instantiate(pfBullets[type], pos, Quaternion.identity);
        }
    }
}
