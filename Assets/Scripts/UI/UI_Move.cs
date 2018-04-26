using UnityEngine;
using UnityEngine.EventSystems;

public class UI_Move : EventTrigger
{
	private Move _move;

	private void Awake()
	{
		_move = GameObject.FindGameObjectWithTag("Player").GetComponent<Move>();
	}

	public override void OnPointerDown(PointerEventData data)
	{
		_move.OnTouchDown();
	}

	public override void OnPointerUp(PointerEventData data)
	{
		_move.OnTouchUp();
	}
}
