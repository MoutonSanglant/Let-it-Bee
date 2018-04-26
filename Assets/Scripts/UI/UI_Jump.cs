using UnityEngine;
using UnityEngine.EventSystems;

public class UI_Jump : EventTrigger
{
	private Jump _jump;

	private void Awake()
	{
		_jump = GameObject.FindGameObjectWithTag("Player").GetComponent<Jump>();
	}

	public override void OnPointerDown(PointerEventData data)
	{
		_jump.OnTouchDown();
	}
}
