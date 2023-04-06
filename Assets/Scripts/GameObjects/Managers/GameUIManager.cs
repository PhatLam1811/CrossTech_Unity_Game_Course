using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameUIManager : MonoSingleton<GameUIManager>
{
    [SerializeField] private Image imgHealthBar;
    [SerializeField] private Button btnSpAkt1;
    [SerializeField] private Button btnSpAkt2;
    [SerializeField] private TMP_Text txtSpBulletAmt1;
    [SerializeField] private TMP_Text txtSpBulletAmt2;
    [SerializeField] private TMP_Text txtScore;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartGame(float health, int score, int spBullet1Amt, int spBullet2Amt)
    {
        this.imgHealthBar.fillAmount = health / GameDefine.DEFAULT_PLAYER_HP;
        this.txtScore.text = score.ToString();
        this.txtSpBulletAmt1.text = "x" + spBullet1Amt.ToString();
        this.txtSpBulletAmt2.text = "x" + spBullet2Amt.ToString();
        this.btnSpAkt1.interactable = spBullet1Amt > 0;
        this.btnSpAkt2.interactable = spBullet2Amt > 0;
    }


    // ==================================================

    public void OnPlayerHealthChange(float currentHealth)
    {
        this.imgHealthBar.fillAmount = currentHealth / GameDefine.DEFAULT_PLAYER_HP;
    }

    public void OnPlayerScoreChange(int newScore)
    {
        this.txtScore.text = newScore.ToString();
    }

    // ==================================================

    public void OnSpecialAtk1BtnClicked()
    {
        GamePlayManager.Instance.OnInvokeSpecialAtk1();
    }

    public void OnSpecialAtk2BtnClicked()
    {
        GamePlayManager.Instance.OnInvokeSpecialAtk2();
    }


    // ==================================================

    internal void OnOutOfSpBullets1(int remainingAmt)
    {
        if (remainingAmt <= 0)
        {
            this.btnSpAkt1.interactable = false;
        }

        this.txtSpBulletAmt1.text = "x" + remainingAmt.ToString();
    }

    internal void OnOutOfSpBullets2(int remainingAmt)
    {
        if (remainingAmt <= 0)
        {
            this.btnSpAkt1.interactable = false;
        }

        this.txtSpBulletAmt2.text = "x" + remainingAmt.ToString();
    }
}