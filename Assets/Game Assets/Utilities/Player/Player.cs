using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using etc;

public class Player : MonoBehaviour
{
	private Rigidbody body;
	private Animator anim;
	private string type;
	private Player foe;

	public enum PlayerType 
	{
		WASD,
		ARROWS
	}

	[Header ( "References" )]
	public PlayerType playerType;
	public ParticleSystem paint;

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
	bool falling 
	{
		get { return anim.GetBool ( "Falling" ); }
		set { anim.SetBool ( "Falling", value ); }
	}

	void Update ()
	{
		Movement ();
		CheckFall ();
		CheckJump ();
		CheckFire ();
	}

	void CheckFire ()
	{
		if (!Input.GetButtonDown ( type+"Fire" ))
			return;

		anim.SetTrigger ( "Shot" );
		paint.Play ();
	}

	int jumps;
	void CheckJump () 
	{
		if (!Input.GetButtonDown ( type+"Jump" ))
			return;
		//if ( jumps>=2 ) return;

		// Jump
		body.AddForceAtPosition ( transform.up * jumpForce, transform.position, ForceMode.VelocityChange );
		anim.SetTrigger ( "Jump" );
		grounded = false;
		jumps++;
	}

	void Movement () 
	{
		var x = Input.GetAxis ( type+"Horizontal" )  * speed * Time.deltaTime;
		if ( x!=0 )
		{
			// Move player
			moving = true;
			body.position += -Vector3.right * x;
		}
		else moving = false;

		RotationCheck ( (x < 0) ? -1 : 1 );
		FloorCheck ();
	}

	int movDirection;
	void RotationCheck ( float newDir )
	{
		// Going left
		if ( movDirection==-1 )
		{
			// Passing foe
			if ( transform.position.x > foe.transform.position.x )
			{
				transform.rotation *= Quaternion.Euler ( 0, 180, 0 );
				movDirection=1;
			}
		}
		else
		// Going right
		if ( movDirection==1 )
		{
			// Passing foe
			if ( transform.position.x < foe.transform.position.x )
			{
				transform.rotation *= Quaternion.Euler ( 0, 180, 0 );
				movDirection=-1;
			}
		}

		anim.SetFloat ( "RunDirection", newDir==movDirection ? 1 : -1 );
	}

	public LayerMask raycastCollision;
	void FloorCheck () 
	{
		var hit = new RaycastHit ();
		if ( Physics.Raycast ( transform.position, -transform.up.normalized, out hit, 0.2f, raycastCollision ) )
		{
			// Rotate along Z axis to align
			// to surface
			var rot = Quaternion.LookRotation ( transform.forward, hit.normal );
			this.AsyncLerp<Transform> ( "rotation", rot, 0.15f, transform );
			falling = false;
			grounded = true;
			jumps = 0;
		}
		else
		{
			//transform.Translate ( 0, -9.81f * Time.deltaTime, 0 );
			grounded = false;
		}
	}

	void CheckFall () 
	{
		var v = body.velocity;
		if ( v.y > 0 ) falling = true;
	}

	void Awake () 
	{
		body = GetComponent<Rigidbody> ();
		anim = GetComponent<Animator> ();

		// Eru
		if (playerType == PlayerType.WASD)
		{
			type = "WASD_";
			movDirection = 1;
			foe = GameObject.Find ( "Hari" ).GetComponent<Player> ();
		}
		else
		// Hari
		if (playerType == PlayerType.ARROWS)
		{
			type = "ARROWS_";
			movDirection = -1;
			foe = GameObject.Find ( "Eru" ).GetComponent<Player> ();
		}
	}
}
