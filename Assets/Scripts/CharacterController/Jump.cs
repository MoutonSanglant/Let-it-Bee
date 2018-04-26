using System.Collections;
using UnityEngine;

public class Jump : MonoBehaviour
{
	public Transform GroundCheck;
	public LayerMask Ground;
	public float JumpForce = 2f;
	Rigidbody2D _rigid;

	void Awake () 
    {
		_rigid = GetComponent<Rigidbody2D>();
	}

	bool IsGrounded()
	{
		return (Physics2D.OverlapCircle(GroundCheck.position, 0.1f, Ground));
	}

	public void OnTouchDown()
	{
		if (IsGrounded() && !TouchManager.Instance.TouchIsUsed)
			StartCoroutine(IsJumping());
	}

	IEnumerator IsJumping()
	{
		yield return new WaitForFixedUpdate();
		_rigid.velocity = new Vector2(_rigid.velocity.x, 0);
		_rigid.AddRelativeForce(new Vector2(0, JumpForce), ForceMode2D.Impulse);
	}
}
