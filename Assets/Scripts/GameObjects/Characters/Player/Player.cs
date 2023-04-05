using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player : BaseCharacter
{
    private const float atkInterval = 1f;

    private float cooldown;
    private int currentBullet;
    private int score;
    private int homingBulletAmount;
    private int buckshotBulletAmount;

    [SerializeField] private GameObject pfGunBarrel;
    [SerializeField] private List<GameObject> pfBullets;
    public Image imgHealthBar;
    public Button btnSpecialAkt1;
    public Button btnSpecialAkt2;
    public TMP_Text txtHomingAmount;
    public TMP_Text txtBuckshotAmount;
    public TMP_Text txtScore;

    // Update is called once per frame
    protected override void Update()
    {
        float elapsedTime = GetElapsedTime();

        ProcessInput();

        Move(elapsedTime);

        Attack(elapsedTime);
    }

    void OnTriggerEnter2D(Collider2D collision) 
    { 
        if (collision.TryGetComponent(out BaseEnemy enemy))
        {
            OnColliedWithEnemy();
        }
    }

    public void SetCurrentBullet(int currentBullet)
    {
        if (currentBullet < 0)
        {
            this.currentBullet = pfBullets.Count - 1;
        }
        else if (currentBullet >= pfBullets.Count)
        {
            this.currentBullet = 0;
        }
        else
        {
            this.currentBullet = currentBullet;
        }    
    }
    public void SetScore(int point) 
    { 
        score += point;
        txtScore.text = score.ToString();
    }

    public void ProcessInput()
    {
        // ======================================
        // Move
        // ======================================
        // get normalized moving input directions
        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");

        SetMovingVector(new Vector3(x: hor, y: ver));

        if (Input.GetKeyDown(KeyCode.Q))
        {
            // previous type
            SetCurrentBullet(currentBullet - 1);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            // next type
            SetCurrentBullet(currentBullet + 1);
        }
    }

    protected override void Init()
    {
        base.Init();

        health = 10;
        currentBullet = 0;
        cooldown = atkInterval;
        speed = 3f;
        score = 0;
        imgHealthBar.fillAmount = health / 10f;
        homingBulletAmount = 5;
        buckshotBulletAmount = 5;
        txtHomingAmount.text = "x" + homingBulletAmount.ToString();
        txtBuckshotAmount.text = "x" + buckshotBulletAmount.ToString();
        txtScore.text = score.ToString();
    }

    public override void Attack(float elapsedTime)
    {
        cooldown -= elapsedTime;

        // shoot a bullet after cooldown
        if (cooldown <= 0.0f)
        {
            Vector3 barrelPos = pfGunBarrel.transform.position;

            Instantiate(pfBullets[currentBullet], barrelPos, Quaternion.identity);

            // reset cooldown
            cooldown = atkInterval;
        }
    }

    public void SpecialAttack1()
    {
        // Debug.Log("Btn 1 Cliked!");

        Vector3 barrelPos = pfGunBarrel.transform.position;

        Instantiate(pfBullets[1], barrelPos, Quaternion.identity);

        homingBulletAmount -= 1;
        txtHomingAmount.text = "x" + homingBulletAmount.ToString();

        if (homingBulletAmount == 0) btnSpecialAkt1.interactable = false;
    }

    public void SpecialAttack2()
    {
        Debug.Log("Btn 1 Cliked!");

        Vector3 barrelPos = pfGunBarrel.transform.position;

        Instantiate(pfBullets[2], barrelPos, Quaternion.identity);

        buckshotBulletAmount -= 1;
        txtBuckshotAmount.text = "x" + buckshotBulletAmount.ToString();

        if (buckshotBulletAmount == 0) btnSpecialAkt2.interactable = false;
    }

    public void Save()
    {
        Data data = new Data();
        data.score = this.score;

        string jsonData = JsonUtility.ToJson(data);

        PlayerPrefs.SetString("testSave", jsonData);

        Debug.Log("Save!");
    }

    public void Load()
    {
        string jsonData = PlayerPrefs.GetString("testSave");

        Data data = JsonUtility.FromJson<Data>(jsonData);

        Debug.Log("Hight score = " + data.score);
    }

    public virtual void OnColliedWithEnemy()
    {
        OnDamaged(1);
    }

    public override void OnDamaged(int dmg)
    {
        base.OnDamaged(dmg);

        imgHealthBar.fillAmount = health / 10f;
    }
}

[System.Serializable]
public class Data //ko được kế thừa từ monobehavior
{
    //biến lưu phải là public
    public int score;
}
