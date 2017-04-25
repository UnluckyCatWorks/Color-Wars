using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Painting : MonoBehaviour
{
	public GameObject projector;
	public byte max;
	private byte count;

	// Instantiate color splatter
	void OnParticleCollision ( GameObject other )
	{
		// Limit collision
		if ( count >= max ) return;

		var len = ps.GetCollisionEvents ( other, cols );
		for (var i=0; i!=len; i++)
		{
			var p = Instantiate( projector );
			p.transform.position = cols[i].intersection + cols[i].normal;
			p.transform.LookAt ( cols[i].intersection );
			count++;
		}
	}

	public ParticleSystem ps;
	private List<ParticleCollisionEvent> cols;
	void Start ()
	{
		ps = GetComponent<ParticleSystem> ();
		cols = new List<ParticleCollisionEvent> ();
	}
}