using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class yongAniController : MonoBehaviour {

	private Animator animator;

	// Use this for initialization
	void Start () 
	{
		animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () 
	{	
		if (GameManager.Instance.isLoadedSet)
		{
			transform.position = new Vector3(108.89f, -1.09f, 108.2f);
			GameManager.Instance.isLoadedSet = false;
			StartCoroutine(Loading());
		}
			if (transform.position.z >= 92)
				transform.position -= new Vector3(0f, 0f, 0.02f);
	}

	IEnumerator Loading()
	{
		animator.SetTrigger("isLoading");
		yield return new WaitForSeconds(2);
		animator.SetTrigger("isRolling");
		yield return new WaitForSeconds(3);
		animator.SetTrigger("isStanding");
	}

}
