using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour 
{

	[SerializeField] private int speed = 4;
	Vector3 dir = Vector3.zero;
	private Animator animator;

	// Use this for initialization
	void Awake () 
	{
		animator = GetComponent<Animator>();
	}
	
	void Update()
	{
		if (dir == Vector3.zero)
			animator.SetBool("isWalking", false);
	}

	// Update is called once per frame
	void FixedUpdate () 
	{
		Move();
	}
	void Move()
	{
		animator.SetBool("isWalking", true);
		float x = Input.GetAxis("Horizontal");
		float z = Input.GetAxis("Vertical");
		dir.x = x;
		dir.z = z;
		transform.Translate(dir * speed * Time.deltaTime,Space.Self);
	}
}