using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuplicatorTower : MonoBehaviour {

	public GameObject bulletPrefab;

	public Transform spawnPosition0;
	public Transform spawnPosition1;
	public Transform spawnPosition2;

	public float towerRadius = 0.5f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SpawnBullets()
	{
		Instantiate( bulletPrefab, spawnPosition0.position, spawnPosition0.rotation ).GetComponent<BulletBehavior>().isClone = true ;
		Instantiate( bulletPrefab, spawnPosition1.position, spawnPosition1.rotation ).GetComponent<BulletBehavior>().isClone = true;
		Instantiate( bulletPrefab, spawnPosition2.position, spawnPosition2.rotation ).GetComponent<BulletBehavior>().isClone = true;

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
