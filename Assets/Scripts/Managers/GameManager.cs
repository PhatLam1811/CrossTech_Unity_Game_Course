using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField] private Transform canvasPos;

    private Dictionary<int, EnemyConfig> enemyConfigs;
    private Dictionary<int, BulletConfig> bulletConfigs;

    void Start()
    {
        this.OpenApp();
    }

    public void OpenApp()
    {
        this.LoadEnemyConfigs();
        this.LoadBulletConfigs();
        this.OnShowDialog<StartDialog>(GameDefine.START_DIALOG_PATH);
    }

    // ==================================================

    public void StartGame()
    {
        GameDataManager.Instance.StartGame();
    }

    public void GameOver()
    {
        this.OnShowDialog<HighscoreDialog>(GameDefine.HIGHSCORE_DIALOG_PATH, data: PlayerData.Instance.highScores);
    }

    public void ReplayGame()
    {
        GamePlayManager.Instance.ReplayGame();
    }

    // ==================================================

    private void LoadEnemyConfigs()
    {
        this.enemyConfigs = new Dictionary<int, EnemyConfig>();

        if (EnemyConfigs.Instance.enemyConfigs != null)
        {
            foreach (EnemyConfig config in EnemyConfigs.Instance.enemyConfigs)
            {
                this.enemyConfigs[config.enemyId] = config;
            }

            Debug.Log("Successfully loadded enemy configs!!!");
        }
        else
        {
            // this shouldn't happen
            Debug.LogError("Null enemy configs dictionary!!!");
        }
    }

    private void LoadBulletConfigs()
    {
        this.bulletConfigs = new Dictionary<int, BulletConfig>();

        if (BulletConfigs.Instance.bulletConfigs != null)
        {
            foreach (BulletConfig config in BulletConfigs.Instance.bulletConfigs)
            {
                this.bulletConfigs[config.bulletId] = config;
            }

            Debug.Log("Successfully loadded bullet configs!!!");
        }
        else
        {
            // this shouldn't happen
            Debug.LogError("Null bullet configs dictionary!!!");
        }
    }

    public EnemyConfig GetEnemyConfigOfType(int enemyId)
    {
        if (this.enemyConfigs[enemyId] != null)
        {
            return this.enemyConfigs[enemyId];
        }
        else
        {
            Debug.LogError("Unknown enemy type!!!! - type : " + enemyId); return null;
        }
    }

    public BulletConfig GetBulletConfigOfType(int bulletId)
    {
        if (this.bulletConfigs[bulletId] != null)
        {
            return this.bulletConfigs[bulletId];
        }
        else
        {
            Debug.LogError("Unknown enemy type!!!! - type : " + bulletId); return null;
        }
    }

    // ==================================================

    public void OnShowDialog<T>(string path, object data = null, UnityEngine.Events.UnityAction callbackCompleteShow = null) where T : BaseDialog
    {
        GameObject dialogPrefab = this.GetResourceFile<GameObject>(path);

        if (dialogPrefab != null)
        {
            T dialogComponent = (Instantiate(dialogPrefab, this.canvasPos)).GetComponent<T>();

            if (dialogComponent != null)
            {
                dialogComponent.OnShow(data, callbackCompleteShow);
            }
        }
    }

    // ==================================================

    public T GetResourceFile<T>(string path) where T : UnityEngine.Object
    {
        return Resources.Load<T>(path) as T;
    }
}
