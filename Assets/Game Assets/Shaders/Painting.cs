using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Painting : MonoBehaviour
{
	public GameObject projector;
	public Texture2D[] splatters;

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
				var c = cols[i].colliderComponent.transform;
				var p = c.parent.GetComponent<Projector> ();

				if (p.fieldOfView <= 105f)
				{
					p.fieldOfView += 0.5f;
					var cRot = c.rotation;
					p.transform.rotation *= Quaternion.Euler ( -0.3f, 0, 0 );
					c.rotation = cRot;
				}

				if (p.fieldOfView <= 80f)
					c.transform.localScale += new Vector3 ( 0.005f, 0.005f, 0 );
			}
			else
			if ( tag == "Player" )
			{
				var player = cols[i].colliderComponent.transform.parent.GetComponent<Player> ();
				player.hp += 0.1f;
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