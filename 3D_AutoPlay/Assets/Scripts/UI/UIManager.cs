using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 전체적인 UI을 ON/OFF
/// 관리하는 클래스
/// </summary>
public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] public GameObject UIInventory; //차후 UIInventory로 변경
    [SerializeField] public GameObject UIEquipment; // 장비창

    Dictionary<int,GameObject> UIDictionary = new Dictionary<int,GameObject>(); //UI들을 담은 Dictionary


    private void Awake()
    {
        if(Instance == null) Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        
    }

    private void Load()
    {
        
    }
}
