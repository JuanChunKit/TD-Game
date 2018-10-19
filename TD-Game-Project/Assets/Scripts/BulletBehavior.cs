﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
	public float bulletDamage;

	public float maxAliveTime = 4.0f;
	public float bulletSpeed = 1.0f;
	public bool printInfo = false;

	// the position of the object in the y axis should never change
	private float yPos;

	private Vector3 direction;

	private bool isBomb = false;
	public Material bombMaterial;

	public GameObject explosionPrefab;
	public bool isClone = false;

	// Use this for initialization
	void Start()
	{
		direction = transform.forward;

		// Future task!!!
		// Check that the bullet is always staying in it's starting y position
		// The bullet should only move in the x and z planes
		yPos = transform.position.y;

		// destroy the bullet after x amount of time to prevent it from leaking memory
		Destroy( gameObject, maxAliveTime );

	}

	// Update is called once per frame
	void Update()
	{
		// Move the object forward along its z axis 
		transform.Translate( transform.forward * bulletSpeed * Time.deltaTime, Space.World );

		// Draw debug info
		
		float rayDrawDistance = 1.0f;
		Debug.DrawRay( transform.position, rayDrawDistance * transform.forward, Color.blue, 0, false );   // z axis
		Debug.DrawRay( transform.position, rayDrawDistance * transform.up,		Color.green, 0, false );  // y axis
		Debug.DrawRay( transform.position, rayDrawDistance * transform.right,	Color.red, 0, false );    // x axis
		Debug.DrawRay( transform.position, rayDrawDistance * direction,			Color.magenta, 0, false ); 
		
	}

	private void OnCollisionEnter( Collision collision )
	{
		if( collision.gameObject.tag == "Enemy" )
		{
			// deal normal bullet damage to the enemy
			EnemyBehavior enemyScript = collision.gameObject.GetComponent<EnemyBehavior>();
			enemyScript.TakeDamage( bulletDamage );

			// Trigger explosion that damages enemies around the bomb
			if( isBomb )
			{
				// spawn the explosion prefab and delete the bullet
				Instantiate( explosionPrefab, transform.position, transform.rotation );
				Destroy( gameObject );
			}
		}

		if( collision.gameObject.tag == "DuplicatorTower" )
		{
			if( isClone == false )
				collision.gameObject.GetComponent<DuplicatorTower>().SpawnBullets();
		}

		// Get the normal of the object we collided with
		// Calculate the reflection direction of the bullet
		direction = Vector3.Reflect( transform.forward, collision.contacts[0].normal );

		// Figure out the angle between the reflection direction and the bullet's forward vector
		float angle = Vector3.SignedAngle( direction, transform.forward, Vector3.up );
		angle *= -1f;

		// Rotate the bullet
		transform.Rotate( Vector3.up, angle );
	}

	void Ricochet()
	{

	}

	public void MakeBomb()
	{
		isBomb = true;

		// Change the appearance of the bullet
		GetComponent<Renderer>().material.CopyPropertiesFromMaterial( bombMaterial );


	}
}
