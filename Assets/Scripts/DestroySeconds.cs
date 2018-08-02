using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySeconds : MonoBehaviour
{
	public float Seconds;

	void Start ()
	{
		Invoke("Destroy", Seconds);
	}
	
	void Destroy()
	{
		Destroy(gameObject);
	}
}
