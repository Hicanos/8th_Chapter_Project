using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 아이템 장착부위 (머리/몸/신발/반지/목)
public enum EquipParts
{
    Head,
    Body,
    Shoes,
    Ring,
    Neck,
    Weapon
}

[CreateAssetMenu(fileName ="EquipItem", menuName ="Item/Equipment")]
public class EquipData : ItemBase
{
    [Header("장비설정")]
    [SerializeField] private int itemLV; // 아이템 레벨 =>강화 시 상승
    [SerializeField] private int itemAttack;
    [SerializeField] private int itemDeffensce;
    [SerializeField] private int itemSpeed;
    [SerializeField] private EquipParts equipParts;
    [SerializeField] private int coinBonus;
    

    public EquipParts EquipPart => equipParts;
    public int ItemAttack => itemAttack;
    public int ItemDeffensce => itemDeffensce;
    public int ItemSpeed => itemSpeed; 

    public int CoinBonus => coinBonus; 

}
