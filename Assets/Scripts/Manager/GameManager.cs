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

        PlayerCharacter = playerCharacter;
    }

    private void Start()
    {
        DataManager.Instance.LoadDataTables();

        if(PlayerCharacter != null)
        {
            PlayerCharacter.AcquireAllItems();
        }

        UIManager.Instance.OpenUI<MainMenuUI>(new BaseUIData());
    }
}
