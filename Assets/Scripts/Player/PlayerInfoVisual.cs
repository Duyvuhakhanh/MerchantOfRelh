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
        player.OnCoinChange += Player_OnCoinChange;
        player.OnReputaionChange += Player_OnReputaionChange;
        Player_OnReputaionChange(0);
        Player_OnCoinChange(0);
    }

    private void Player_OnReputaionChange(int obj)
    {
        playerReputation.text = "Reputation: " + player.GetPlayerReputation().ToString();
    }

    private void Player_OnCoinChange(int obj)
    { 
        playerGold.text = "Coin: " + player.GetPlayerCoins().ToString();
    }
}
