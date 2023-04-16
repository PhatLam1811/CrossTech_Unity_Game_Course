using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField] private Transform dialogPosition;

    private Dictionary<int, EnemyConfig> enemyConfigs;

    void Start()
    {
        this.OpenApp();
    }

    public void OpenApp()
    {
        this.OnShowDialog<StartDialog>(GameDefine.START_DIALOG_PATH);
    }

    // ==================================================

    public void StartGame()
    {
        this.LoadEnemyConfigs();
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

        if (EnemyConfigs.Instance.Enemies != null)
        {
            foreach (EnemyConfig config in EnemyConfigs.Instance.Enemies)
            {
                this.enemyConfigs[config.TypeId] = config;
            }
        }
        else
        {
            // this shouldn't happen
            Debug.LogError("Null enemy configs!!!");
        }
    }

    public EnemyConfig GetEnemyConfigOfType(int enemyTypeId)
    {
        if (this.enemyConfigs[enemyTypeId] != null)
        {
            return this.enemyConfigs[enemyTypeId];
        }
        else
        {
            Debug.LogError("Unknown enemy type!!!! - type : " + enemyTypeId); return null;
        }
    }

    // ==================================================

    public void OnShowDialog<T>(string path, object data = null, UnityEngine.Events.UnityAction callbackCompleteShow = null) where T : BaseDialog
    {
        GameObject dialogPrefab = this.GetResourceFile<GameObject>(path);

        if (dialogPrefab != null)
        {
            T dialogComponent = (Instantiate(dialogPrefab, dialogPosition)).GetComponent<T>();

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
