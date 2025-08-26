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

    Dictionary<int, ItemData> inventory = new Dictionary<int, ItemData>();
}
