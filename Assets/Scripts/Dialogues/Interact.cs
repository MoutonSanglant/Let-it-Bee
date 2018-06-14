using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{

	public GameObject exclamationSprite;
	private GameObject go;
	private DialogueTrigger _dial;
	
	private int _layerMask;
	public Sprite SpritePortraitLeft;
	public Sprite SpritePortraitRight;
	public DialogPortrait PortraitLeft;
	public DialogPortrait PortraitRight;
	public bool _isClose;
	
	
	// Use this for initialization
	private void Start()
	{
		_layerMask = LayerMask.NameToLayer("NPC");
        _dial = GetComponent<DialogueTrigger>();
	}

	// Update is called once per frame
	private void Update()
	{	
		if (false == _isClose)
		{
			return;
		}
		if (exclamationSprite.active)
		{
			if (Touch.TouchCount() > 0) // && Input.GetTouch(0).phase == TouchPhase.Began)
			{
				print(Touch.TouchCount());
				Debug.Log("TOUCHING");
				
				var hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Touch.GetPos()), Vector2.zero, _layerMask); 
				if (hit)
				{
					var interact = hit.collider.GetComponent<Interact>() ?? hit.collider.GetComponentInParent<Interact>();

					if (interact._isClose)
					{
						exclamationSprite.SetActive(false);
						_dial.TriggerDialogue();
						PortraitLeft.SetImage(SpritePortraitLeft);
						PortraitRight.SetImage(SpritePortraitRight);
						Debug.Log("Touched " + hit.collider.name);	
						
					}
				}
			}
		}
	}

	public void ReloadSpriteOnEndDialog()
	{
		exclamationSprite.SetActive(true);
	}
	
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			exclamationSprite.SetActive(true);
			_isClose = true;
		}
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			exclamationSprite.SetActive(false);
			_isClose = false;
		}
	}
}