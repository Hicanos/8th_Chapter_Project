using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class Slot : MonoBehaviour
{
    [Header("아이템 정보")]
    [SerializeField] private string itemName;
    [SerializeField] private string description;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text descriptionText;
    [SerializeField] protected TMP_Text EquipOrStack; //장비라면 Equip, 스택 아이템이라면 숫자

    [Header("버튼 할당")]
    [SerializeField] protected Button selectButton;
    [SerializeField] protected Button cancelBtn;

    public void SetItem(ItemBase item)
    {
        itemName = item.name;
        ItemDescription(item);
        nameText.text = item.name;
        descriptionText.text = description;
        
    }

    public string ItemDescription (ItemBase item)
    {
        if(item is EquipData equip)
        {
            description = $"공격력: {equip.ItemAttack}| 방어력: {equip.ItemDeffensce}|가격:{item.SellPrice}";
        }
        else if (item is ConsumeData consume)
        {
            description = $"코인 증가:{consume.Amount}|지속 시간:{consume.Duration}|가격:{item.SellPrice}";
        }

            return description;
    }
}
