using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class InventoryContainer
{
    public List<int> ItemId = new List<int>();
    public int EquipedSwordID = -1;
    public int EquipedShieldID = -1;
}
