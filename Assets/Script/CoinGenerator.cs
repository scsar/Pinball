using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinGenerator : MonoBehaviour 
{

	[SerializeField] private GameObject [] Coins;
	[SerializeField] private GameObject coinPrefeb;
	// Use this for initialization
	void Awake () 
	{
		for (int i = 0; i < Coins.Length; i++)
		{
			GameObject coin = Instantiate(coinPrefeb,Coins[i].transform);
			coin.transform.position = Coins[i].transform.position;
		}
	}
}
