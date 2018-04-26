using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

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
		if (!GameObject.Find("EventTrigger"))
		{
			gameObject.AddComponent<EventSystem>();
			gameObject.AddComponent<StandaloneInputModule>();
		}
	}

	public void SetIsUsed(bool b)
	{
		_touchIsUsed = b;
	}
}
