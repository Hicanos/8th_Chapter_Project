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
    private static UIManager instance;

    // 인벤토리, 클릭커, 스테이터스를 여기에 GetComponent하여 참조하기 쉽게 만듦

    //Dictionary에 UI를 담아두기
    [SerializeField]
    private GameObject[] uiObjects;

    [SerializeField] private UIClicker clicker;
    [SerializeField] private UIInventory inventory;
    public Dictionary<UIType, GameObject> UIDictionary = new Dictionary<UIType, GameObject>();
    public static UIManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<UIManager>();
                if (instance == null)
                {
                    Debug.LogError("UIManager 인스턴스가 씬에 없습니다.");
                }
            }
            return instance;
        }
    }


    private void Awake()
    {
        if (instance == null)
        {
            instance=this;
            DontDestroyOnLoad(gameObject);
            // Enum 순서와 배열 순서가 일치해야 함, 배열로 Dictionary 등록
            for (int i = 0; i < uiObjects.Length; i++)
            {
                UIDictionary[(UIType)i] = uiObjects[i];
            }
        }
        else
        {
            Destroy(this.gameObject);
        }
        
    }


    void Start()
    {

    }

    // UI를 보여주고 감추는 메서드,타입을 찾아서 해당되는 오브젝트를 들고온다.
    public void ShowUI(UIType type)
    {
        if (UIDictionary.TryGetValue(type, out var obj))
            obj.SetActive(true);
    }

    public void HideUI(UIType type)
    {
        if (UIDictionary.TryGetValue(type, out var obj))
            obj.SetActive(false);
    }

    public T GetUIComponent<T>(UIType type) where T : MonoBehaviour
    {
        if (UIDictionary.TryGetValue(type, out var obj))
            return obj.GetComponent<T>();
        return null;
    }


}
