using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAniController : MonoBehaviour {

	[SerializeField] private Animator animator;
	// Use this for initialization
	void Start () 
	{
		StartCoroutine(Paddle());
	}
	
	// Update is called once per frame
	void Update () 
	{

	}

	IEnumerator Paddle()
	{
		while(true)
		{
			animator.SetBool("Paddle", true);
			yield return new WaitForSeconds(5);
			animator.SetBool("Paddle", false);
			yield return new WaitForSeconds(5);
		}
	}
}
