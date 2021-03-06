﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionBehavior : MonoBehaviour
{
	public float fullPowerMultiplier	= 1.0f;
	public float mediumPowerMultiplier	= 0.5f;
	public float lowPowerMultiplier     = 0.2f;

	private float fullPowerDistance		= 0.15f;
	private float mediumPowerDistance	= 0.5f;
	private float lowPowerDistance      = 0.7f;

	// The power of the bullet that spawned the explosion
	private float power	= 0.0f;

	// Use this for initialization
	void Start()
	{
		Destroy( gameObject, 0.3f );

	}

	// Update is called once per frame
	void Update()
	{

	}

	private void OnTriggerEnter( Collider other )
	{
		
		if( other.gameObject.tag == "Enemy" )
		{
			EnemyBehavior enemyScript = other.gameObject.GetComponent<EnemyBehavior>();

			// Calculate the distance between the enemy and the center of the explosion
			Transform enemyTransform = other.gameObject.GetComponent<Transform>();

			// Find out the distance between the enemy and the center of the explosion
			float distanceToCenter = (enemyTransform.position - transform.position).magnitude;

			// print( distanceToCenter );

			float damageDealt;

			if( distanceToCenter <= fullPowerDistance )	// max damage
			{
				damageDealt = power * fullPowerMultiplier; 
			}
			else if( distanceToCenter < mediumPowerDistance )	//medium damage
			{
				damageDealt = power * mediumPowerMultiplier;
			}
			else // low damage
			{
				damageDealt = power * lowPowerMultiplier;
			}

			enemyScript.TakeDamage( damageDealt );
			// print( damageDealt );

		}

	}

	public void SetExplosionPower( float bulletPower )
	{
		power = bulletPower;
	}

	public void MakeBigExplosion()
	{
		fullPowerDistance   *= 2f;
		mediumPowerDistance	*= 2f;
		lowPowerDistance    *= 2f;
	}
}
