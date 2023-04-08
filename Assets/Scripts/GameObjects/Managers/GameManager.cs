using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField] private Transform _dialogPosition;

    // Start is called before the first frame update
    void Start()
    {
        this.OpenApp();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenApp()
    {
        GameDataManager.Instance.OpenApp();
    }

    public void OnShowDialog<T>(string path, object data = null, UnityEngine.Events.UnityAction callbackCompleteShow = null) where T : BaseDialog
    {
        GameObject dialogPrefab = this.GetResourceFile<GameObject>(path);

        if (dialogPrefab != null)
        {
            T dialogComponent = (Instantiate(dialogPrefab, _dialogPosition)).GetComponent<T>();
            
            if (dialogComponent != null)
            {
                dialogComponent.OnShow(data, callbackCompleteShow);
            }
        }
    }

    public T GetResourceFile<T>(string path) where T : UnityEngine.Object
    {
        return Resources.Load<T>(path) as T;
    }
}
