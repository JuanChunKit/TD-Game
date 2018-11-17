using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuplicatorTower : Tower
{

	public GameObject bulletPrefab;

	public float towerRadius = 0.5f;

	protected override void Awake()
	{
		base.Awake();
	}

	// Update is called once per frame
	protected override void Update()
	{
		base.Update();

	}

	public void DuplicateBullet( Transform otherBulletTransform, float otherBulletDamage )
	{
		// make a new position with the x and z of the center of the tower, but the y position of the bullet
		Vector3 centerOfTower = new Vector3( transform.position.x, otherBulletTransform.position.y, transform.position.z );

		Vector3 spawnDirection = centerOfTower - otherBulletTransform.position;
		spawnDirection.Normalize();

		Vector3 spawnPosition = centerOfTower + (spawnDirection * towerRadius);

		// Rotation option 1
		// BulletBehavior newBulletBehavior = Instantiate( 
		//	bulletPrefab, spawnPosition, otherBulletTransform.rotation ).GetComponent<BulletBehavior>();
		////////////////////////////////////////////////////

		// Rotation option 2
		BulletBehavior newBulletBehavior = Instantiate(
			bulletPrefab, spawnPosition, Quaternion.identity ).GetComponent<BulletBehavior>();
		newBulletBehavior.faceTowards( spawnDirection );
		///////////////////////////////////////////////////

		newBulletBehavior.bulletDamage = otherBulletDamage;
		newBulletBehavior.isClone = true;
	}
}
