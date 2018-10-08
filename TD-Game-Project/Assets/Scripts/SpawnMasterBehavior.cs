using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMasterBehavior : MonoBehaviour {

	public GameObject enemyPrefab;


	// Use this for initialization
	void Start ()
	{
		for( int i = 0; i < 10; i++ )
		{
			SpawnEnemy();
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	void SpawnEnemy()
	{
		Vector3 position;
		float shittyRandomNumber = Random.Range( 0.0f, 4.0f );
		
		if( shittyRandomNumber > 0f && shittyRandomNumber <= 1f )
			position = new Vector3( -5.0f, 0.0f, Random.Range( -5.0f, 5.0f ) );
		else if( shittyRandomNumber > 1f && shittyRandomNumber <= 2f )
			position = new Vector3( Random.Range( -5.0f, 5.0f ), 0.0f, 5.0f );
		else if( shittyRandomNumber > 2f && shittyRandomNumber <= 3f )
			position = new Vector3( 5.0f, 0.0f, Random.Range( -5.0f, 5.0f ) );
		else
			position = new Vector3( Random.Range( -5.0f, 5.0f ), 0.0f, -5.0f );



		Instantiate( enemyPrefab, position, Quaternion.identity );
	}
}
