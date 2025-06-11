using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

[CreateAssetMenu(fileName = "Character", menuName = "Characters/Player")]
public class PlayerData : ScriptableObject
{
    public event Action OnStatusChanged; //스탯 변경 시 발생할 이벤트액션


    // 레벨, 체, 마, 공, 방, 속
    [SerializeField] private int level;
    [SerializeField] private int maxHP;
    [SerializeField] private int maxMP;
    [SerializeField] private int attack;
    [SerializeField] private int deffense;
    [SerializeField][field: Range(1f, 3f)] private float speed; //기본 스피드
    [SerializeField][field: Range(0f, 3f)] private float sprint;//달리기 스피드 배수

    // 다른 코드에서 사용하기 위한 인수
    public int Level => level;
    public int MaxHP => maxHP;
    public int MaxMP => maxMP;
    public int Attack => attack;
    public int Deffense => deffense;
    public float Speed => speed;
    public float Sprint => sprint;


    public void SetLevel(int value)
    {
        level = value;
        OnStatusChanged?.Invoke();
    }

    public void SetMaxHP(int value)
    {
        maxHP = value;
        OnStatusChanged?.Invoke();
    }

    public void SetMaxMP(int value)
    {
        maxMP = value;
        OnStatusChanged?.Invoke();
    }

    public void SetAttack(int value)
    {
        attack = value;
        OnStatusChanged?.Invoke();

    }

    public void SetDeffense(int value)
    {
        deffense = value;
        OnStatusChanged?.Invoke();
    }

    public void StatusChanged()
    {
        OnStatusChanged?.Invoke();
    }


    //총 공격력과 총 방어력을 가져오는 프로퍼티

    public int TotalAttack
    {
        get
        {
            int total = Attack;
            // 장비 공격력 합산
            foreach (var equip in UIManager.Instance.Inventory.Equipments)
            {
                if (equip.isEquipped) //장비중인 아이템이 있다면
                {
                    // Items 딕셔너리에서 아이템 정보 가져오기
                    if (UIManager.Instance.Inventory.Items.TryGetValue(equip.itemId, out var entry) && entry.item is EquipData equipData)
                    {
                        total += equipData.ItemAttack;
                    }
                }
            }
            return total;
        }
    }

    public int TotalDeffense
    {
        get
        {
            int total = Deffense;
            foreach (var equip in UIManager.Instance.Inventory.Equipments)
            {
                if (equip.isEquipped)
                {
                    if (UIManager.Instance.Inventory.Items.TryGetValue(equip.itemId, out var entry) && entry.item is EquipData equipData)
                    {
                        total += equipData.ItemDeffense; 
                    }
                }
            }
            return total;
        }
    }
}
