using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aiming : MonoBehaviour
{
	private Player foe;

	[Header ( "References" )]
	public Transform body;
	public Transform weaponArm;
	public Transform altArm;

	[Header ( "Constraints" )]
	public float maxBody;
	public float maxArms;

	void LateUpdate ()
	{
		body.localRotation = AimBody ();
	}

	Quaternion AimBody () 
	{
		var invert = transform.position.x > foe.transform.position.x;
		var q = Quaternion.FromToRotation
		(
			Vector3.right * ( invert ? -1 : 1 ),
			foe.transform.position - transform.position
		);

		if ( transform.eulerAngles.y >= 179 )
			q = Quaternion.Inverse ( q );

		return q;
	}

	void Awake () 
	{
		// Find foe
		var player = GetComponent<Player> ();
		foe = FindObjectsOfType<Player> ().First ( x => x.playerType != player.playerType );
	}
}
