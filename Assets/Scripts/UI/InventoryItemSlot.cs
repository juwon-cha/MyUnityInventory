using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemSlotData
{
    public int ItemID;
}

public class InventoryItemSlot : MonoBehaviour
{
    [SerializeField] private Button slotButton;
    [SerializeField] private Image itemIcon;
    [SerializeField] private GameObject equipIcon;

    private InventoryItemSlotData slotData;

    // Constants
    private const string ITEM_ICON_PATH = "Textures/Items/";

    public void UpdateSlotData(InventoryItemSlotData data)
    {
        this.slotData = data;

        // 데이터가 없거나 ItemID가 0이면 빈 슬롯으로 처리
        if (slotData == null || slotData.ItemID == 0)
        {
            ClearSlot();
            return;
        }

        // 아이템 ID를 기반으로 이미지 업데이트
        UpdateImage();
    }

    private void UpdateImage()
    {
        // StringBuilder를 사용해 ItemID로 아이콘 파일 이름을 만든다.
        // 예: 12001 -> "12001"
        StringBuilder sb = new StringBuilder(slotData.ItemID.ToString());
        var itemIconName = sb.ToString();

        // 텍스처를 불러옴
        var itemIconTexture = Resources.Load<Texture2D>($"{ITEM_ICON_PATH}{itemIconName}");

        if (itemIconTexture != null)
        {
            // 텍스처 로딩에 성공하면 스프라이트를 생성하여 Image에 적용
            itemIcon.sprite = Sprite.Create(itemIconTexture, new Rect(0, 0, itemIconTexture.width, itemIconTexture.height), new Vector2(0.5f, 0.5f));
            itemIcon.color = Color.white; // 이미지가 보이도록 설정
        }
        else
        {
            // 텍스처를 찾지 못하면 경고를 출력하고 슬롯 비움
            Debug.LogWarning($"Item icon texture not found for name: {itemIconName}");
            ClearSlot();
        }
    }

    private void EquipItem()
    {
        equipIcon.SetActive(true);
    }

    private void UnequipItem()
    {
        equipIcon.SetActive(false);
    }

    // 슬롯의 이미지를 지워 빈 슬롯처럼 보이게 함
    private void ClearSlot()
    {
        itemIcon.sprite = null;
        itemIcon.color = new Color(1, 1, 1, 0); // 이미지를 투명하게 만듦
    }
}
