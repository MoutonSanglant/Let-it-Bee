using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Move : MonoBehaviour
{
	public GameObject PrefabTouch;
	public float Speed = 2;
	Rigidbody2D _rigid;
	Slider _slider;
	bool _isMoving;
	float _sliderDistance = 35f;
	float _startPos;
	Vector2 _minSliderPos;
	Vector2 _maxSliderPos;
	Jump _jump;

	void Awake()
	{
		_isMoving = false;
		_jump = GetComponent<Jump>();
		var instance = Instantiate(PrefabTouch, Vector3.zero, Quaternion.identity) as GameObject;
		_slider = instance.transform.GetChild(2).GetComponent<Slider>(); ;
		_rigid = GetComponent<Rigidbody2D>();
		_minSliderPos = new Vector2(Screen.width / 10, 0);
		_maxSliderPos = new Vector2(Screen.width / 2, Screen.height - (Screen.height / 10));
	}

	public void OnTouchDown()
	{
		if (TouchManager.Instance.TouchIsUsed || _isMoving)
			return;
		_rigid.bodyType = RigidbodyType2D.Dynamic;
		_isMoving = true;
		_slider.gameObject.SetActive(true);
		if (Input.touchCount != 0)
			_startPos = Input.GetTouch(Input.touchCount - 1).position.x;
		else
			_startPos = Input.mousePosition.x;
		StartCoroutine(IsMoving());
	}

	IEnumerator IsMoving()
	{
		var currentTouch = Input.touchCount - 1;
		int touchId = 0;
		if (Input.touchCount != 0)
			touchId = Input.GetTouch(currentTouch).fingerId;
		while (true)
		{
			for (int i = 0; i < Input.touchCount; i++)
			{
				if (Input.GetTouch(i).fingerId == touchId)
				{
					currentTouch = i;
					break;
				}
			}
			Vector2 touchPos;
			if (Input.touchCount != 0)
				touchPos = Input.GetTouch(currentTouch).position;
			else
				touchPos = Input.mousePosition;
			var tmp = new Vector2(touchPos.x, touchPos.y + _sliderDistance);
			tmp.x = Mathf.Clamp(tmp.x, _minSliderPos.x, _maxSliderPos.x);
			tmp.y = Mathf.Clamp(tmp.y, _minSliderPos.y, _maxSliderPos.y);
			_slider.gameObject.transform.position = tmp;
			_slider.value = touchPos.x - _startPos;
			yield return new WaitForFixedUpdate();
			_rigid.velocity = new Vector2(_slider.value * Speed * Time.deltaTime, _rigid.velocity.y);
		}
	}

	public void OnTouchUp()
	{
		if (_jump.IsGrounded())
			_rigid.velocity = Vector2.zero;
		_isMoving = false;
		_slider.gameObject.SetActive(false);
		StopAllCoroutines();
	}
}