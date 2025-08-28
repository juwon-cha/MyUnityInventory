using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatusUI : BaseUI, IStatusView
{
    [SerializeField] private TextMeshProUGUI attackText;
    [SerializeField] private TextMeshProUGUI defenseText;
    [SerializeField] private TextMeshProUGUI hpText;
    [SerializeField] private TextMeshProUGUI criticalText;
    [SerializeField] private Button closeBtn;

    private StatusPresenter presenter;

    public event Action OnCloseButtonClicked;

    public override void SetInfo(BaseUIData uiData)
    {
        base.SetInfo(uiData);

        if(presenter == null)
        {
            var model = GameManager.Instance.PlayerCharacter.Inventory;
            presenter = new StatusPresenter(this, model);

            closeBtn.onClick.RemoveAllListeners();
            closeBtn.onClick.AddListener(() => OnCloseButtonClicked?.Invoke());
        }
    }

    public void UpdateStats(UserItemStats totalStats)
    {
        // Presenter가 전달한 데이터로 UI를 그리기만 함
        attackText.text = $"공격력\n{totalStats.attack_power}";
        defenseText.text = $"방어력\n{totalStats.defense}";
        hpText.text = $"체력\n{totalStats.health}";
        criticalText.text = $"치명타\n{totalStats.critical}";
    }

    public void Close()
    {
        CloseUI();
    }

    private void OnDestroy()
    {
        presenter?.Dispose();
    }
}
