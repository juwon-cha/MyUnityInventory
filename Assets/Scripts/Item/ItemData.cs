using System;
using System.Collections.Generic;

public enum EItemType
{
    Weapon = 1,
    Shield,
    ChestArmor,
    Helmet,
    Boots,
    Accessory,
}

[Serializable]
public class ItemData
{
    public int item_id;
    public string item_name;
    public int attack_power;
    public int defense;
    public int health;
    public int critical;
}

[Serializable]
public class ItemDataTable
{
    public List<ItemData> items;

    public ItemDataTable()
    {
        items = new List<ItemData>();
    }
}