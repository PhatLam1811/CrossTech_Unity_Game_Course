using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    // Start is called before the first frame update
    void Start()
    {
        OpenApp();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenApp()
    {
        GameDataManager.Instance.OpenApp();
    }
}
