using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using etc;

public class Player : MonoBehaviour
{
	private Rigidbody body;
	private Animator anim;

	public enum PlayerType 
	{
		Keyboard,
		Gamepad
	}
	public PlayerType playerType;

	[Header ( "Settings" )]
	public ParticleSystem paint;
	public float speed;

	/// Animator Params
	bool grounded 
	{
		get { return anim.GetBool ( "Grounded" ); }
		set { anim.SetBool ( "Grounded", true ); }
	}

	void Update ()
	{
		Movement ();
		Correct ();

		if ( playerType == PlayerType.Gamepad )
		{

		}
		else
		if ( playerType == PlayerType.Keyboard )
		{
			if (Input.GetMouseButtonDown ( 0 ) )
			{
				paint.Play ();
			}
		}
	}

	int facingDirection=1;
	void Movement () 
	{
		var x = Input.GetAxis ( "Horizontal" ) * speed * Time.deltaTime;
		if ( x!=0 )
		{
			var newDir = (x < 0) ? -1 : 1;
			// Correct orientation
			if ( newDir != facingDirection )
			{
				transform.rotation *= Quaternion.Euler ( 0, 180, 0 );
				facingDirection = newDir;
			}
			// Move player
			body.position += -Vector3.right * x;
		}
	}

	public LayerMask raycastCollision;
	void Correct () 
	{
		var hit = new RaycastHit ();
		if ( Physics.Raycast ( transform.position, -transform.up.normalized, out hit, 0.2f, raycastCollision ) )
		{
			// Rotate along Z axis to align
			// to surface
			var rot = Quaternion.LookRotation ( transform.forward, hit.normal );
			body.rotation = rot;
			//body.position = hit.point + transform.up * 0.1f;
		}
		else
		{
			//transform.Translate ( 0, -9.81f * Time.deltaTime, 0 );
			grounded = false;
		}
	}

	void Awake () 
	{
		body = GetComponent<Rigidbody> ();
		anim = GetComponent<Animator> ();
	}
}
