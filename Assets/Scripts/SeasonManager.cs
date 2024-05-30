using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SeasonManager : MonoBehaviourSingleton<SeasonManager>
{

	private static SeasonManager instance;
	public static event Action OnSeasonChange;

	private SeasonSate curSeason;
	public enum SeasonSate
	{
		Srping,
		Summner,
		Autumn,
		Winter
	}

	public int GetItemPriceBySeason(TradingItem item)
	{
		int price = 0;
		switch (curSeason)
		{
			case SeasonSate.Srping:
				price = item.price.springPrice;
				break;
			case SeasonSate.Summner:
                price = item.price.summerPrice;

                break;
			case SeasonSate.Autumn:
                price = item.price.autumnPrice;

                break;
			case SeasonSate.Winter:
                price = item.price.winterPrice;

                break;
		}
		return price;
	}
	public SeasonSate GetCurSeason()
	{
		return curSeason;
	}
	public void GoToNextSeason()
	{
		curSeason++;
        OnSeasonChange?.Invoke();

    }

}
