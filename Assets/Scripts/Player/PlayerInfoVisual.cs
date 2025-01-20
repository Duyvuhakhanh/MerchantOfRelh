using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerInfoVisual : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private TextMeshProUGUI playerGold;
    [SerializeField] private TextMeshProUGUI playerReputation;
    private void Start()
    {
        player.playerData.OnCoinChange += Player_OnCoinChange;
        player.playerData.OnReputaionChange += Player_OnReputaionChange;
        Player_OnReputaionChange(0);
        Player_OnCoinChange(0);
    }

    private void Player_OnReputaionChange(int reputaion)
    {
        playerReputation.text = "Reputation: " + reputaion;
    }

    private void Player_OnCoinChange(int coin)
    { 
        playerGold.text = "Coin: " + coin;
    }
}
