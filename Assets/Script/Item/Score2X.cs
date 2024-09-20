using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score2X : MonoBehaviour, IItem 
{
	public void use(GameObject target)
	{
		Debug.Log("2배사용");
		GameManager.Instance.xSet = 2;
		StartCoroutine(Waituse());	
	}

	IEnumerator Waituse()
	{
		yield return new WaitForSeconds(0.5f);
		gameObject.SetActive(false);
	}

	public GameObject Guse(GameObject target){ return null;}
}
