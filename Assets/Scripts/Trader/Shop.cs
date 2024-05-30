using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Shop : MonoBehaviourSingleton<Shop>, ITradeable
{
    [SerializeField] private List<ShopItem> shopItems;
    [SerializeField] private ShopItem shopItemPrefabs;
    [SerializeField] private Transform itemHolder;
    [SerializeField] private List<TradingItem> itemsPool = new();
    [ContextMenu("Init Pool")]
    void InitPool()
    {
        for (int i = 0; i < 100; i++)
        {

            var newItem = new TradingItem() { itemId = i +"s", name = "a" + i, price = new() { springPrice = i, summerPrice = i * 2, autumnPrice = i * 3, winterPrice = i*4} };
            itemsPool.Add(newItem);
        }
    }
    private void Start()
    {
        Init();
    }
    [ContextMenu("Init")]

    private void Init()
    {
        InitPool();
        int itemMax = 4;
        for (int i = 0; i < itemMax; i++)
        {
            ShopItem shopItem = Instantiate(shopItemPrefabs, itemHolder);
            shopItem.OnBuyShopItem += ShopItem_OnBuyShopItem;
            shopItem.Init(GetItemFromPool());
            shopItem.gameObject.SetActive(true);
            shopItems.Add(shopItem);
        }
    }



    private void ShopItem_OnBuyShopItem(ShopItem shopItem)
    {
        var tradingItem = shopItem.GetTradingItem();
        Player.Instance.BuyAnItem(tradingItem);
        shopItem.gameObject.SetActive(false);
    }

    [ContextMenu("RollNewShop")]
    public void RollNewShop()
    {
        for (int i = 0; i < shopItems.Count; i++)
        {
            var item = shopItems[i];
            ReturnItemToPool(item.GetTradingItem());
            item.Init(GetItemFromPool());
            item.gameObject.SetActive(true);
        }
    }
    private TradingItem GetItemFromPool()
    {
        int randomIndex = UnityEngine.Random.Range(0, itemsPool.Count);
        var rs = itemsPool[randomIndex];
        RemoveItemFromPool(rs);
        return rs;
    }
    private void ReturnItemToPool(TradingItem item)
    {
        itemsPool.Add(item);
    }
    private void RemoveItemFromPool(TradingItem item)
    {
        if (itemsPool.Contains(item))
        {
            itemsPool.Remove(item);
        }
    }

    public void OnTradingWithPlayer(Player player, params object[] @params)
    {
        if (@params.Length > 0 && @params[0] is TradingItem)
        {

        }
    }

    public void OnSeasonChange()
    {
        RollNewShop();
    }
}
