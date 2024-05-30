using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StoreHouse : MonoBehaviourSingleton<StoreHouse> ,ITradeable
{
    [SerializeField] private List<TradingItem> itemsStored;
    public void OnSeasonChange()
    {
        throw new System.NotImplementedException();
    }

    public void OnTradingWithPlayer(Player player, params object[] @params)
    {
        throw new System.NotImplementedException();
    }

}
