using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviourSingleton<Player>
{
    [SerializeField] private int playerCoins = 20;
    [SerializeField] private int playerReputation = 0;
    [SerializeField] private PlayerHandVisual handVisual;
    public List<PlayerItem> playerTickedItems;
    public event Action<int> OnCoinChange;
    public event Action<int> OnReputaionChange;

    private List<TradingItem> tradingItems = new();
    public event Action<TradingItem> OnHandChange;

    private void Start()
    {
        PlayerItem.OnItemTick += OnTickPlayerItem;
    }

    public int GetItemsAmount() => tradingItems.Count;

    public List<TradingItem> GetPlayerItems() => tradingItems;

    public int GetPlayerCoins() => playerCoins;

    public void AddItemToHand(TradingItem item) => tradingItems.Add(item);

    public void RemoveItemFromHand(TradingItem item)
    {
        if (!tradingItems.Contains(item)) return;
        tradingItems.Remove(item);
        OnHandChange?.Invoke(item);
    }
    public bool TryBuyAnItem(TradingItem item)
    {
        int adjustCoin = SeasonManager.Instance.GetItemPriceBySeason(item);
        if (playerCoins < adjustCoin) return false;

        AdjustPlayerCoins(-adjustCoin);
        AddItemToHand(item);

        int reponsitiveGain = 1;
        AdjustPlayerResponsitive(reponsitiveGain);
        return true;
    }

    public void Trade(ITradeable target,params object[] @params)
    {
        target.OnTradingWithPlayer(this, @params);
    }
    public void AdjustPlayerCoins(int amount)
    {
        var pre = playerCoins;
        playerCoins += amount;
        if (playerCoins < 0)
        {
            playerCoins = 0;
        }
        OnCoinChange?.Invoke(playerCoins - pre);
    }
    public void SetPlayerCoins(int amount) => playerCoins = amount;
 
    public int GetPlayerReputation() =>  playerReputation;
    public void AdjustPlayerResponsitive(int amount)
    {
        playerReputation += amount;
        if (playerReputation < 0)
        {
            playerReputation = 0;
        }
        OnReputaionChange?.Invoke(amount);
    }
    public void SetPlayerReponsitive(int amount) => playerReputation = amount;

    public void OnButtonShowHandClick()
    {
        handVisual.RefreshHandVisual();
        handVisual.gameObject.SetActive(true);
    }
    public void OnTickPlayerItem(PlayerItem item, bool hasTick)
    {
        if (item.GetToggleState() == true)
        {
            if (!playerTickedItems.Contains(item))
            {
                playerTickedItems.Add(item);
            }
        }
        else
        {
            if (playerTickedItems.Contains(item))
            {
                playerTickedItems.Remove(item);
            }
        }
    }
    public void OnResetHand()
    {
        playerTickedItems.Clear();
    }
}
[Serializable]
public class Price
{
    public int springPrice;
    public int summerPrice;
    public int autumnPrice;
    public int winterPrice;
}
