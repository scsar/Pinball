using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour, IItem 
{
	public void use(GameObject target)
	{
	}
	public GameObject Guse(GameObject target)
	{
		GameManager.Instance.scoreSet = 100;
		return null;
	}
}
