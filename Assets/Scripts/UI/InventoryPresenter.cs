using System.Collections.Generic;
using System.Linq;

public class InventoryPresenter
{
    private IInventoryView view;
    private InventoryData inventoryData;

    // 생성자에서 Model과 View를 주입받고 이벤트 연결
    public InventoryPresenter(IInventoryView view, InventoryData model)
    {
        this.view = view;
        this.inventoryData = model;

        // View와 Model로부터 오는 이벤트 구독
        this.view.OnItemSlotClicked += HandleItemSlotClick;
        this.view.OnCloseButtonClicked += HandleCloseButtonClick;
        this.inventoryData.OnInventoryChanged += UpdateView;

        // Presenter가 생성될 때 View 초기화
        UpdateView();
    }

    // Presenter가 파괴될 때 이벤트 구독 해제 (메모리 누수 방지)
    public void Dispose()
    {
        this.view.OnItemSlotClicked -= HandleItemSlotClick;
        this.view.OnCloseButtonClicked -= HandleCloseButtonClick;
        this.inventoryData.OnInventoryChanged -= UpdateView;
    }

    private void HandleItemSlotClick(int itemID)
    {
        // View의 입력을 받아 Model의 상태 변경을 요청
        inventoryData.ToggleEquipItem(itemID);
    }

    private void HandleCloseButtonClick()
    {
        // View 닫기 요청 처리
        view.Close();
    }

    private void UpdateView()
    {
        // Model의 데이터가 변경되면 데이터를 가공하여 View의 업데이트 지시
        var allItems = inventoryData.GetAllItems();
        var slotDataList = allItems.Select(item => new InventoryItemSlotData
        {
            ItemID = item.ItemID,
            IsEquipped = inventoryData.IsEquipped(item.ItemID)
        }).ToList();

        view.DisplayItems(slotDataList);

        string infoText = $"Inventory {inventoryData.items.Count} / {inventoryData.MaxItemCount}";
        view.UpdateInventoryInfo(infoText);
    }
}