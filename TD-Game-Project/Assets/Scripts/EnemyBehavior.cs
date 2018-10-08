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

	// Use this for initialization
	void Start()
	{
		// Make it get the location of the target via script
		targetLocation = GameObject.FindWithTag( "MainTower" ).transform.position;

		transform.position = new Vector3( transform.position.x, groundOffset, transform.position.z );
		targetLocation.y = groundOffset;

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
		if( collision.gameObject.tag == "Bullet" )
		{
			GameMaster.Instance.currentScore += 10;
			print( GameMaster.Instance.currentScore );
			Destroy( gameObject );
		}
	}
}
