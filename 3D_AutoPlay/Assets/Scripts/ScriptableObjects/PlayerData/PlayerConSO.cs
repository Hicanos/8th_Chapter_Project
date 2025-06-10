using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;

[CreateAssetMenu(fileName = "PlayerCon", menuName = "Conditions/Player")]
public class PlayerConSO : ConditionSO
{
    [SerializeField] private int level = 1;
    [SerializeField] private float exp = 0f;
    [SerializeField] private float expToNextLevel;

    // Player의 초기 Coin값
    [SerializeField] private int coin;
    public int Level => level;
    public float Exp => exp;
    public float ExpToNextLevel => expToNextLevel;
    public int Coin => coin;


    //경험치의 획득 및 레벨업
    public void AddExp(float amount)
    {
        exp += amount;
        while (exp > expToNextLevel)
        {
            exp -= expToNextLevel;
            level++;
            expToNextLevel = GetNextLevelExp(level);
        }
        
    }

    public void AddCoin(int amount)
    {
        coin += amount;
    }
   
    private float GetNextLevelExp(int currentLevel)
    {
        return 100f * Mathf.Pow(1.2f, currentLevel - 1);
        // 100f * 1,2^(현재레벨-1)
    }

    //Player의 스킬 리스트

}
