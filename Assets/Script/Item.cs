using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IItem
{
	// target = 아이템 효과를 적용받을 대상 = 플레이어
	GameObject Guse(GameObject target);
	void use(GameObject target);
}


public class Item : MonoBehaviour, IItem
{
	[SerializeField] private GameObject itemImage;
	protected GameObject itemch;
	private int itemcount = 0;

	public GameObject Guse(GameObject target)
	{
		for (int i = 0; i < 3; i++)
		{
			itemImage.transform.GetChild(i).gameObject.SetActive(false);
		}
		itemcount = Random.Range(0, 3);
		itemch = itemImage.transform.GetChild(itemcount).gameObject;
		itemch.SetActive(true);
		Debug.Log(itemch.name);
		return itemch;
	}

	public void use(GameObject target){}
}	
