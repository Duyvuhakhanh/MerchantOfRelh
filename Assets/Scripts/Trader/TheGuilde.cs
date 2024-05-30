using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class TheGuilde : MonoBehaviourSingleton<TheGuilde>, ITradeable
{
    [SerializeField] private List<TradingItem> tradingItems;

    [SerializeField] private List<TradingItem> itemsPool;
    [SerializeField] int[] reputationConfigArray = new int[3];
    [SerializeField] int[] coinConfigArray = new int[3];
    public void OnSeasonChange()
    {
        
    }
    [ContextMenu("RollNewShop")]
    public void RollNewItemsGuid()
    {
        int itemsCount = 4;

        foreach (var item in tradingItems)
        {
            ReturnItemToPool(item);
        }
        tradingItems.Clear();

        for (int i = 0; i < itemsCount; i++)
        {
            tradingItems.Add(GetItemFromPool());
        }
    }

    private TradingItem GetItemFromPool()
    {
        int randomIndex = UnityEngine.Random.Range(0, itemsPool.Count);
        var rs = itemsPool[randomIndex];
        RemoveItemFromPool(rs);
        return rs;
    }

    private void RemoveItemFromPool(TradingItem item)
    {
        if (itemsPool.Contains(item))
        {
            itemsPool.Remove(item);
        }
    }

    private void ReturnItemToPool(TradingItem item)
    {
        itemsPool.Add(item);
    }

    public void OnTradingWithPlayer(Player player, params object[] @params)
    {
        if (@params.Length > 0 && @params[0] is List<TradingItem>)
        {
            List<TradingItem> items = (List<TradingItem>)@params[0];
            int count = 0;
            int priceRefund = 0;
            foreach (var item in items)
            {
                if(IsInGuildItems(item))
                {
                    count++;

                    priceRefund += SeasonManager.Instance.GetItemPriceBySeason(item);
                }
            }

            int playerCoinGain = GetCoinChargeByItemsAmount(count);
            int playerReputationGain = GetReputationByItemsAmount(count);

            player.AdjustPlayerCoins(playerCoinGain + priceRefund);
            player.AdjustPlayerResponsitive(playerReputationGain);
        }
    }
    public bool IsInGuildItems(TradingItem item)
    {
        return tradingItems.Any(x => x.itemId == item.itemId);
    }
    public int GetCoinChargeByItemsAmount(int amount)
    {
        return coinConfigArray[amount];
    }
    public int GetReputationByItemsAmount(int amount)
    {
        return reputationConfigArray[amount];
    }
    public List<TradingItem> GetListGuildTrading() => tradingItems;
}
