using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour {

	public Transform target;
	public float dist = 0.25f;
	public float height = 0.125f;

	private Transform tr;
	void Start ()
	{
		tr = GetComponent<Transform>();
	}
	
	void LateUpdate () 
	{
		if (GameManager.Instance.isPlayedSet)
		{
			CameraTurn();
		}
		else
		{
			transform.position = new Vector3(27.709f, 1.631f, 3.414f);
			transform.rotation = Quaternion.Euler(0, 90, 0);
		}
	}

	void CameraTurn()
	{
		tr.position = target.position - (Vector3.forward * dist) - (Vector3.up * height);
		tr.LookAt (target);
		
	}
}
