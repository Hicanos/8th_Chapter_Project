using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMainMenu : MonoBehaviour
{
    // Inspector에서 연결할 버튼들
    [SerializeField] private Button openMainMenuButton;   // 메인 메뉴(초기화면)
    [SerializeField] private Button openStatusButton;       // 상태 UI로 전환하는 버튼
    [SerializeField] private Button openInventoryButton;    // 인벤토리 UI로 전환하는 버튼
    [SerializeField] private Button openShopButton;     // 상점 UI로 전환

    private void Start()
    {
        // 버튼 클릭 시 해당 메서드 호출하도록 리스너 추가
        if (openMainMenuButton != null)
            openMainMenuButton.onClick.AddListener(OpenMainMenu);
        if (openStatusButton != null)
            openStatusButton.onClick.AddListener(OpenStatus);
        if (openInventoryButton != null)
            openInventoryButton.onClick.AddListener(OpenInventory);
        if (openShopButton != null)
            openShopButton.onClick.AddListener(OpenShop);
    }

    // 메인 메뉴 UI를 활성화하는 메서드
    public void OpenMainMenu()
    {
        // UIManager를 통해 메인 메뉴 활성화
        UIManager.Instance.SwitchUI(gameObject);
        Debug.Log("메인 메뉴 열림");
    }

    // 상태 UI로 전환하는 메서드
    public void OpenStatus()
    {
        // UIManager를 통해 상태 UI 활성화
        UIManager.Instance.SwitchUI(UIManager.Instance.Status.gameObject);
        Debug.Log("상태 UI 열림");
    }

    // 인벤토리 UI로 전환하는 메서드
    public void OpenInventory()
    {
        // UIManager를 통해 인벤토리 UI 활성화
        UIManager.Instance.SwitchUI(UIManager.Instance.Inventory.gameObject);
        Debug.Log("인벤토리 UI 열림");
    }

    public void OpenShop()
    {
        UIManager.Instance.SwitchUI(UIManager.Instance.Shop.gameObject);
    }
}
