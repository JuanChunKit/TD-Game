using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
	public static SpawnManager Instance { get; private set; }

	public bool[]       formationPositionOccupied;
	public Transform[]  formationPositions;

	public Transform[]	spawnPoints;
	public bool[]       spawnPointsOccupied;


	private int enemiesPerFormation = 4;

	private int enemiesAlive = 0;

	public GameObject enemyPrefab;


	private void Awake()
	{
		Instance = this;
	}

	// Use this for initialization
	void Start()
	{
		formationPositionOccupied = new bool[ formationPositions.Length ];
		FreePositions();

		spawnPointsOccupied = new bool[ spawnPoints.Length ];
		FreeSpawnPoints();

		// SpawnFormation();
	}

	// Update is called once per frame
	void Update()
	{

	}

	void SpawnFormation()
	{
		int index, tries = 0;
		Vector3 spawnPosition;

		int curSP = Random.Range( 0, spawnPoints.Length );

		for( int i = 0; i < enemiesPerFormation; )
		{
			index = Random.Range( 0, formationPositions.Length );

			if( formationPositionOccupied[ index ] == false )
			{
				formationPositionOccupied[ index ] = true;
				i++;

				// spawnTransform = formationPositions[index];

				//spawnTransform = spawnPoint;
				//spawnTransform.Translate( formationPositions[index].position, Space.Self );
				//Instantiate( enemyPrefab, spawnTransform.position, spawnTransform.rotation );


				spawnPosition = spawnPoints[ curSP ].position;
				spawnPosition += formationPositions[ index ].position;

				Instantiate( enemyPrefab, spawnPosition, spawnPoints[ curSP ].rotation );
			}

			tries++;
			if( tries > 500 )
			{
				Debug.LogError( "Failed to spawn all enemies in the formation" );
				break;
			}
		}

		FreePositions();
		FreeSpawnPoints();
	}

	public void SpawnFormations( int formationsToSpawn )
	{
		int index, tries = 0;
		Vector3 spawnPosition;

		int curSP;

		for( int i = 0; i < formationsToSpawn; i++ )
		{
			// Find a spawn point that's free
			while( true )
			{
				curSP = Random.Range( 0, spawnPoints.Length );

				if( spawnPointsOccupied[ curSP ] == false )
				{
					spawnPointsOccupied[ curSP ] = true;
					break;
				}
			}
			
			for( int j = 0; j < enemiesPerFormation; )
			{
				index = Random.Range( 0, formationPositions.Length );

				if( formationPositionOccupied[ index ] == false )
				{
					j++;
					formationPositionOccupied[ index ] = true;

					spawnPosition = spawnPoints[ curSP ].position;
					spawnPosition += formationPositions[ index ].position;

					Instantiate( enemyPrefab, spawnPosition, spawnPoints[ curSP ].rotation );
					enemiesAlive += 1;
				}

				// Make sure you don't get trap in the loop
				tries++;
				if( tries > 500 )
				{
					Debug.LogError( "Failed to spawn all enemies in the formation" );
					break;
				}
			}

			FreePositions();
			tries = 0;
		}
		

		FreePositions();
		FreeSpawnPoints();
	}

	private void FreePositions()
	{
		for( int i = 0; i < formationPositions.Length; i++ )
		{
			formationPositionOccupied[ i ] = false;
		}
	}

	private void FreeSpawnPoints()
	{
		for( int i = 0; i < spawnPoints.Length; i++ )
		{
			spawnPointsOccupied[ i ] = false;
		}
	}

	public void EnemyDied()
	{
		enemiesAlive -= 1;

		if( enemiesAlive == 0 )
		{
			GameManager.Instance.WaveEnded();
		}
	}
}
