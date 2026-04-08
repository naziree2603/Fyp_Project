using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

[CreateAssetMenu(fileName = "Items", menuName = "Item/Create")]
public class Items : ScriptableObject
{
    public int ID;
    public Sprite ItemSprite;
    public GameObject ItemPrefab;
    public int value;
    public ItemType itemType;
    public ItemRarity rarity;
    public GameObject groundedPrefab;
}

public enum ItemType
{
    Sword,
    Shield
}   

public enum ItemRarity
{
    Common,
    Uncommon,
    Rare,
    Epic,
}