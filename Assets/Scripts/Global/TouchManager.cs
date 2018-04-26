using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchManager : MonoBehaviour
{
	public static TouchManager Instance
	{
		get
		{
			return _instance;
		}
	}
	public bool TouchIsUsed
	{
		get
		{
			return _touchIsUsed;
		}
	}
	private static TouchManager _instance;
	private bool _touchIsUsed;

	private void Awake()
	{
		if (!_instance)
			_instance = this;
		_touchIsUsed = false;
	}

	public void SetIsUsed(bool b)
	{
		_touchIsUsed = b;
	}
}
