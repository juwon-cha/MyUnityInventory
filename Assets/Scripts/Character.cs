using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private string characterName;
    private int level;
    private int health;
    private int attackPower;
    private int defense;
    private int experience;
    private int critical;

    public InventoryData Inventory { get; private set; }

    private const int INVENTORY_MAX_COUNT = 120;

    private void Awake()
    {
        Inventory = new InventoryData();
        Inventory.SetInventoryMaxCount(INVENTORY_MAX_COUNT);
    }

    public void AcquireAllItems()
    {
        Dictionary<int, ItemData> allItemDatabase = DataManager.Instance.ItemDatabase;
        foreach(var item in allItemDatabase.Values)
        {
            Inventory.AddItem(item);
        }
    }
}
