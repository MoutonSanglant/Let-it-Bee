using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PollenSpawner : MonoBehaviour 
{

	public float SpawnDelay;
	public GameObject PollenGrain;
	public PollenColor PollenColor;
	public bool IllimitedSpawn;

	void Start() 
	{
		SpawnAllGrain ();
	}

	void Update() 
	{
		if (IllimitedSpawn) { CheckMissingGrain (); }
	}

	private void CheckMissingGrain() 
	{
		foreach (Transform child in transform) 
		{
			if (child.childCount == 0) { StartCoroutine (SpawnGrainWithDelay (child)); }
		}
	}

	private void SpawnGrain(Transform grainParent) 
	{
		GameObject _newGrain = Instantiate (PollenGrain, grainParent.position, Quaternion.identity, grainParent);
		_newGrain.transform.GetChild(0).gameObject.layer = 12;
		PollenGrain _grain = _newGrain.GetComponent<PollenGrain> ();
		_grain.GrainColor = PollenColor;
	}

	private void SpawnAllGrain() 
	{
		foreach(Transform child in transform)  
		{
			SpawnGrain (child.transform);
		}
	}

	private IEnumerator SpawnGrainWithDelay(Transform anchor) 
	{
		SpawnGrain (anchor);
		anchor.gameObject.SetActive (false);
		yield return new WaitForSeconds (SpawnDelay);
		anchor.gameObject.SetActive (true);
		yield return null;
	}
}
