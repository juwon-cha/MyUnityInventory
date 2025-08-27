using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatusUI : BaseUI
{
    [SerializeField] private TextMeshProUGUI attackText;
    [SerializeField] private TextMeshProUGUI defenseText;
    [SerializeField] private TextMeshProUGUI hpText;
    [SerializeField] private TextMeshProUGUI criticalText;

    [SerializeField] private Button closeBtn;

    private InventoryData inventoryData;

    public override void SetInfo(BaseUIData uiData)
    {
        base.SetInfo(uiData);

        SetStatusInfo();

        closeBtn.onClick.AddListener(() =>
        {
            CloseUI();
        });
    }

    private void SetStatusInfo()
    {
        inventoryData = GameManager.Instance.PlayerCharacter.Inventory;
        if(inventoryData == null)
        {
            Debug.LogError("InventoryData is not set!");
            return;
        }

        UserItemStats userTotalItemStats = inventoryData.GetUserTotalItemStats();

        attackText.text = $"공격력\n{userTotalItemStats.attack_power}";
        defenseText.text = $"방어력\n{userTotalItemStats.defense}";
        hpText.text = $"체력\n{userTotalItemStats.health}";
        criticalText.text = $"치명타\n{userTotalItemStats.critical}";
    }
}
