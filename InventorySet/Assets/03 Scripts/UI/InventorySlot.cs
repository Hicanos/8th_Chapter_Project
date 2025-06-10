using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlot : Slot
{
    [SerializeField] private Button useButton;
    [SerializeField] private TMP_Text useText;

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
        

    public override void SetItem(ItemBase item)
    {
        base.SetItem(item);
        TextSetting(item);
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
            useText.text = "장착하기";
            useButton.interactable = true;
        }
        else if (item is ConsumeData)
        {
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
    
