using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player", menuName = "Characters/Player")]
public class PlayerConSO : ConditionSO
{
    //플레이어의 소지금
    [SerializeField] private int coin;
}
