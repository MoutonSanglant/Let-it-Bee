using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BudButton : MonoBehaviour
{
	public bool PlayerInside
	{
		get
		{
			return _playerInside;
		}
	}
	public Color ColorIn;
	public Color ColorOut;
	private SpriteRenderer _renderer;
	bool _playerInside;
	Bud _parent;

	private void Awake()
	{
		_parent = GetComponentInParent<Bud>();
		_renderer = GetComponent<SpriteRenderer>();
		_playerInside = false;
		_renderer.color = ColorOut;
	}

	void PlayerComesIn()
	{
		_playerInside = true;
		if (!_parent.IsOpen)
			_renderer.color = ColorIn;
	}

	void PlayerLeaves()
	{
		_playerInside = false;
		if (_renderer.color == ColorIn)
			DisablePopUp();
	}

	public void DisablePopUp()
	{
		_renderer.color = ColorOut;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "Player")
			PlayerComesIn();
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.tag == "Player")
			PlayerLeaves();
	}

	private void OnMouseDown()
	{
		print("salut");
		if (!TouchManager.Instance.TouchIsUsed && !_parent.IsOpen && PlayerInside)
		{
			_parent.EnablePlatforms();
			DisablePopUp();
		}
	}
}
