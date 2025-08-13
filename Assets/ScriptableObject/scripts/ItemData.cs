using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Consumable
}

public enum ConsumableType
{
    SpeedUp,
    SuperJump
}

[System.Serializable]
public class ItemDataConsumable
{
    public ConsumableType type;
}

[CreateAssetMenu(fileName = "Item", menuName = "New Item")]
public class ItemData : ScriptableObject
{
    [Header("Info")]
    public string displayName;
    public string description;
    public ItemType type;
    public Sprite icon;
    public GameObject dropPrefab;

    [Header("Consumable")]
    public ItemDataConsumable[] consumables;
}
