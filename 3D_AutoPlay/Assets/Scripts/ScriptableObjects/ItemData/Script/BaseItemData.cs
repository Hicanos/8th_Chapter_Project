using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 아이템에 보너스가 붙어 있을 때 부가효과 여부
enum ItemType
{
    None,
    Damage,
    Speed,
    JumpForce

}

/// <summary>
/// 아이템의 기본 정보를 담는 클래스
/// Name, Description, CanStack, ItemType, Price, SellPrice
/// </summary>

[CreateAssetMenu(fileName = "Item", menuName = "")]
public class BaseItemData : ScriptableObject
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
