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

    private InventoryData inventoryData;

    // Constants
    private const string IITEM_SLOT_PATH = "Prefabs/UI/ItemSlot";

    public override void SetInfo(BaseUIData uiData)
    {
        base.SetInfo(uiData);

        if(GameManager.Instance.PlayerCharacter != null)
        {
            InitInventoryUI(GameManager.Instance.PlayerCharacter.Inventory);
        }

        closeBtn.onClick.RemoveAllListeners(); // 기존 리스너 중복 등록 방지
        closeBtn.onClick.AddListener(() =>
        {
            CloseUI();
        });
    }

    private void InitInventoryUI(InventoryData inventory)
    {
        // 다른 인벤토리를 표시하게 될 경우를 대비해 기존 이벤트 구독 해제
        if (inventoryData != null)
        {
            inventoryData.OnInventoryChanged -= UpdateInventoryUI;
        }

        inventoryData = inventory;

        // 인벤토리에 아이템이 추가되거나 삭제될 때마다 UpdateInventoryUI가 자동으로 호출되도록 이벤트 구독
        inventoryData.OnInventoryChanged += UpdateInventoryUI;

        // UI가 처음 열릴 때 현재 인벤토리 상태를 기준으로 UI 즉시 업데이트
        UpdateInventoryUI();
    }

    private void UpdateInventoryUI()
    {
        if (inventoryData == null)
        {
            Debug.LogError("InventoryData is not set!");
            return;
        }

        // 인벤토리 아이템 개수 텍스트 업데이트
        inventoryInfoText.text = $"Inventory {inventoryData.items.Count} / {inventoryData.MaxItemCount}";

        // 이전에 생성했던 모든 슬롯 오브젝트들 파괴 -> UI 초기화
        foreach (Transform child in itemParent)
        {
            Destroy(child.gameObject);
        }

        // 현재 인벤토리에 있는 모든 아이템을 순회
        foreach (var item in inventoryData.GetAllItems())
        {
            // 슬롯 프리팹을 itemParent 아래에 생성
            var itemObj = Instantiate(Resources.Load<GameObject>($"{IITEM_SLOT_PATH}"), itemParent);

            // 생성한 오브젝트에서 InventoryItemSlot 컴포넌트를 가져옴
            InventoryItemSlot slot = itemObj.GetComponent<InventoryItemSlot>();

            if (slot != null)
            {
                // 슬롯에 필요한 데이터(ItemID)를 담아 객체 생성
                var slotData = new InventoryItemSlotData { ItemID = item.item_id };

                // 슬롯에 데이터를 전달하여 이미지 표시
                slot.UpdateSlotData(slotData);
            }
        }
    }

    private void OnDestroy()
    {
        if (inventoryData != null)
        {
            inventoryData.OnInventoryChanged -= UpdateInventoryUI;
        }
    }
}
