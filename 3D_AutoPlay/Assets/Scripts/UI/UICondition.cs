using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 화면 좌측 상단에 위치하는 컨디션 관리자
/// </summary>
public class UICondition : MonoBehaviour
{
    // 1. HP, 2. SP(int), 3. Exp, 4.LV
    // Player의 Health값을 받아와서 HPBar에 적용
    // HP Bar와 EXP는 FillAmount를 조절함 (Health는 1에서 감소, Exp는 0에서 증가)

    public GameObject condition;
    
}
