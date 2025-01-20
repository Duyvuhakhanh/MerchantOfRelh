using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
    {
    public PlayerDataSO playerData;
    [SerializeField] private PlayerHandVisual handVisual;
    public List<PlayerItem> playerTickedItems;


    private List<TradingItem> tradingItems = new();
    public event Action<TradingItem> OnHandChange;

    private void OnEnable()
    {
        PlayerItem.OnItemTick += OnTickPlayerItem;
    }
    private void OnDisable()
    {
        PlayerItem.OnItemTick -= OnTickPlayerItem;
    }
    public int GetItemsAmount() => tradingItems.Count;

    public List<TradingItem> GetPlayerItems() => tradingItems;


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
        if (playerData.Coins < adjustCoin) return false;

        playerData.AdjustPlayerCoins(-adjustCoin);
        AddItemToHand(item);

        int reponsitiveGain = 1;
        playerData.AdjustPlayerResponsitive(reponsitiveGain);
        return true;
    }

    public void Trade(ITradeable target,params object[] @params)
    {
        target.OnTradingWithPlayer(this, @params);
    }


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

    internal void AdjustPlayerCoins(int v)
    {
        playerData.AdjustPlayerCoins(v);
    }

    internal void AdjustPlayerResponsitive(int rp)
    {
        playerData.AdjustPlayerResponsitive(rp);
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
