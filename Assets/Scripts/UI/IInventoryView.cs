using System;
using System.Collections;
using System.Collections.Generic;

// Presenter가 View를 제어하기 위해 사용하는 메서드와 이벤트 정의
public interface IInventoryView
{
    // View에서 발생하는 사용자 입력 이벤트를 Presenter에게 알림
    public event Action<int> OnItemSlotClicked;
    public event Action OnCloseButtonClicked;

    // Presenter가 View에게 UI 업데이트를 지시하는 메서드
    public void DisplayItems(List<InventoryItemSlotData> items);
    public void UpdateInventoryInfo(string infoText);
    public void Close();
}
