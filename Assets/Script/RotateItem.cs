using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateItem : MonoBehaviour {

	private float rotSpeed = 70f;


	public float amplitude = 1.0f; // 진폭
    public float frequency = 1.0f;   // 주파수
	private Vector3 move;

	// Use this for initialization
	void Start () 
	{
		move = transform.position;
	}
	
	// Update is called once per frame
	void Update () 
	{
		

		transform.Rotate(new Vector3(0,1,0) * rotSpeed * Time.deltaTime);	
	}

	void LateUpdate()
	{
		float newY = move.y + amplitude * Mathf.Sin(Time.time * frequency);
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
	}
}
