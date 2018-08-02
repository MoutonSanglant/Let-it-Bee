using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nectar : MonoBehaviour
{
	public GameObject AddNectar;
	public static Nectar Instance
	{
		get
		{
			return _instance;
		}
	}
	private Rigidbody2D _rigid;
	private static Nectar _instance;

	private void Awake()
	{
		if (!_instance)
			_instance = this;
		_rigid = GetComponent<Rigidbody2D>();
	}

	public void RecoltNectar()
	{
		TouchManager.Instance.SetIsUsed(true);
		StartCoroutine(CoRecoltNectar());
	}

	IEnumerator CoRecoltNectar()
	{
		var prev = _rigid.bodyType;
		_rigid.bodyType = RigidbodyType2D.Static;
		Instantiate(AddNectar, transform.position + new Vector3(0, 2f, 0), Quaternion.identity);
		yield return new WaitForSeconds(1f);
		_rigid.bodyType = prev;
		TouchManager.Instance.SetIsUsed(false);
	}

}
