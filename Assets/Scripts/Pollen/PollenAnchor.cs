﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PollenAnchor : MonoBehaviour 
{

	public float GizmosRadius;

	void OnDrawGizmos() 
	{
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere (transform.position, GizmosRadius);
	}
}
