using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using etc;

public class Player : MonoBehaviour
{
	private Rigidbody body;
	private Animator anim;
	private string type;

	public enum PlayerType 
	{
		Keyboard,
		Gamepad
	}
	public PlayerType playerType;

	[Header ( "Settings" )]
	public float speed;
	public float jumpForce;

	/// Animator Params
	bool moving
	{
		get { return anim.GetBool ( "Moving" ); }
		set { anim.SetBool ( "Moving", value ); }
	}
	bool grounded 
	{
		get { return anim.GetBool ( "Grounded" ); }
		set { anim.SetBool ( "Grounded", value ); }
	}

	void Update ()
	{
		Movement ( Input.GetAxis ( type+"Horizontal" ) );
		Correct ();

		if ( Input.GetButtonDown ( type+"Jump" ) ) Jump ();
	}

	void Jump ()
	{
		body.AddForceAtPosition ( transform.up * jumpForce, transform.position, ForceMode.Impulse );
	}

	int facingDirection=1;
	void Movement ( float axis ) 
	{
		var x = axis * speed * Time.deltaTime;
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
			this.AsyncLerp<Transform> ( "rotation", rot, 0.15f, transform );
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

		if ( playerType == PlayerType.Gamepad ) type = "G_";
		else
		if ( playerType == PlayerType.Keyboard ) type = "";
	}
}
