using System;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemSlotData
{
    public int ItemID;
    public bool IsEquipped;
}

public class InventoryItemSlot : MonoBehaviour, IInventoryItemSlotView
{
    [SerializeField] private Button slotButton;
    [SerializeField] private Image itemIcon;
    [SerializeField] private GameObject equipIcon;

    private InventoryItemSlotPresenter presenter;

    public event Action OnClicked;

    private void Awake()
    {
        slotButton.onClick.AddListener(() => OnClicked?.Invoke());
    }

    // 상위 View(InventoryUI)가 이 슬롯을 초기화할 때 호출
    public void Initialize(InventoryItemSlotData data)
    {
        // 자신을 제어할 Presenter를 생성하고 연결
        presenter = new InventoryItemSlotPresenter(this, data);
    }

    public void SetIcon(Sprite icon)
    {
        itemIcon.sprite = icon;
        itemIcon.color = Color.white;
    }

    public void ShowEquipIcon(bool isVisible)
    {
        equipIcon.SetActive(isVisible);
    }

    public void ClearSlot()
    {
        itemIcon.sprite = null;
        itemIcon.color = new Color(1, 1, 1, 0);
        equipIcon.SetActive(false);
    }

    private void OnDestroy()
    {
        // 이 슬롯 오브젝트가 파괴될 때 Presenter 정리
        presenter?.Dispose();
    }
}
