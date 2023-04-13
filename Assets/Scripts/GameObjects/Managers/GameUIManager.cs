using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUIManager : MonoSingleton<GameUIManager>
{
    private const float TEMP_DEFAULT_PLAYER_HP = 10f;

    [SerializeField] private Image imgHealthBar;
    [SerializeField] private Image imgHealth;
    [SerializeField] private Button btnSpAkt1;
    [SerializeField] private Button btnSpAkt2;
    [SerializeField] private TextMeshProUGUI txtSpBulletAmt1;
    [SerializeField] private TextMeshProUGUI txtSpBulletAmt2;
    [SerializeField] private TextMeshProUGUI txtScore;

    private bool isGameOver;

    // Start is called before the first frame update
    void Start()
    {
        this.SetActive(false);
    }

    public void StartGame(float health, int score, int spBullet1Amt, int spBullet2Amt)
    {
        this.imgHealth.fillAmount = health / TEMP_DEFAULT_PLAYER_HP;
        this.txtScore.text = score.ToString();
        this.txtSpBulletAmt1.text = "x" + spBullet1Amt.ToString();
        this.txtSpBulletAmt2.text = "x" + spBullet2Amt.ToString();
        this.btnSpAkt1.interactable = spBullet1Amt > 0;
        this.btnSpAkt2.interactable = spBullet2Amt > 0;

        this.SetActive();

        GamePlayManager.Instance.onGameOverCallback += this.GameOver;
        this.isGameOver = false;
    }

    public void GameOver()
    {
        GamePlayManager.Instance.onGameOverCallback -= this.GameOver;

        this.isGameOver = true;
    }

    public void SetActive(bool value = true)
    {
        this.imgHealthBar.gameObject.SetActive(value);
        this.imgHealth.gameObject.SetActive(value);
        this.txtScore.gameObject.SetActive(value);
        this.btnSpAkt1.gameObject.SetActive(value);
        this.btnSpAkt2.gameObject.SetActive(value);
        this.txtSpBulletAmt1.gameObject.SetActive(value);
        this.txtSpBulletAmt2.gameObject.SetActive(value);
    }

    // ==================================================

    public void OnPlayerHealthChange(float currentHealth)
    {
        if (this.isGameOver) return;

        this.imgHealth.fillAmount = currentHealth / TEMP_DEFAULT_PLAYER_HP;
    }

    public void OnPlayerScoreChange(int newScore)
    {
        if (this.isGameOver) return;

        this.txtScore.text = newScore.ToString();
    }

    // ==================================================

    public void OnSpecialAtk1BtnClicked()
    {
        if (this.isGameOver) return;

        GamePlayManager.Instance.OnInvokeSpecialAtk1();
    }

    public void OnSpecialAtk2BtnClicked()
    {
        if (this.isGameOver) return;

        GamePlayManager.Instance.OnInvokeSpecialAtk2();
    }


    // ==================================================

    public void OnOutOfSpBullets1(int remainingAmt)
    {
        if (this.isGameOver) return;

        if (remainingAmt <= 0)
        {
            this.btnSpAkt1.interactable = false;
        }

        this.txtSpBulletAmt1.text = "x" + remainingAmt.ToString();
    }

    public void OnOutOfSpBullets2(int remainingAmt)
    {
        if (this.isGameOver) return;

        if (remainingAmt <= 0)
        {
            this.btnSpAkt2.interactable = false;
        }

        this.txtSpBulletAmt2.text = "x" + remainingAmt.ToString();
    }
}
