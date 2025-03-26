using UnityEngine;

public class GoldManager : Manager
{
    public int currentGold;

    public void AddGold(int amount)
    {
        currentGold += amount;
    }
}