using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UIInventory : MonoBehaviour
{
    private InventorySlot currentSelectedSlot;

    public GameObject Parentobject;
    public GameObject InventorySlot;
    public ItemBase ItemData;


    // 아이템 ID와 (아이템 정보, 보유 개수) 쌍으로 관리
    public Dictionary<int, (ItemBase item, int count)> Items = new Dictionary<int, (ItemBase, int)>();

    // 장비 장착 상태 관리
    public List<Equipment> Equipments = new List<Equipment>();

    //아이템 슬롯 관리
    private List<GameObject> slotObjects = new List<GameObject>();

    public void UpdateUI()
    {
        // 기존 슬롯 파괴
        foreach (var slot in slotObjects)
        {
            Destroy(slot);
        }
        slotObjects.Clear();


        //아이템 리스트에 맞춰 슬롯 생성
        foreach (var pair in Items)
        {
            GameObject slotObj = Instantiate(InventorySlot, Parentobject.transform);
            slotObjects.Add(slotObj);

            // InventorySlot 컴포넌트에 아이템 정보와 개수 전달
            InventorySlot slot = slotObj.GetComponent<InventorySlot>();
            if (slot != null)
            {
                bool equipped = false;
                // 장비 아이템이면 장착 상태 확인
                if (pair.Value.item is EquipData)
                {
                    equipped = Equipments.Exists(e => e.itemId == pair.Key && e.isEquipped);
                }
                slot.SetItem(pair.Value.item, pair.Value.count, equipped);
            }
        }
    }

    public void OnSlotSelected(InventorySlot slot)
    {
        if (currentSelectedSlot != null)
            currentSelectedSlot.Deselect();

        currentSelectedSlot = slot;
        currentSelectedSlot.Select();
    }

    // 아이템 추가
    public void AddItem(int id, ItemBase item, int amount = 1)
    {
        if (Items.ContainsKey(id))
        {
            // 스택 가능하면 개수 증가
            if (item.isStackable)
            {
                var entry = Items[id];
                entry.count += amount;
                Items[id] = entry;
            }
            else
            {
                // 스택 불가면 무시하거나 별도 처리
            }
        }
        else
        {
            Items.Add(id, (item, amount));
        }
        UpdateUI();
    }

    // 아이템 제거
    public void RemoveItem(int id, int amount = 1)
    {
        if (Items.ContainsKey(id))
        {
            var entry = Items[id];
            if (entry.item.isStackable)
            {
                if (entry.count > amount)
                {
                    entry.count -= amount;
                    Items[id] = entry;
                }
                else
                {
                    // 개수가 1 미만이 되면 완전히 제거
                    Items.Remove(id);

                }
            }
            else
            {
                // 스택 불가 아이템은 바로 제거
                Items.Remove(id);
            }
            UpdateUI();
        }
    }

    //아이템 사용
    public void UseItem(InventorySlot slot, ItemBase item)
    {        
        UIClicker clicker = UIManager.Instance.Clicker;
        clicker.UseItem(item);

        // 인벤토리에서 수량 감소
        int id = int.TryParse(item.itemID, out var parsedId) ? parsedId : GetItemId(item);
        RemoveItem(id, 1);
    }

    // 아이템 장착/해제

    // 장비 장착
    public void EquipItem(InventorySlot slot, ItemBase item)
    {
        if (!(item is EquipData newEquipData)) return;
        int id = GetItemId(item);

        // 동일 타입의 장비가 이미 장착되어 있다면 해제
        foreach (var equip in Equipments)
        {
            if (equip.isEquipped && equip.parts == newEquipData.EquipPart)
                equip.isEquipped = false;
        }

        // 장착 상태 등록 또는 갱신
        var exist = Equipments.Find(e => e.itemId == id);
        if (exist == null)
            Equipments.Add(new Equipment(id, true, newEquipData.EquipPart));
        else
            exist.isEquipped = true;

        //갱신이 완료되었다면 PlayerData에도 알려줌
        var player = FindObjectOfType<Player>();
        if (player != null && player.playerData != null)
            player.playerData.StatusChanged();

        UpdateUI();
    }

    // 장비 해제
    public void UnequipItem(InventorySlot slot, ItemBase item)
    {
        int id = int.TryParse(item.itemID, out var parsedId) ? parsedId : GetItemId(item);

        // 장착중인 아이템에서 해당 장비 찾기
        var equip = Equipments.Find(e => e.itemId == id);
        if (equip != null)
            equip.isEquipped = false;

        //갱신이 완료되었다면 PlayerData에도 알려줌
        var player = FindObjectOfType<Player>();
        if (player != null && player.playerData != null)
            player.playerData.StatusChanged();

        UpdateUI();
    }


    private int GetItemId(ItemBase item)
    {
        foreach (var pair in Items)
        {
            if (pair.Value.item == item)
                return pair.Key;
        }
        return -1;
    }
}
