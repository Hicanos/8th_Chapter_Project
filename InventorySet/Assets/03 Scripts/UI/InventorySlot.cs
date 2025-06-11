using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlot : Slot
{
    [SerializeField] private Button useButton;
    [SerializeField] private TMP_Text useText;

    private bool isEquiped;
    private int itemStack;
    private ItemBase currentItem; // 현재 아이템 데이터

    public void Select()
    {
        if (useButton != null) useButton.gameObject.SetActive(true);
        if (cancelBtn != null) cancelBtn.gameObject.SetActive(true);
    }

    public void Deselect()
    {
        if(useButton != null) useButton.gameObject.SetActive(false);
        if(cancelBtn != null) cancelBtn.gameObject.SetActive(false);
    }

    private void Awake()
    {
        if (selectButton != null)
            selectButton.onClick.AddListener(OnSlotClicked);
        Deselect();
    }

    private void OnSlotClicked()
    {
        UIInventory inventory = UIManager.Instance.Inventory;
        inventory.OnSlotSelected(this);
    }

    // 사용버튼을 누름=> 아이템 사용 혹은 장착
    public void OnUseButtonClicked()
    {
        UIInventory inventory = UIManager.Instance.Inventory;
        if (currentItem is ConsumeData)
        {
            inventory.UseItem(this, currentItem);
        }
        else if (currentItem is EquipData)
        {
            if (isEquiped)
                inventory.UnequipItem(this, currentItem);
            else
                inventory.EquipItem(this, currentItem);
        }
    }

    public void OnCancelClicked()
    {
        Deselect();
    }


    public void SetItem(ItemBase item, int count, bool equipped)
    {
        base.SetItem(item);
        isEquiped = equipped;
        itemStack = count;
        currentItem = item;
        TextSetting(item);
        // 버튼 이벤트 중복 방지 및 연결
        if (useButton != null)
        {
            useButton.onClick.RemoveAllListeners();
            useButton.onClick.AddListener(OnUseButtonClicked);
        }
        if (cancelBtn != null)
        {
            cancelBtn.onClick.RemoveAllListeners();
            cancelBtn.onClick.AddListener(OnCancelClicked);
        }
    }
    private void TextSetting(ItemBase item)
    {
        //만약 선택된 아이템이 장비라면 장착하기, 소비품이라면 사용하기로 텍스트 표기
        if (item == null)
        {
            useText.text = "";
            useButton.interactable = false;
            return;
        }

        // 장비 아이템이면 "장착하기", 소비 아이템이면 "사용하기"
        if (item is EquipData)
        {
            EquipOrStack.text = isEquiped ? "Equip" : "";
            useText.text = isEquiped ? "해제하기" : "장착하기";
            useButton.interactable = true;
        }
        else if (item is ConsumeData)
        {
            if(item.isStackable)
            EquipOrStack.text = itemStack >= 1 ? itemStack.ToString() : "";

            useText.text = "사용하기";
            useButton.interactable = true;
        }
        else
        {
            useText.text = "사용 불가";
            useButton.interactable = false;
        }
    }



}
    
