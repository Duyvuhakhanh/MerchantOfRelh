using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheMarket : MonoBehaviourSingleton<TheMarket>, ITradeable
{
    [SerializeField] private int[] coinsChargeArray = new int[3];
    public void OnSeasonChange()
    {
    }

    public void OnTradingWithPlayer(Player player, params object[] @params)
    {
        if (@params.Length > 0 && @params[0] is List<TradingItem>)
        {

            List<TradingItem> items = (List<TradingItem>)@params[0];

            if (items.Count <= 1) return;

            int totalCoins = 0;


            foreach (TradingItem item in items)
            {
                totalCoins += SeasonManager.Instance.GetItemPriceBySeason(item);
            }
            int itemAmount = items.Count;

            totalCoins += GetCoinsChargeByItemsAmount(itemAmount);

            player.AdjustPlayerCoins(totalCoins);
        }

    }
    private int GetCoinsChargeByItemsAmount(int ItemsAmount)
    {
        if(ItemsAmount >= coinsChargeArray.Length) return 0;
        return coinsChargeArray[ItemsAmount];
    }
}
