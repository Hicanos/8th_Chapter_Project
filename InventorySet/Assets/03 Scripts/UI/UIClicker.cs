using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIClicker : MonoBehaviour
{
    [SerializeField] private long coin;
    [SerializeField] private TMP_Text coinTxt;
    [SerializeField] private long DebugCoin; // 큰 수 테스트용 코인


    public long CurCoin => coin;

    public void Start()
    {
        Reset();
    }
    public void Reset()
    {
        coin = 0;
        UpdateUI();
    }
    public void UpdateUI()
    {
        coinTxt.text = CostUtil.ConvertCurrency(coin);
    }
    public void GetCoin()
    {
        //버튼을 누르면, Coin 증가
        coin += 1+DebugCoin;
        // coin + 현재 장착중인 아이템 데이터의 CoinBonus 총합
        UpdateUI();
    }




    
}
