using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PollenGrain : MonoBehaviour 
{

	public string FreeGrainContainer;
	public float AttachSpeed, GravityScale;
	public float FadeTime;
	[HideInInspector]
	public PollenColor GrainColor;
	[HideInInspector]
	public bool MovingTowardPlayer, AttachedToPlayer;
	[HideInInspector]
	public bool OnFlower, Fading, Pushed;

	private SpriteRenderer _sprite;
	private GameObject _childCollider;
	private Rigidbody2D _rig;

	void Start() 
	{
		_rig = GetComponent<Rigidbody2D> ();
		_sprite = GetComponent<SpriteRenderer> ();
		_childCollider = transform.GetChild (0).gameObject;
		transform.hasChanged = false;
		OnFlower = true;
		SetColor();
	}

	void Update() 
	{
		if (AttachedToPlayer) 
		{
			_rig.gravityScale = 0;
			if (MovingTowardPlayer) 
			{
				MoveToPlayer (); 
			} 
			else 
			{
				StayOnPlayer ();
			}
		} 
		else if (transform.hasChanged && !AttachedToPlayer) 
		{
			ReleaseGrain ();
			if (!Pushed) { Fading = true; }
		}
		if (Fading) { FadeOut (); }
	}

	private void MoveToPlayer() 
	{
		_childCollider.layer = 11;
		float _moveSpeed = AttachSpeed * Time.deltaTime;
		transform.position = Vector3.MoveTowards (transform.position, transform.parent.position, _moveSpeed);
		if (transform.position == transform.parent.position) 
		{
			MovingTowardPlayer = false; 
		}
	}

	private void StayOnPlayer() 
	{
		transform.position = transform.parent.position;
	}

	private void ReleaseGrain()
	{
		OnFlower = false;
		_childCollider.layer = 10;
		_rig.gravityScale = GravityScale;
		transform.parent = GameObject.Find (FreeGrainContainer).transform;
	}

	private void FadeOut() {
		if (_sprite.color.a <= 0) { Destroy (gameObject); }
		Color _currentColor = _sprite.color;
		float _alphaChange = Time.deltaTime / FadeTime;
		_currentColor.a -= _alphaChange;
		_sprite.color = _currentColor;
	}

	private IEnumerator DelayedDespawn() 
	{
		yield return new WaitForSeconds (FadeTime);
		Destroy (gameObject);
	}

	private void SetColor() 
	{
		if (GrainColor == PollenColor.Yellow) { _sprite.color = Color.yellow; }
		else if (GrainColor == PollenColor.Green) { _sprite.color = Color.green; }
		else if (GrainColor == PollenColor.Cyan) { _sprite.color = Color.cyan; }
		else if (GrainColor == PollenColor.Magenta) { _sprite.color = Color.magenta; }
	}
}
