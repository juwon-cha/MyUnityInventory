using System;
using System.Collections.Generic;
using UnityEngine;

public class UserItemData
{
    public int ItemID;

    public UserItemData(int itemID)
    {
        ItemID = itemID;
    }
}

public class UserItemStats
{
    public int attack_power;
    public int defense;
    public int health;
    public int critical;

    public UserItemStats(int attack_power, int defense, int health, int critical)
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

    public UserItemData EquippedWeaponData { get; set; }
    public UserItemData EquippedShieldData { get; set; }
    public UserItemData EquippedChestArmorData { get; set; }
    public UserItemData EquippedBootsData { get; set; }
    public UserItemData EquippedHelmetData { get; set; }
    public UserItemData EquippedAccessoryData { get; set; }

    public List<UserItemData> items = new List<UserItemData>();
    public Dictionary<int, UserItemStats> EquippedItemDict = new Dictionary<int, UserItemStats>();

    public event Action OnInventoryChanged;

    public void SetDefaultData()
    {
        AcquireAllItems();
        SetEquippedItemDict();
    }

    public void SetInventoryMaxCount(int maxCount)
    {
        MaxItemCount = maxCount;
    }

    public void SetEquippedItemDict()
    {
        if (EquippedWeaponData != null)
        {
            var itemData = DataTableManager.Instance.GetItem(EquippedWeaponData.ItemID);
            if (itemData != null)
            {
                EquippedItemDict.Add(EquippedWeaponData.ItemID, new UserItemStats(itemData.attack_power, itemData.defense, itemData.health, itemData.critical));
            }
        }

        if (EquippedShieldData != null)
        {
            var itemData = DataTableManager.Instance.GetItem(EquippedShieldData.ItemID);
            if (itemData != null)
            {
                EquippedItemDict.Add(EquippedShieldData.ItemID, new UserItemStats(itemData.attack_power, itemData.defense, itemData.health, itemData.critical));
            }
        }

        if (EquippedChestArmorData != null)
        {
            var itemData = DataTableManager.Instance.GetItem(EquippedChestArmorData.ItemID);
            if (itemData != null)
            {
                EquippedItemDict.Add(EquippedChestArmorData.ItemID, new UserItemStats(itemData.attack_power, itemData.defense, itemData.health, itemData.critical));
            }
        }

        if (EquippedBootsData != null)
        {
            var itemData = DataTableManager.Instance.GetItem(EquippedBootsData.ItemID);
            if (itemData != null)
            {
                EquippedItemDict.Add(EquippedBootsData.ItemID, new UserItemStats(itemData.attack_power, itemData.defense, itemData.health, itemData.critical));
            }
        }

        if (EquippedHelmetData != null)
        {
            var itemData = DataTableManager.Instance.GetItem(EquippedHelmetData.ItemID);
            if (itemData != null)
            {
                EquippedItemDict.Add(EquippedHelmetData.ItemID, new UserItemStats(itemData.attack_power, itemData.defense, itemData.health, itemData.critical));
            }
        }

        if (EquippedAccessoryData != null)
        {
            var itemData = DataTableManager.Instance.GetItem(EquippedAccessoryData.ItemID);
            if (itemData != null)
            {
                EquippedItemDict.Add(EquippedAccessoryData.ItemID, new UserItemStats(itemData.attack_power, itemData.defense, itemData.health, itemData.critical));
            }
        }
    }

    public bool IsEquipped(int itemID)
    {
        return EquippedItemDict.ContainsKey(itemID);
    }

    public void EquipItem(int itemID)
    {
        if (EquippedItemDict.ContainsKey(itemID))
        {
            Debug.LogWarning("Item is already equipped.");
            return;
        }

        var itemData = DataTableManager.Instance.GetItem(itemID);
        if (itemData == null)
        {
            Debug.LogWarning("Item data not found for the given item ID.");
        }

        var itemType = (EItemType)(itemID / 10000);
        switch (itemType)
        {
            case EItemType.Weapon:
                if (EquippedWeaponData != null)
                {
                    // 기존 아이템 제거
                    EquippedItemDict.Remove(EquippedWeaponData.ItemID);
                    EquippedWeaponData = null;
                }
                // 새로운 아이템 장착
                EquippedWeaponData = new UserItemData(itemID);
                break;

            case EItemType.Shield:
                if (EquippedShieldData != null)
                {
                    EquippedItemDict.Remove(EquippedShieldData.ItemID);
                    EquippedShieldData = null;
                }
                EquippedShieldData = new UserItemData(itemID);
                break;

            case EItemType.ChestArmor:
                if (EquippedChestArmorData != null)
                {
                    EquippedItemDict.Remove(EquippedChestArmorData.ItemID);
                    EquippedChestArmorData = null;
                }
                EquippedChestArmorData = new UserItemData(itemID);
                break;

            case EItemType.Helmet:
                if (EquippedHelmetData != null)
                {
                    EquippedItemDict.Remove(EquippedHelmetData.ItemID);
                    EquippedHelmetData = null;
                }
                EquippedHelmetData = new UserItemData(itemID);
                break;

            case EItemType.Boots:
                if (EquippedBootsData != null)
                {
                    EquippedItemDict.Remove(EquippedBootsData.ItemID);
                    EquippedBootsData = null;
                }
                EquippedBootsData = new UserItemData(itemID);
                break;

            case EItemType.Accessory:
                if (EquippedAccessoryData != null)
                {
                    EquippedItemDict.Remove(EquippedAccessoryData.ItemID);
                    EquippedAccessoryData = null;
                }
                EquippedAccessoryData = new UserItemData(itemID);
                break;

            default:
                break;
        }

        // 장착한 아이템을 EquippedItemDic에 추가
        EquippedItemDict.Add(itemID, new UserItemStats(itemData.attack_power, itemData.defense, itemData.health, itemData.critical));

        OnInventoryChanged?.Invoke();
    }

    public void UnEquipItem(int itemID)
    {
        if (!EquippedItemDict.ContainsKey(itemID))
        {
            Debug.LogWarning("Item is not equipped.");
            return;
        }

        var itemType = (EItemType)(itemID / 10000);
        switch (itemType)
        {
            case EItemType.Weapon:
                EquippedWeaponData = null;
                break;
            case EItemType.Shield:
                EquippedShieldData = null;
                break;
            case EItemType.ChestArmor:
                EquippedChestArmorData = null;
                break;
            case EItemType.Helmet:
                EquippedHelmetData = null;
                break;
            case EItemType.Boots:
                EquippedBootsData = null;
                break;
            case EItemType.Accessory:
                EquippedAccessoryData = null;
                break;
            default:
                break;
        }

        // 해제한 아이템 EquippedItemDic에서 제거
        EquippedItemDict.Remove(itemID);

        OnInventoryChanged?.Invoke();
    }

    public UserItemStats GetUserTotalItemStats()
    {
        int totalAttack = 0;
        int totalDefense = 0;
        int totalHealth = 0;
        int totalCritical = 0;

        foreach (var stats in EquippedItemDict.Values)
        {
            totalAttack += stats.attack_power;
            totalDefense += stats.defense;
            totalHealth += stats.health;
            totalCritical += stats.critical;
        }

        return new UserItemStats(totalAttack, totalDefense, totalHealth, totalCritical);
    }

    public List<UserItemData> GetAllItems()
    {
        return items;
    }

    public void AcquireAllItems()
    {
        Dictionary<int, ItemData> allItemDatabase = DataTableManager.Instance.ItemDatabase;
        foreach (var item in allItemDatabase.Values)
        {
            items.Add(new UserItemData(item.item_id));
        }
    }

    public bool AcquireItem(UserItemData newItem)
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

    public bool RemoveItem(UserItemData itemToRemove)
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
}
