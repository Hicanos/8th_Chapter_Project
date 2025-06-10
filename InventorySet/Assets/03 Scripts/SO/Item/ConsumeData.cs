using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="ConsumeItem",menuName ="Item/Consumable")]
public class ConsumeData :ItemBase
{
    [SerializeField]
    private float duration; //증가량 지속시간
    [SerializeField]
    private float amount; //회복량, 증가량

    public float Duration => duration;
    public float Amount => amount;

    //여기에 원래 상승시키는 능력치 종류 따위 적혀있는데 이번에는 전부 코인 증가량으로 고정
}
