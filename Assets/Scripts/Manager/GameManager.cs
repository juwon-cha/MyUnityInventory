using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonBehaviour<GameManager>
{
    protected override void Init()
    {
        base.Init();
    }

    private void Awake()
    {
        DataManager.Instance.LoadDataTables();
    }

    private void Start()
    {
        UIManager.Instance.OpenUI<MainMenuUI>(new BaseUIData());
    }
}
