using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIClicker : MonoBehaviour
{
    [SerializeField] private int coin;
    [SerializeField] private TMP_Text coinTxt;


    public int CurCoin => coin;


    public void Reset()
    {
        coin = 0;
        coinTxt.text = coin.ToString();
    }
    public void UpdateUI()
    {
        coinTxt.text = coin.ToString();
    }
    public void GetCoin()
    {
        //버튼을 누르면, Coin 증가
        coin += 1;
        // coin + 현재 장착중인 아이템 데이터의 CoinBonus 총합
        UpdateUI();
    }




    
}
