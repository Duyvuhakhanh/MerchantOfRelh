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

    private void Start()
    {
        player.OnHandChange += Player_OnHandChange;
    }

    private void Player_OnHandChange(TradingItem obj)
    {
        RefreshHandVisual();
    }

    public void RefreshHandVisual()
    {
        ResetHand();
        RefeshHand();
    }

    private void RefeshHand()
    {
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
