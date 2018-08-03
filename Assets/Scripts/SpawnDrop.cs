using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDrop : MonoBehaviour
{

	public Transform Drop;
	public Transform RespawnPoint;
	private float _timer = 0f;

	public float rate = 1.0f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		_timer += Time.deltaTime;
		if (_timer > rate)
		{
			Instantiate(Drop);
			Drop.position = this.transform.position;
			Drop.GetComponent<Destroy>().respawnPoint = RespawnPoint;	
			_timer -= rate;
		}
	}
}
