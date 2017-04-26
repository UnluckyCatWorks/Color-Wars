using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	private Animator anim;

	public enum PlayerType 
	{
		Keyboard,
		Gamepad
	}
	public PlayerType playerType;

	[Header ( "Settings" )]
	public float speed;

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
			transform.Translate ( -transform.right * x );
		}
	}

	void Correct ()
	{
		var hit = new RaycastHit ();
		if ( Physics.Raycast ( transform.position + transform.up * 0.1f, -transform.up, out hit, 0.15f ) )
		{
			// Rotate along surface normal
			var rot = Quaternion.LookRotation ( hit.normal, Vector3.forward );
			transform.rotation = rot * Quaternion.Euler ( 90, 0, 0 ) * Quaternion.Euler ( 0, 180, 0 );
		}
		else
		{
			// If not colliding, means in-air
			// Apply gravity
			transform.Translate ( 0, -9.81f * Time.deltaTime, 0 );
		}
	}

	void Awake () 
	{
		anim = GetComponent<Animator> ();
	}
}
