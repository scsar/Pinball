using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb :  MonoBehaviour,  IItem
{
	public void use(GameObject target)
	{
		Debug.Log("폭탄 사용");
		Rigidbody _Brigidbody = target.GetComponent<Rigidbody>();
		if (_Brigidbody != null)
		{
			_Brigidbody.AddForce(0, 10, 0, ForceMode.VelocityChange);
		}
		StartCoroutine(Waituse());
	}

	IEnumerator Waituse()
	{
		yield return new WaitForSeconds(0.5f);
		gameObject.SetActive(false);
	}
	public GameObject Guse(GameObject target){ return null;}
}
