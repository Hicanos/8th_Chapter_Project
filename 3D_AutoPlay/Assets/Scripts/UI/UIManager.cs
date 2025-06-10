using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 전체적인 UI을 ON/OFF
/// 관리하는 클래스
/// </summary>
public class UIManager : MonoBehaviour
{
    private static UIManager Instance;

    [SerializeField] public GameObject UIInventory; //차후 UIInventory로 변경
    [SerializeField] public GameObject UIEquipment; // 장비창

    Dictionary<int,GameObject> UIDictionary = new Dictionary<int,GameObject>(); //UI들을 담은 Dictionary


    private void Awake()
    {
        if(Instance == null) Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }
    void Start()
    {
        
    }

    private void Load()
    {
        
    }

    // Dictionary의 UI 중 하나가 켜지면, 다른 UI로 향하는 버튼은 감춰짐
    // 단, 뒤로가기 버튼은 살아있어야 함.




}
