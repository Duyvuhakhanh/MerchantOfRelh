using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviourSingleton<Shop>
{
    [SerializeField] private List<ShopItem> shopItems;
    [SerializeField] private ShopItem shopItemPrefabs;
    [SerializeField] private Transform itemHolder;
    [SerializeField] private List<TradingItem> itemsPool = new();
    [SerializeField] private ItemDatabaseConfig itemDatabaseConfig;
    

    [ContextMenu("Init Pool")]
    void InitPool()
    {
        var dupCount = 4;
        for (int i = 0; i < dupCount; i++)
        {
            itemsPool.AddRange(itemDatabaseConfig.tradingItems);
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
        StartCoroutine(IDisableGrid());
    }
    IEnumerator IDisableGrid()
    {
        yield return new WaitForEndOfFrame();
        itemHolder.GetComponent<GridLayoutGroup>().enabled = false;
    }


    private void ShopItem_OnBuyShopItem(ShopItem shopItem)
    {
        var tradingItem = shopItem.GetTradingItem();
        if (Player.Instance.TryBuyAnItem(tradingItem))
        {
            shopItem.gameObject.SetActive(false);
        }
    }

    [ContextMenu("RefreshShop")]
    public void RefreshShop()
    {
        for (int i = 0; i < shopItems.Count; i++)
        {
            var item = shopItems[i];
            if(item.gameObject.activeSelf) ReturnItemToPool(item.GetTradingItem());
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


    public void OnSeasonChange()
    {
        RefreshShop();
    }
}
