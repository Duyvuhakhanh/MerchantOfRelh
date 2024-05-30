using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITradeable {
    public void OnTradingWithPlayer(Player player, params object[] @params);
    public void OnSeasonChange();
}
