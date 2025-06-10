using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// 화면 좌측 상단에 위치하는 컨디션 관리자
/// </summary>
public class UICondition : MonoBehaviour
{
    // 1. HP, 2. SP(int), 3. Exp, 4.LV
    // Player의 Health값을 받아와서 HPBar에 적용
    // HP Bar와 EXP는 FillAmount를 조절함 (Health는 1에서 감소, Exp는 0에서 증가)

    public GameObject condition;
    [SerializeField] private Player player;
    [SerializeField] private PlayerConSO playerConSO;

    [Header("HP")]
    [SerializeField] private Image hPbar; // HP 변화값을 담을 HP바
    [SerializeField] private TMP_Text hPText;
    [SerializeField] private string hPAmount;
    [SerializeField] private int maxHP;


    [Header("EXP")]
    [SerializeField] private Image expBar;
    [SerializeField] private float maxExp;
    private float curExp;


    [SerializeField] private Image[] SPTokens; //SP가 감소할 때마다 3 -> 2 -> 1 -> 0. 회복시에는 역순

    [Header("Level")]
    [SerializeField] private TMP_Text lvText; // 레벨이 오를 때마다 반영함
    [SerializeField] private int level; // 현재 레벨

    [Header("Coin")]
    [SerializeField] private int coin;
    [SerializeField] private TMP_Text coinTxt;

    private void Start()
    {
        maxHP = playerConSO.MaxHP;
        coin = playerConSO.Coin;

    }
    public void UpdateUI(PlayerConSO playerCon)
    {
        //HP 바와 EXP 바의 숫자 변화 - Player의 Health
        hPbar.fillAmount = player.Health.CurrentHealth / maxHP;
        DisplayHP(player.Health.CurrentHealth, maxHP);
        //Exp
        expBar.fillAmount = playerCon.Exp / playerCon.ExpToNextLevel;
        lvText.text = playerCon.Level.ToString();
        coinTxt.text = playerCon.Coin.ToString();

    }


    public float GetPercentage(int cur, int max)
    {
        return Mathf.Round(cur / max);
    }
    
    public void DisplayHP(int curHP, int maxHP)
    {
        hPText.text = $"{curHP}/{maxHP}";
    }

    
}
