using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlayerDataSO : ScriptableObject
{
    public int Coins = 20;
    public int Reputation = 0;
    public event Action<int> OnCoinChange;
    public event Action<int> OnReputaionChange;

    public void SetPlayerCoins(int amount) => Coins = amount;

    public void AdjustPlayerResponsitive(int amount)
    {
        Reputation += amount;
        if (Reputation < 0)
        {
            Reputation = 0;
        }
        OnReputaionChange?.Invoke(amount);
    }
    public void SetPlayerReponsitive(int amount) => Reputation = amount;

    public void AdjustPlayerCoins(int amount)
    {
        var playerCoins = Coins;
        var pre = playerCoins;
        playerCoins += amount;
        if (playerCoins < 0)
        {
            playerCoins = 0;
        }
        OnCoinChange?.Invoke(playerCoins - pre);
    }
    public int GetPlayerCoins() => Coins;
}
