using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : BaseUI, IInventoryView
{
    [SerializeField] private TextMeshProUGUI inventoryInfoText;
    [SerializeField] private Transform itemParent;
    [SerializeField] private Button closeBtn;

    private InventoryPresenter presenter;

    // Constants
    private const string IITEM_SLOT_PATH = "Prefabs/UI/ItemSlot";

    public event Action<int> OnItemSlotClicked;
    public event Action OnCloseButtonClicked;

    public override void SetInfo(BaseUIData uiData)
    {
        base.SetInfo(uiData);

        // Model을 가져와 Presenter를 생성하고 View(자기 자신)와 연결
        var model = GameManager.Instance.PlayerCharacter.Inventory;
        presenter = new InventoryPresenter(this, model);

        closeBtn.onClick.RemoveAllListeners();
        closeBtn.onClick.AddListener(() => OnCloseButtonClicked?.Invoke());
    }

    public void DisplayItems(List<InventoryItemSlotData> items)
    {
        // Presenter의 지시에 따라 화면을 그리는 역할만 수행
        foreach (Transform child in itemParent)
        {
            Destroy(child.gameObject);
        }

        foreach (var slotData in items)
        {
            var itemObj = Instantiate(Resources.Load<GameObject>(IITEM_SLOT_PATH), itemParent);
            var slotView = itemObj.GetComponent<InventoryItemSlot>();

            if (slotView != null)
            {
                // 슬롯 View 초기화
                slotView.Initialize(slotData);

                // 슬롯의 클릭 이벤트 구독 -> 상위 Presenter에게 그대로 전달
                var view = (IInventoryItemSlotView)slotView;
                view.OnClicked += () => OnItemSlotClicked?.Invoke(slotData.ItemID);
            }
        }
    }

    public void UpdateInventoryInfo(string infoText)
    {
        // Presenter가 시키는 대로 텍스트 업데이트
        inventoryInfoText.text = infoText;
    }

    public void Close()
    {
        // Presenter의 지시에 따라 UI 닫음
        CloseUI();
    }

    // UI가 파괴될 때 Presenter도 정리 -> 메모리 누수 방지
    private void OnDestroy()
    {
        presenter?.Dispose();
    }
}
