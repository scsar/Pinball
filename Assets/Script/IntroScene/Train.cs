using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Train : MonoBehaviour {

	[SerializeField] private int moveSpeed = 10;
	private float Timer = 0f;

	private Vector3 last;
	// Use this for initialization
	void Awake ()
	{
		last = transform.position;
	}
	
	// Update is called once per frame
	void Update () 
	{
		Timer += Time.deltaTime;
        if(Timer > 5f)
        {
            Timer = 0;
            transform.position = last;
        }

		transform.Translate(Vector3.right * Time.deltaTime * moveSpeed);
	}
}
