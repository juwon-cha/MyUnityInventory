using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : BaseUI
{
    [SerializeField] private TextMeshProUGUI inventoryInfoText;
    [SerializeField] private Transform itemParent;

    [SerializeField] private Button closeBtn;

    public override void SetInfo(BaseUIData uiData)
    {
        base.SetInfo(uiData);

        SetInventoryInfo();
    }

    private void SetInventoryInfo()
    {
        //var inventory = GameManager.Instance.Player.Inventory;
        //inventoryInfoText.text = $"Inventory {inventory.CurrentCount}/{inventory.MaxCount}";
        //foreach (Transform child in itemParent)
        //{
        //    Destroy(child.gameObject);
        //}
        //foreach (var item in inventory.GetAllItems())
        //{
        //    var itemObj = Instantiate(Resources.Load<GameObject>("Prefabs/UI/InventoryItem"), itemParent);
        //    var itemUI = itemObj.GetComponent<InventoryItemUI>();
        //    itemUI.SetItemInfo(item);
        //}

        closeBtn.onClick.AddListener(() =>
        {
            CloseUI();
        });
    }
}
