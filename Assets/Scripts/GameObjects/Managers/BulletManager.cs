using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoSingleton<BulletManager>
{
    [SerializeField] private List<GameObject> pfBullets;

    private bool isGameOver;

    private void Start()
    {
        this.isGameOver = true;
    }

    public void StartGame()
    {
        GamePlayManager.Instance.onGameOverCallback += this.GameOver;
        this.isGameOver = false;
    }

    public void GameOver()
    {
        GamePlayManager.Instance.onGameOverCallback -= this.GameOver;

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
