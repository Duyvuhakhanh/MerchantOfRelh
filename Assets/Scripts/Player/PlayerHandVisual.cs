using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandVisual : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private PlayerItem itemPrefab;
    [SerializeField] private Transform itemHoldPoint;
    [Header("Debug")]
    [SerializeField] private List<PlayerItem> playerItems;

    public void ShowHandVisual()
    {
        ResetHand();
        foreach (TradingItem item in player.GetPlayerItems())
        {
            PlayerItem playerItem = Instantiate(itemPrefab, itemHoldPoint);
            playerItem.SetData(item);
            playerItem.ShowVisual();
            playerItem.gameObject.SetActive(true);
            playerItems.Add(playerItem);
        }
    }

    private void ResetHand()
    {
        player.OnResetHand();

        foreach (Transform itemTrans in itemHoldPoint)
        {
            if (itemTrans != itemPrefab.transform) Destroy(itemTrans.gameObject);
        }
    }
}
