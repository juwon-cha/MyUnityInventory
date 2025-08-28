using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class InventoryItemSlotPresenter
{
    private const string ITEM_ICON_PATH = "Textures/Items/";

    private readonly IInventoryItemSlotView view;
    private readonly InventoryItemSlotData data;

    // 생성자에서 View와 Model(SlotData) 주입받음
    public InventoryItemSlotPresenter(IInventoryItemSlotView view, InventoryItemSlotData data)
    {
        this.view = view;
        this.data = data;

        // View의 클릭 이벤트 구독
        //this.view.OnClicked += HandleClick;

        // Presenter가 생성될 때 View 초기화
        UpdateView();
    }

    // Presenter가 더 이상 필요 없을 때 이벤트 구독 해제
    public void Dispose()
    {
        if (view != null)
        {
            //view.OnClicked -= HandleClick;
        }
    }

    //private void HandleClick()
    //{
    //    // 클릭 이벤트가 발생하면 상위 Presenter에게 알려야 하지만
    //    // 이 Presenter는 상위 Presenter를 직접 알지 못한다.
    //    // 대신 View를 통해 이벤트를 다시 외부에 전파한다.
    //    // (이 부분은 InventoryUI가 처리하게 된다)
    //}

    private void UpdateView()
    {
        if (data == null || data.ItemID == 0)
        {
            view.ClearSlot();
            return;
        }

        // 아이콘 로드 로직을 Presenter가 담당
        // StringBuilder를 사용해 ItemID로 아이콘 파일 이름을 만든다.
        // 예: 12001 -> "12001"
        StringBuilder sb = new StringBuilder(data.ItemID.ToString());
        var itemIconName = sb.ToString();
        var itemIconTexture = Resources.Load<Texture2D>($"{ITEM_ICON_PATH}{itemIconName}");

        if (itemIconTexture != null)
        {
            var sprite = Sprite.Create(itemIconTexture, new Rect(0, 0, itemIconTexture.width, itemIconTexture.height), new Vector2(0.5f, 0.5f));
            view.SetIcon(sprite);
        }
        else
        {
            Debug.LogWarning($"Item icon texture not found for name: {itemIconName}");
            view.ClearSlot();
        }

        view.ShowEquipIcon(data.IsEquipped);
    }
}
