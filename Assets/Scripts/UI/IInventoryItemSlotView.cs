using System;
using UnityEngine;

// 개별 아이템 슬롯 Presenter가 View를 제어하기 위한 인터페이스
public interface IInventoryItemSlotView
{
    // View에서 클릭 이벤트가 발생했음을 Presenter에게 알림
    public event Action OnClicked;

    // Presenter가 View에게 UI 업데이트를 지시하는 메서드
    public void SetIcon(Sprite icon);
    public void ShowEquipIcon(bool isVisible);
    public void ClearSlot();
}