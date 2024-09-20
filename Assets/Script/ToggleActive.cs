using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleActive : MonoBehaviour {

	private int rezenTime = 8;
	private GameObject ch;

	private bool isCoroutineRunning = false;
	// Use this for initialization
	void Start () 
	{
		ch = transform.GetChild(0).gameObject;
	}
	
	void LateUpdate()
	{
		
	}
	// Update is called once per frame
	void Update () 
	{
		if (!ch.activeSelf && !isCoroutineRunning)
		{
			StartCoroutine(toggleActive());
		}	
	}

	IEnumerator toggleActive()
	{
		isCoroutineRunning = true;
		yield return new WaitForSeconds(rezenTime);
		ch.SetActive(true);
		
		isCoroutineRunning = false;
	}
}
