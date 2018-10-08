using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{

	public float maxAliveTime = 4.0f;
	public float bulletSpeed = 1.0f;
	public bool printInfo = false;

	// the position of the object in the y axis should never change
	private float yPos;

	private Vector3 direction;


	// Use this for initialization
	void Start()
	{
		direction = transform.forward;
		yPos = transform.position.y;

		// destroy the bullet after x amount of time to prevent it from leaking memory
		Destroy( gameObject, maxAliveTime );
	}

	// Update is called once per frame
	void Update()
	{
		// Move the object forward along its z axis 
		transform.Translate( transform.forward * bulletSpeed * Time.deltaTime, Space.World );

		// transform.Translate( transform.forward * bulletSpeed * Time.deltaTime, Space.World );


		// Draw debug info
		float rayDrawDistance = 1.0f;
		Debug.DrawRay( transform.position, rayDrawDistance * transform.forward, Color.blue, 0, false );   // z axis
		Debug.DrawRay( transform.position, rayDrawDistance * transform.up,		Color.green, 0, false );  // y axis
		Debug.DrawRay( transform.position, rayDrawDistance * transform.right,	Color.red, 0, false );    // x axis
		Debug.DrawRay( transform.position, rayDrawDistance * direction, Color.magenta, 0, false ); 
	}

	private void OnCollisionEnter( Collision collision )
	{
		// Get the normal of the object we collided with
		Vector3 collisionNormal = new Vector3( 0.0f, collision.contacts[0].normal.y, 0.0f );

		// Calculate the reflection direction of the bullet ( we only care about the rotation on y since we are simulating 2d rotation)
		direction = Vector3.Reflect( transform.forward, collision.contacts[0].normal );

		// Figure out the angle between the reflection direction and the bullet's forward vector
		// float angle = Vector3.Angle( direction, transform.forward );
		float angle = Vector3.SignedAngle( direction, transform.forward, Vector3.up );
		angle *= -1f;

		Debug.Log( "Angle of rotation: " + angle );
		Debug.Log( "yAxisRot before rotation: " + transform.rotation.y );

		// Rotate the bullet
		transform.Rotate( Vector3.up, angle );
		Debug.Log( "yAxisRot after rotation: " + transform.rotation.y );

		// this.transform.rotation = Quaternion.AngleAxis( angle, Vector3.up );


		/*
		// Determine the target rotation.  This is the rotation if the transform looks at the target point.
		direction = Vector3.Reflect( transform.forward, collision.contacts[0].normal ) - transform.position;
		Quaternion targetRotation = Quaternion.LookRotation( Vector3.Reflect( transform.forward, collision.contacts[0].normal ) - transform.position );

		// Smoothly rotate towards the target point.
		transform.rotation = Quaternion.Slerp( transform.rotation, targetRotation, 1.0f );

		/*
		// only use the first point of contact to determine the reflection vector
		Vector3 newDir = direction = Vector3.Reflect( transform.forward, collision.contacts[0].normal );

		float angle = Vector3.Angle( transform.forward, newDir );
		transform.Rotate( 0.0f, angle, 0.0f, Space.World );
		*/

		/*
		foreach( ContactPoint contact in collision.contacts )
		{
			print( contact.thisCollider.name + " hit " + contact.otherCollider.name );
			// Visualize the contact point
			Debug.DrawRay( contact.point, contact.normal, Color.magenta, 120, false );

		}
		*/
	}

	void Ricochet()
	{

	}
}
