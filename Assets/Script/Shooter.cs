using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour {

	[SerializeField] private Animator animator;
	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			StartCoroutine(Shoot());
			collision.gameObject.GetComponent<Rigidbody>().AddForce(0,0,50, ForceMode.VelocityChange);
		}
	}

	IEnumerator Shoot()
	{
		yield return new WaitForSeconds(3);
	}
}
