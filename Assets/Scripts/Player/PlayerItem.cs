using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerItem : MonoBehaviour
{
    [SerializeField] private Toggle toggle;
    [SerializeField] private TradingItemVisual itemVisual;
    private TradingItem itemData;
    public static event Action<PlayerItem, bool> OnItemTick;
    private void Start()
    {
        toggle.isOn = false;
        toggle.onValueChanged.AddListener(OnTickItem);
    }
    public TradingItem GetTradingItem() => itemData;
    public void SetData(TradingItem item)
    {
        itemData = item;
    }
    public bool GetToggleState() => toggle.isOn;
    public void ShowVisual()
    {
        if (itemData != null)
        {
            itemVisual.Init(itemData);
        }
    }
    private void OnTickItem(bool hasTick)
    {
        OnItemTick?.Invoke(this, hasTick);
    }
    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
