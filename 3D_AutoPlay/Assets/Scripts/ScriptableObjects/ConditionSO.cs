using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Player와 Enemy의 공통적인 Condition
/// </summary>

[CreateAssetMenu(fileName = "Condition", menuName = "Conditions")]
public class ConditionSO : ScriptableObject
{

    [SerializeField] public int MaxHP;
    [SerializeField] private int SP;
    
}
