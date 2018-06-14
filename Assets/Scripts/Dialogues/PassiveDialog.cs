using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveDialog : MonoBehaviour {
	
	public GameObject bubblebox;

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			bubblebox.SetActive(true);
		}
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			bubblebox.SetActive(false);
		}
	}
}
