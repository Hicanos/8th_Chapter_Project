using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Enemy의 레벨, Player에게 지급하는 경험치와 돈
/// 
/// </summary>
[CreateAssetMenu(fileName = "EnemyCon", menuName = "Conditions/Enemy")]
public class EnemyConSO : ConditionSO
{
    [SerializeField] private int level; //에너미마다 다른 레벨
    [SerializeField] private float exp; // 에너미가 사망 시 플레이어에게 들어가는 Exp
    [SerializeField] private int coin; // 에너미가 Player에게 지급하는 돈
    [SerializeField] private BaseItemData[] Items; // 에너미가 드랍하는 아이템들

    public float RewardExp => exp;
    public int RewardCoin => coin;
}
