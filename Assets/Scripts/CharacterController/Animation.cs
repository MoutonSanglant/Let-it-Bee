using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation : MonoBehaviour
{
	private Rigidbody2D Rigidbody;
	private Animator Animator;

	private void Start()
	{
		Rigidbody = GetComponent<Rigidbody2D>();
		Animator = GetComponentInChildren<Animator>();
	}

	// Update is called once per frame
	void Update ()
	{
		Animator.SetFloat("SpeedX", Rigidbody.velocity.x);
		Animator.SetFloat("SpeedY", Rigidbody.velocity.y);
		//Debug.Log(Rigidbody.velocity.x);
	}
}
