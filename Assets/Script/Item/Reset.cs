using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reset : MonoBehaviour, IItem {

	public void use(GameObject target)
	{
		Debug.Log("리셋사용");
		BallPlayerController ballPlayerController = target.GetComponent<BallPlayerController>();
		if (ballPlayerController != null)
		{
			ballPlayerController.forceset = false;
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
