using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : MonoBehaviour 
{

	public float RawPushForce;
	public float MaxDist, MinDist;

	[HideInInspector]
	public Vector3 RawPushDirection, PushDirection;
	[HideInInspector]
	public float TouchDist;

	private BoxCollider2D _col;
	private float _pushLength, _pushForce;

	void Awake() 
	{
		_col = GetComponent<BoxCollider2D> ();
	}

	void Update() 
	{
		_pushLength = Mathf.Clamp (TouchDist, MinDist, MaxDist);
		PushDirection = RawPushDirection.normalized;
		_pushForce = (RawPushForce / 10) + (_pushLength / MaxDist);
	}

	private void OnTriggerStay2D (Collider2D collider) 
	{
		float _colDist = Vector3.Magnitude (collider.transform.position - transform.parent.position);
		PollenGrain _grain = collider.gameObject.GetComponent<PollenGrain> ();
		if (collider.tag == "Movable" &&  _colDist <= _pushLength) 
		{
			_grain.Pushed = true;
			ApplyForce (collider, _colDist);
		}
	}

	private void OnTriggerExit2D(Collider2D collider) {
		PollenGrain _grain = collider.gameObject.GetComponent<PollenGrain> ();
		if (_grain && !_grain.AttachedToPlayer) 
		{
			_grain.Pushed = false;
		}
	}

	private void ApplyForce (Collider2D collider, float colDist) {
		Rigidbody2D _colliderRig = collider.gameObject.GetComponent<Rigidbody2D> ();
		float _reducedColDist = Mathf.Clamp (colDist, 1f, 10f) / MaxDist;
		_colliderRig.AddForce (PushDirection * (_pushForce - _reducedColDist));
	}
}
