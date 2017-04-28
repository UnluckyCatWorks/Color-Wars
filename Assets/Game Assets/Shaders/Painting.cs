using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Painting : MonoBehaviour
{
	public GameObject projector;

	// Instantiate color splatter
	void OnParticleCollision ( GameObject other )
	{
		var len = ps.GetCollisionEvents ( other, cols );
		for (var i=0; i!=len; i++)
		{
			var tag = cols[i].colliderComponent.tag;
			if ( tag == "Paintable" )
			{
				var p = Instantiate ( projector ).GetComponent<Projector> ();
				p.ignoreLayers = ~(1<<cols[i].colliderComponent.gameObject.layer);
				p.transform.position = cols[i].intersection + cols[i].normal * 3.5f;
				p.transform.LookAt ( cols[i].intersection );
			}
			else
			if ( tag == "Painter" )
			{
				var p = cols[i].colliderComponent.GetComponent<Projector> ();

				if ( p.fieldOfView <= 80f )
				{
					p.fieldOfView += 0.5f;
					p.transform.localScale += new Vector3 ( 0.005f, 0.005f, 0 );
				}
			}
			else
			if ( tag == "Player" )
			{

			}
		}
	}

	private ParticleSystem ps;
	private List<ParticleCollisionEvent> cols;
	void Start ()
	{
		ps = GetComponent<ParticleSystem> ();
		cols = new List<ParticleCollisionEvent> ();
	}
}