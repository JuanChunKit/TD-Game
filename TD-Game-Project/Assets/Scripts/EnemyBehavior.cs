using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{

	// public Transform target;

	public float speed = 1.0f;
	// We want the enemy to always be at the same y position
	public float groundOffset = 0.3f;
	public float enemyOffset = 1.0f;

	private Vector3 targetLocation;
	public float health;

	// Use this for initialization
	void Start()
	{
		// Make it get the location of the target via script
		targetLocation = GameObject.FindWithTag( "MainTower" ).transform.position;

		transform.position = new Vector3( transform.position.x, groundOffset, transform.position.z );
		targetLocation.y = groundOffset;

		health = 100.0f;

		// (task) Rotate the enemy to face the target
	}

	// Update is called once per frame
	void Update()
	{
		// Check if the enemy is within x distance of the target
		// If the enemy has reached that distance, stop moving
		if( (transform.position - targetLocation).magnitude > enemyOffset )
		{
			transform.position = Vector3.MoveTowards( transform.position, targetLocation, speed * Time.deltaTime );
		}
		else
		{
			// You are next to the main tower, attack it!

		}
	}

	private void OnCollisionEnter( Collision collision )
	{
		
	}

	public void TakeDamage( float amount )
	{
		health -= amount;

		if( health <= 0.0f )
		{
			// the enemy has taken fatal damage, kill him
			WaveManager.Instance.EnemyDied();
			Destroy( gameObject );
		}
	}
}
