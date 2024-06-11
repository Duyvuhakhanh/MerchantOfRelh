using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TradingItemVisual : MonoBehaviour
{
    [SerializeField] private Image itemSprite;
    [SerializeField] private TextMeshProUGUI springPrice;
    [SerializeField] private TextMeshProUGUI summerPrice;
    [SerializeField] private TextMeshProUGUI autumnPrice;
    [SerializeField] private TextMeshProUGUI winterPrice;
    public void Init (TradingItem item)
    {
        springPrice.text = $"Spring: " + item.price.springPrice.ToString();
        summerPrice.text = $"Summer: " + item.price.summerPrice.ToString();
        autumnPrice.text = $"Autumn: " + item.price.autumnPrice.ToString();
        winterPrice.text = $"Winter: " + item.price.winterPrice.ToString();
        itemSprite.sprite = item.itemSprite;
    }
}

