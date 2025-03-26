using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StatisticsManager : Manager
{
    public int currentLevel = 1;
    public int currentExp = 0;
    public int requiredExp;

    private Dictionary<int, int> requiredExpPerLevel = new()
    {
        {1, 100},
        {2, 120},
        {3, 140},
        {4, 160},
        {5, 180},
        {6, 200},
        {7, 220},
        {8, 240},
        {9, 260}
    };

    public override void Initialize(GeneralManager manager)
    {
        base.Initialize(manager);
        requiredExp = requiredExpPerLevel[currentLevel];
    }

    public void AddExp(int amount)
    {
        currentExp += amount;

        if(currentExp >= requiredExp)
        {
            LevelUp();    
        }
    }

    private void LevelUp()
    {
        currentExp -= requiredExp;
        currentLevel ++;
        requiredExp = requiredExpPerLevel[currentLevel];
    }
}