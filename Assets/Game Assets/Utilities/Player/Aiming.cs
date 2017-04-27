using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aiming : MonoBehaviour
{
	private Player player;

	[Header ( "References" )]
	public Transform body;
	public Transform weaponArm;
	public Transform altArm;

	[Header ( "Constraints" )]
	public float maxBody;
	public float minBody;
	public float maxArms;
	public float minArms;

	void LateUpdate ()
	{
		if ( player.playerType == Player.PlayerType.Keyboard )
		{
			/// Rotate body
			var rot = -Input.GetAxis ( "Mouse Y" ) * 5f;
			var alt = -Input.GetAxis ( "Mouse X" ) * 2f;
			body.localRotation *= Quaternion.Euler ( 0, 0, rot + alt );
		}
		else
		if ( player.playerType == Player.PlayerType.Gamepad )
		{

		}
	}

	void Awake () 
	{
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
		player = GetComponent<Player> ();
	}
}
