using UnityEngine;

[System.Serializable]
public class Equipment
{
    public int itemId; // ItemBase의 고유 ID
    public EquipParts parts;
    public bool isEquipped;

    public Equipment(int id, bool equipped, EquipParts parts)
    {
        itemId = id;
        isEquipped = equipped;
        this.parts = parts;
    }
}