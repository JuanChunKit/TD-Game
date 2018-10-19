using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuplicatorTower : MonoBehaviour {

	public GameObject bulletPrefab;

	public Transform spawnPosition0;
	public Transform spawnPosition1;
	public Transform spawnPosition2;

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
}
