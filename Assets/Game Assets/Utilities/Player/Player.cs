using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	private CharacterController player;

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
		// THESE ARE ALL ONLY TESTS
		if ( playerType == PlayerType.Gamepad )
			throw new System.NotImplementedException ();

		Movement ();
		RotateCam ();
	}

	void Movement () 
	{
		// First person movement - tests
		var mov = Vector3.zero;
		if ( Input.GetKey ( KeyCode.W ) ) mov += transform.forward;
		if ( Input.GetKey ( KeyCode.S ) ) mov -= transform.forward;
		if ( Input.GetKey ( KeyCode.A ) ) mov -= transform.right;
		if ( Input.GetKey ( KeyCode.D ) ) mov += transform.right;
		mov = mov.normalized * speed;

		player.SimpleMove ( mov );
	}
	void RotateCam ()
	{
		var cam = Camera.main.transform;
		cam.Rotate ( cam.right, Input.GetAxis ( "Mouse Y" ) * 270 * Time.deltaTime );
		transform.Rotate ( cam.up, Input.GetAxis ( "Mouse X" ) * 120 * Time.deltaTime );
		// fml
		var euler = transform.localEulerAngles;
		var euler2 = cam.transform.localEulerAngles;
		transform.localEulerAngles = new Vector3 ( 0, euler.y, 0 );
		cam.transform.localEulerAngles = new Vector3 ( euler2.x, 0, 0 );
	}

	void Awake ()
	{
		player = GetComponent<CharacterController> ();
	}
}
