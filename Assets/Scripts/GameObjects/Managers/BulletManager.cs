using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoSingleton<BulletManager>
{
    [SerializeField] private List<GameObject> pfBullets;

    private bool isPlaying = false;

    public void StartGame()
    {
        this.isPlaying = true;
    }

    public void GameOver()
    {
        this.isPlaying = false;
    }

    public void ShootBulletOfType(int type, Vector3 pos)
    {
        if (isPlaying)
        {
            Instantiate(pfBullets[type], pos, Quaternion.identity);
        }
    }
}
