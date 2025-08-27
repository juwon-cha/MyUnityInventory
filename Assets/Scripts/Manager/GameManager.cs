using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonBehaviour<GameManager>
{
    [SerializeField] private Character playerCharacter;
    public Character PlayerCharacter { get; private set; }

    protected override void Init()
    {
        base.Init();

        DataTableManager.Instance.LoadDataTables();

        PlayerCharacter = playerCharacter;
    }

    private void Start()
    {
        UIManager.Instance.OpenUI<MainMenuUI>(new BaseUIData());
    }
}
