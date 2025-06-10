using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

[CreateAssetMenu(fileName ="Item", menuName ="Item")]
public class ItemBase : ScriptableObject
{
    [Header("기본 정보")]
    // 고유 ID를 추가하면 아이템 관리 및 저장/불러오기에 유용 (확장성 고려)
    public string itemID; // 아이템의 고유 식별자
    public string itemName; // 아이템 이름

    [SerializeField] private int price;


    [Header("인벤토리 설정")]
    public bool isStackable = true; // 겹치기 가능 여부 (기본값 true), 도구나 무기에서는 false로 변경
    public int maxStackSize = 20; // 최대 겹침 개수 (기본값 20)

    public int Price => price;
    public int SellPrice => (int)(price * 0.8);
}
