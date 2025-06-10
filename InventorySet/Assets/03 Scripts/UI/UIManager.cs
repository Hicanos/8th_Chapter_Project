using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UIType
{
    Clicker,
    Inventory,
    Status
}


public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    // Inspector에서 할당할 UI 패널들
    
    [SerializeField] private UIClicker clicker;
    [SerializeField] private UIMainMenu mainMenu;
    [SerializeField] private UIStatus uiStatus;
    [SerializeField] private UIInventory inventory;

    // public 프로퍼티로 각각의 UI에 접근
    public UIClicker Clicker { get { return clicker; } }
    public UIMainMenu MainMenu { get { return mainMenu; } }
    public UIStatus Status { get { return uiStatus; } }
    public UIInventory Inventory { get { return inventory; } }

    private void Awake()
    {
        // 싱글톤 인스턴스 생성 및 중복 제거
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // 모든 UI를 비활성화 후 특정 UI만 활성화
    public void SwitchUI(GameObject uiToActivate)
    {
        // 모든 UI 비활성화
        mainMenu.gameObject.SetActive(false);
        uiStatus.gameObject.SetActive(false);
        inventory.gameObject.SetActive(false);

        // 선택한 UI 활성화
        uiToActivate.SetActive(true);
    }


}
