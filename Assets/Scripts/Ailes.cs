using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ailes : MonoBehaviour 
{

	public GameObject WindPushArea, PollenContainer;
	public float ThrowRate, ThrowStartDelay;
	public bool AutoFire;

	private CircleCollider2D _col;
	private Touch _firstTouch, _fireTouch;
	private Vector3 _firstTouchPos;
	private Vector2 _touchHeading;
	private Wind _wind;
	private Move _playerMove;
	private Jump _playerJump;

	private bool _touchOnPlayer, _isFiring;
	private float _touchDist, _activationDist;
	private float _initMoveSpeed, _initJumpForce;


	void Awake() 
	{
		_col = GetComponent<CircleCollider2D> ();
		_wind = WindPushArea.GetComponent<Wind> ();
		_playerMove = GetComponentInParent<Move> ();
		_playerJump = GetComponentInParent<Jump> ();
		_initMoveSpeed = _playerMove.Speed;
		_initJumpForce = _playerJump.JumpForce;
		_activationDist = _wind.MinDist;
	}

	void Update() 
	{
		if (Input.touchCount != 0) 
		{
			TouchInput ();
		} else if (Input.GetMouseButton(1)) {
			MouseInput ();
		} else if (Input.GetMouseButtonUp(1)) {
			DisableTouchArea ();
			CancelInvoke ("ThrowGrain");
			_isFiring = false;
		}
	}

	private void TouchInput() {
		RegisterTouchInfos ();
		if (_firstTouch.phase == TouchPhase.Began && _col.OverlapPoint(_firstTouchPos)) 
		{
			TouchOnPlayer ();
		}
		if (_touchOnPlayer && _touchDist >= _activationDist) 
		{
			EnableWind ();
		}
		if (WindPushArea.activeSelf) 
		{
			RotateToTouch ();
			if (AutoFire) 
			{
				Fire ();
			} 
			else 
			{
				if (Input.touchCount >= 2) 
				{
					Fire ();
				} 
				else 
				{
					CancelInvoke ("ThrowGrain");
					_isFiring = false;
				}
			}
		}
		if (_firstTouch.phase == TouchPhase.Ended) 
		{
			_isFiring = false;
			_touchOnPlayer = false;
			CancelInvoke ("ThrowGrain");
			DisableTouchArea ();
		}
	}

	private void MouseInput() {
		_playerMove.Speed = 0;
		_playerJump.JumpForce = 0;
		_firstTouchPos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		_touchHeading = _firstTouchPos - transform.position;
		_touchDist = _touchHeading.magnitude;
		_wind.TouchDist = _touchDist;
		_wind.RawPushDirection = _touchHeading;
		EnableWind ();
		RotateToTouch ();
		if (AutoFire) {
			Fire ();
		} else {
			if (Input.GetMouseButton(0)) {
				Fire ();
			} else if (Input.GetMouseButtonUp(0)) {
				CancelInvoke ("ThrowGrain");
				_isFiring = false;
			}
		}
	}

	private void Fire() 
	{
		if (!_isFiring) 
		{
			_isFiring = true;
			InvokeRepeating ("ThrowGrain", ThrowStartDelay, ThrowRate);
		}
	}

	private void RegisterTouchInfos() 
	{
		_firstTouch = Input.GetTouch (0);
		_firstTouchPos = Camera.main.ScreenToWorldPoint (_firstTouch.position);
		_touchHeading = _firstTouchPos - transform.position;
		_touchDist = _touchHeading.magnitude;
		_wind.TouchDist = _touchDist;
		_wind.RawPushDirection = _touchHeading;
	}

	private void TouchOnPlayer() 
	{
		_playerMove.Speed = 0;
		_playerJump.JumpForce = 0;
		_touchOnPlayer = true;
	}

	private void EnableWind() 
	{
		_touchOnPlayer = false;
		WindPushArea.SetActive (true);
	}

	private void RotateToTouch() 
	{
		float _rotateDirection = Mathf.Atan2 (_touchHeading.y, _touchHeading.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Euler (0f, 0f, _rotateDirection - 180);
	}

	private void DisableTouchArea() 
	{
		_playerMove.Speed = _initMoveSpeed;
		_playerJump.JumpForce = _initJumpForce;
		WindPushArea.SetActive (false);
	}

	private void ThrowGrain() 
	{
		foreach (Transform anchor in PollenContainer.transform) 
		{
			if (anchor.childCount != 0) 
			{
				PlayerPollen.GrainCount--;
				Transform _grain = anchor.GetChild (0);
				_grain.tag = "Movable";
				_grain.gameObject.layer = 0;
				_grain.gameObject.GetComponent<PollenGrain> ().AttachedToPlayer = false;
				break;
			}
		}
	}
}
