using System;
using System.Collections.Generic;
using UnityEngine;

public class ItemStats
{
    public int attack_power;
    public int defense;
    public int health;
    public int critical;

    public ItemStats(int attack_power, int defense, int health, int critical)
    {
        this.attack_power = attack_power;
        this.defense = defense;
        this.health = health;
        this.critical = critical;
    }
}

[Serializable]
public class InventoryData
{
    public int MaxItemCount { get; private set; }

    public ItemData EquippedWeaponData { get; set; }
    public ItemData EquippedShieldData { get; set; }
    public ItemData EquippedChestArmorData { get; set; }
    public ItemData EquippedBootsData { get; set; }
    public ItemData EquippedHelmetData { get; set; }
    public ItemData EquippedAccessoryData { get; set; }

    public List<ItemData> items = new List<ItemData>();
    public Dictionary<int, ItemStats> EquippedItemDict = new Dictionary<int, ItemStats>();

    public event Action OnInventoryChanged;

    public void SetInventoryMaxCount(int maxCount)
    {
        MaxItemCount = maxCount;
    }

    public void SetEquippedItemDic()
    {
        if (EquippedWeaponData != null)
        {
            var itemData = DataManager.Instance.GetItem(EquippedWeaponData.item_id);
            if (itemData != null)
            {
                EquippedItemDict.Add(EquippedWeaponData.item_id, new ItemStats(itemData.attack_power, itemData.defense, itemData.health, itemData.critical));
            }
        }

        if (EquippedShieldData != null)
        {
            var itemData = DataManager.Instance.GetItem(EquippedShieldData.item_id);
            if (itemData != null)
            {
                EquippedItemDict.Add(EquippedShieldData.item_id, new ItemStats(itemData.attack_power, itemData.defense, itemData.health, itemData.critical));
            }
        }

        if (EquippedChestArmorData != null)
        {
            var itemData = DataManager.Instance.GetItem(EquippedChestArmorData.item_id);
            if (itemData != null)
            {
                EquippedItemDict.Add(EquippedChestArmorData.item_id, new ItemStats(itemData.attack_power, itemData.defense, itemData.health, itemData.critical));
            }
        }

        if (EquippedBootsData != null)
        {
            var itemData = DataManager.Instance.GetItem(EquippedBootsData.item_id);
            if (itemData != null)
            {
                EquippedItemDict.Add(EquippedBootsData.item_id, new ItemStats(itemData.attack_power, itemData.defense, itemData.health, itemData.critical));
            }
        }

        if (EquippedHelmetData != null)
        {
            var itemData = DataManager.Instance.GetItem(EquippedHelmetData.item_id);
            if (itemData != null)
            {
                EquippedItemDict.Add(EquippedHelmetData.item_id, new ItemStats(itemData.attack_power, itemData.defense, itemData.health, itemData.critical));
            }
        }

        if (EquippedAccessoryData != null)
        {
            var itemData = DataManager.Instance.GetItem(EquippedAccessoryData.item_id);
            if (itemData != null)
            {
                EquippedItemDict.Add(EquippedAccessoryData.item_id, new ItemStats(itemData.attack_power, itemData.defense, itemData.health, itemData.critical));
            }
        }
    }

    public bool AddItem(ItemData newItem)
    {
        if (items.Count >= MaxItemCount)
        {
            Debug.LogWarning("Inventory is full. Cannot add more items.");
            return false;
        }

        items.Add(newItem);
        OnInventoryChanged?.Invoke();
        return true;
    }

    public bool RemoveItem(ItemData itemToRemove)
    {
        if (items.Remove(itemToRemove))
        {
            OnInventoryChanged?.Invoke();
            return true;
        }
        else
        {
            Debug.LogWarning("Item not found in inventory.");
            return false;
        }
    }



    public List<ItemData> GetAllItems()
    {
        return items;
    }
}
