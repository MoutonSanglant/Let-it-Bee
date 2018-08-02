using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PollenManager : MonoBehaviour 
{

	public int FreeGrainLimit;

	void Update () 
	{
		if (transform.childCount > FreeGrainLimit) 
		{
			Destroy (transform.GetChild (0).gameObject);
		}
	}
}
