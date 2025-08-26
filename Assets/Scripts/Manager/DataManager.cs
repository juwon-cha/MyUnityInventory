using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : SingletonBehaviour<DataManager>
{
    public Dictionary<int, ItemData> ItemDatabase { get; private set; } = new Dictionary<int, ItemData>();

    // Constants
    private const string ITEM_DATA_PATH = "ItemData/"; // Resources 폴더 내 경로

    protected override void Init()
    {
        base.Init();
    }

    private void Awake()
    {
        LoadItemData("ItemData");
    }

    private void LoadItemData(string fileName)
    {
        // Resources 폴더에서 ItemData.json 파일을 텍스트 에셋으로 불러옴
        // 파일 확장자(.json)는 경로에 포함하지 않음
        TextAsset jsonData = Resources.Load<TextAsset>($"{ITEM_DATA_PATH}{fileName}");

        if (jsonData == null)
        {
            Debug.LogError("Cannot find ItemData in any Resources folder!");
            return;
        }

        // JSON 텍스트를 C# 클래스(ItemDataTable) 객체로 변환(역직렬화)
        ItemDataTable dataTable = JsonUtility.FromJson<ItemDataTable>(jsonData.text);

        if (dataTable == null || dataTable.items == null)
        {
            Debug.LogError("Failed to parse ItemData.json. Check the file format.");
            return;
        }

        foreach (ItemData item in dataTable.items)
        {
            if (!ItemDatabase.ContainsKey(item.item_id))
            {
                ItemDatabase.Add(item.item_id, item);
            }
            else
            {
                // 동일한 ID를 가진 아이템이 데이터 테이블에 중복으로 있을 경우 경고 출력
                Debug.LogWarning($"Duplicate item ID found in data table: {item.item_id}");
            }
        }
    }

    public ItemData GetItem(int itemID)
    {
        if(ItemDatabase.TryGetValue(itemID, out ItemData item))
        {
            return item;
        }

        Debug.LogWarning($"Item with ID '{itemID}' not found in the ItemDatabase.");
        return null;
    }
}
