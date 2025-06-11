using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIClicker : MonoBehaviour
{
    [SerializeField] private long coin;
    [SerializeField] private TMP_Text coinTxt;
    [SerializeField] private long DebugCoin; // 큰 수 테스트용 코인
    private long itembonus;

    [SerializeField] private ItemBase[] items;

    UIInventory inventory;
    public long CurCoin => coin;

    public void Start()
    {
        inventory = UIManager.Instance.Inventory;

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
        
        coin += 1 *(1+itembonus);

        // coin + 현재 장착중인 아이템 데이터의 CoinBonus 총합
        UpdateUI();
    }

    public void GetItem()
    {
        if (items == null || items.Length == 0)
            return;

        // 랜덤 아이템 선택
        int randIdx = Random.Range(0, items.Length);
        ItemBase item = items[randIdx];

        // 고유 ID가 int가 아니면, 배열 인덱스를 ID로 사용
        int id;
        if (!int.TryParse(item.itemID, out id))
            id = randIdx;

        // 인벤토리에 아이템 추가 (기본 수량 1)
        inventory.AddItem(id, item, 1);

        // 필요시 UI 갱신
        inventory.UpdateUI();
    }



    public void UseItem(ItemBase item)
    {
        // 소모품(ConsumeData)인지 확인
        if (item is ConsumeData consume)
        {
            itembonus += (long)consume.Amount;
            Debug.Log($"소모품 사용: itembonus가 {consume.Amount}만큼 증가하여 {itembonus}가 되었습니다.");
        }
    }
}
