using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{

	public static WaveManager Instance { get; private set; }
	public GameObject enemyPrefab;

	int enemyCount = 0;

	private void Awake()
	{
		Instance = this;
	}

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}

	private void SpawnEnemy()
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

	public void SpawnEnemy( int spawnAmount )
	{
		enemyCount = spawnAmount;

		for( int i = 0; i < spawnAmount; i++ )
		{
			SpawnEnemy();
		}
	}

	public void EnemyDied()
	{
		enemyCount -= 1;

		if( enemyCount == 0 )
		{
			GameManager.Instance.WaveEnded();
		}
	}
}
