using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItem : MonoBehaviour
{
    private TradingItem item;
    [SerializeField] TradingItemVisual itemVisual;
    public event Action<ShopItem> OnBuyShopItem;
    public void OnShopItemClick()
    {
        OnBuyShopItem?.Invoke(this);
        Debug.Log("You buy " + item.itemId);
    }
    public void Init (TradingItem item)
    {
        this.item = item;
        itemVisual.Init(item);
    }
    public TradingItem GetTradingItem() => item;
}
