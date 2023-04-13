using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    private const string HIGHSCORE_DIALOG_PATH = "UI Elements/Highscore Dialog";
    private const string START_DIALOG_PATH = "UI Elements/Start Dialog";

    [SerializeField] private Transform _dialogPos;

    void Start()
    {
        this.OpenApp();
    }

    public void OpenApp()
    {
        this.OnShowDialog<StartDialog>(START_DIALOG_PATH);
    }

    // ==================================================

    public void StartGame()
    {
        GameDataManager.Instance.StartGame();
    }

    public void GameOver()
    {
        this.OnShowDialog<HighscoreDialog>(HIGHSCORE_DIALOG_PATH, data: PlayerData.Instance.highScores);
    }

    public void ReplayGame()
    {
        GamePlayManager.Instance.ReplayGame();
    }

    // ==================================================

    public void OnShowDialog<T>(string path, object data = null, UnityEngine.Events.UnityAction callbackCompleteShow = null) where T : BaseDialog
    {
        GameObject dialogPrefab = this.GetResourceFile<GameObject>(path);

        if (dialogPrefab != null)
        {
            T dialogComponent = (Instantiate(dialogPrefab, _dialogPos)).GetComponent<T>();
            
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
