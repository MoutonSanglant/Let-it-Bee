using UnityEngine;
using System.Collections;

public class Bud : MonoBehaviour
{
	public GameObject Plateformes, PollenTrigger;
	public PollenSpawner PollenSpawner;
	public PollenColor PollenColor;
	public bool SpawnPollen;
	public bool IllimitedSpawn;
	public bool IsOpen
	{
		get
		{
			return _isOpen;
		}
	}
	bool _isOpen;

	void Awake() 
	{
		PollenSpawner.PollenColor = PollenColor;
		PollenSpawner.IllimitedSpawn = IllimitedSpawn;
	}

	public void EnablePlatforms()
	{
		TouchManager.Instance.SetIsUsed(true);
		_isOpen = true;
		PollenTrigger.SetActive (false);
		Plateformes.SetActive(true);
		if (SpawnPollen) { PollenSpawner.gameObject.SetActive (true); }
		StartCoroutine(DisableCanTouch());
	}

	IEnumerator DisableCanTouch()
	{
		yield return new WaitForSeconds(0.1f);
		TouchManager.Instance.SetIsUsed(false);
	}
}
