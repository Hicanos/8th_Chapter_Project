using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIStatus : MonoBehaviour
{
    Player player;

    [SerializeField] private TMP_Text lvText;
    [SerializeField] private TMP_Text hpText;
    [SerializeField] private TMP_Text mpText;
    [SerializeField] private TMP_Text attackText;
    [SerializeField] private TMP_Text deffenseText;

    private string stringLevel;
    private string stringHP;
    private string stringMP;
    private string stringAttack;
    private string stringDefense;
    

    void Start()
    {
        player = FindObjectOfType<Player>();
        if(player == null)
        {
            return;
        }
        player.playerData.OnStatusChanged += UpdateUI; //스탯에 변화가 생기면 UpdateUI가 발생하도록 구독
        UpdateUI();
    }

    private void OnDestroy()
    {
        if (player != null && player.playerData != null)
            player.playerData.OnStatusChanged -= UpdateUI;
    }


    void UpdateUI()
    {
        var data = player.playerData;
        lvText.text = $"LV: {data.Level}";
        hpText.text = $"HP: {data.MaxHP}";
        mpText.text = $"MP: {data.MaxMP}";
        attackText.text = $"ATK: {data.Attack}";
        deffenseText.text = $"DEF: {data.Deffense}";
    }
}
