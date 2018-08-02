using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PollenColor{None, Yellow, Green, Cyan, Magenta};

public class PlayerPollen : MonoBehaviour 
{

	public GameObject PollenContainer;
	[HideInInspector]
	public PollenColor PlayerPollenColor;
	[HideInInspector]
	public  static int GrainCount;

	private Transform _availableAnchor;

	void Update() {
		Debug.Log (GrainCount);
	}
	private void OnTriggerEnter2D (Collider2D collider)
	{
		PollenGrain _grain = collider.gameObject.GetComponent<PollenGrain> ();
		if (_grain) 
		{
			if (!_grain.AttachedToPlayer && _grain.OnFlower) 
			{
				if (_grain.GrainColor != PlayerPollenColor) {
					DetachAllGrain ();
				}
				if (GrainCount < PollenContainer.transform.childCount) 
				{
					AttachGrainToPlayer (_grain);
				}
			}
		}
	}

	private void AttachGrainToPlayer(PollenGrain grain) 
	{
		GrainCount++;
		SeekAvailableAnchor ();
		PlayerPollenColor = grain.GrainColor;
		grain.transform.parent = _availableAnchor;
		grain.AttachedToPlayer = true;
		grain.MovingTowardPlayer = true;
	}

	private void SeekAvailableAnchor() 
	{
		foreach(Transform child in PollenContainer.transform) 
		{
			if (child.childCount == 0) 
			{
				_availableAnchor = child;
			}
		}
	}

	public void DestroyOneGrain() 
	{
		GrainCount--;
		foreach (Transform child in PollenContainer.transform) 
		{
			if (child.childCount != 0) 
			{
				Destroy (child.GetChild (0).gameObject);
				break;
			}
		}
	}

	private void DetachAllGrain() 
	{
		GrainCount = 0;
		foreach(Transform child in PollenContainer.transform) 
		{
			if (child.childCount != 0) 
			{
				PollenGrain _grain = child.GetComponentInChildren<PollenGrain> ();
				_grain.AttachedToPlayer = false;
				_grain.tag = "Movable";
				_grain.Fading = true;
			}
		}
		PlayerPollenColor = PollenColor.None;
	}
}
