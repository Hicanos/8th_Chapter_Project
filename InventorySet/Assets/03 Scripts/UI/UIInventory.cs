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

    private List<GameObject> slotObjects = new List<GameObject>();

    public void UpdateUI()
    {
        // 기존 슬롯 파괴
        foreach (var slot in slotObjects)
        {
            Destroy(slot);
        }
        slotObjects.Clear();

        foreach (var pair in Items)
        {
            GameObject slotObj = Instantiate(InventorySlot, Parentobject.transform);
            slotObjects.Add(slotObj);

            // InventorySlot 컴포넌트에 아이템 정보와 개수 전달
            InventorySlot slot = slotObj.GetComponent<InventorySlot>();
            if (slot != null)
            {
                slot.SetItem(pair.Value.item);
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
                    // 개수가 1 이하가 되면 완전히 제거
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
}
