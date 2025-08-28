using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusPresenter
{
    private IStatusView view;
    private InventoryData inventoryData;

    public StatusPresenter(IStatusView view, InventoryData model)
    {
        this.view = view;
        this.inventoryData = model;

        // View와 Model 이벤트 구독
        this.view.OnCloseButtonClicked += HandleCloseButtonClick;
        this.inventoryData.OnEquipmentChanged += UpdateView;

        // 최초 업데이트
        UpdateView();
    }

    public void Dispose()
    {
        this.view.OnCloseButtonClicked -= HandleCloseButtonClick;
        this.inventoryData.OnEquipmentChanged -= UpdateView;
    }

    private void HandleCloseButtonClick()
    {
        view.Close();
    }

    private void UpdateView()
    {
        // Model에서 총 스탯을 계산해서 View에 전달
        UserItemStats totalStats = inventoryData.GetUserTotalItemStats();
        view.UpdateStats(totalStats);
    }
}
