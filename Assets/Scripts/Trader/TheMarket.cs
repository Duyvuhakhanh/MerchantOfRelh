using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TheMarket : MonoBehaviourSingleton<TheMarket>, ITradeable
{
    [SerializeField] private int[] coinsChargeArray = new int[3];
    [SerializeField] private Player player;
    public void OnSeasonChange()
    {
    }
    public void OnSellToMarket()
    {
        List<TradingItem> tickedItems = new();
        player.playerTickedItems.ForEach(item =>
        {
            tickedItems.Add(item.GetTradingItem());
        });
        OnTradingWithPlayer(player, tickedItems);
    }
    public void OnTradingWithPlayer(Player player, params object[] @params)
    {
        if (@params.Length > 0 && @params[0] is List<TradingItem>)
        {

            List<TradingItem> items = (List<TradingItem>)@params[0];

            int totalCoins = 0;

            items = items.OrderBy(x => x.itemId).ToList();
            int countDup = 1;
            for (int i = 0; i < items.Count; i++)
            {
                TradingItem item = items[i];
                totalCoins += SeasonManager.Instance.GetItemPriceBySeason(item);
                if(i >= 1)
                {
                    if (items[i -1].itemId == item.itemId)
                    {
                        countDup++;
                    }
                    else
                    {
                        totalCoins += GetCoinsChargeByItemsAmount(countDup);
                        countDup = 1;
                    }
                }
               
                player.RemoveItemFromHand(item);
            }
            if (countDup > 1)
            {
                totalCoins += GetCoinsChargeByItemsAmount(countDup);
            }
            player.AdjustPlayerCoins(totalCoins);
        }

    }
    private int GetCoinsChargeByItemsAmount(int ItemsAmount)
    {
        if(ItemsAmount < 2) return 0;
        return coinsChargeArray[ItemsAmount - 2];
    }
}
