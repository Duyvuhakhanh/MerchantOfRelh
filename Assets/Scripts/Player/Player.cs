using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviourSingleton<Player>
{
    [SerializeField] private int playerCoins;
    [SerializeField] private int playerReputation;
    [SerializeField] private PlayerHandVisual handVisual;
    [SerializeField] private List<PlayerItem> playerTickedItems;

    private List<TradingItem> tradingItems = new();

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
    }
    public void BuyAnItem(TradingItem item)
    {
        int adjustCoin = -SeasonManager.Instance.GetItemPriceBySeason(item);
        AdjustPlayerCoins(adjustCoin);
        AddItemToHand(item);

        int reponsitiveGain = 1;
        AdjustPlayerResponsitive(reponsitiveGain);
    }

    public void Trade(ITradeable target,params object[] @params)
    {
        target.OnTradingWithPlayer(this, @params);
    }
    public void AdjustPlayerCoins(int amount)
    {
        playerCoins += amount;
        if (playerCoins < 0)
        {
            playerCoins = 0;
        }
    }
    public void SetPlayerCoins(int amount) => playerCoins = amount;
 
    public int GetPlayerResponsitive() =>  playerReputation;
    public void AdjustPlayerResponsitive(int amount)
    {
        playerReputation += amount;
        if (playerReputation < 0)
        {
            playerReputation = 0;
        }
    }
    public void SetPlayerReponsitive(int amount) => playerReputation = amount;

    public void OnButtonShowHandClick()
    {
        handVisual.ShowHandVisual();
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
}
[Serializable]
public class Price
{
    public int springPrice;
    public int summerPrice;
    public int autumnPrice;
    public int winterPrice;
}
