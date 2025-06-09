using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Player와 Enemy의 공통적인 Condition
/// </summary>
public class ConditionSO : ScriptableObject
{
    private float HP;
    public float MaxHP { get; private set; }
    [SerializeField] private int SP;
    public float ReadSP { get; private set; }
    [SerializeField] private int LV;
    [SerializeField] private float EXP; //플레이어의 경험치는 쌓는 용, 에너미의 EXP는 플레이어에게 제공하는 용

}
