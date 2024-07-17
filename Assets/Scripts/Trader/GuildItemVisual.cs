using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuildItemVisual : MonoBehaviour
{
    [SerializeField] TradingItemVisual itemVisual;
    public void Init(TradingItem item)
    {
        itemVisual.Init(item);
    }
}
