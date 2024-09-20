using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGenerator : MonoBehaviour {

	[SerializeField] private GameObject [] Items;
	[SerializeField] private GameObject ItemPrefeb;
	// Use this for initialization
	void Awake () 
	{
		for (int i = 0; i < Items.Length; i++)
		{
			GameObject item = Instantiate(ItemPrefeb,Items[i].transform);
			item.transform.position = Items[i].transform.position;
		}	
	}
}
