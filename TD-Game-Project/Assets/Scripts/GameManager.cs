using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	/*
	phases for the game:
	1) Place main tower
		* Indicate which way enemies will come
		* Allow the player to place the tower

	2) Purchase or sell towers towers
		* Indicate which way the enemies will come
		* Allow user to sell towers
			* Removes tower from map
			* Frees area for new towers to be constructed
			* Gives player cash back
		* Allow user to buy new towers
			* Area becomes blocked, new towers can't be built in the same spot
			* Takes cash from player

	3) Fight wave
		* Spawn enemies
		* Wave ends when all enemies are eliminated or player loses
	
	4) Go back to (2) for the number of waves the game lasts

	5) Player wins, display stats
		* Display victory message
		* Gives player option the option to play again or quit the game
	
	5.1) Player loses, display stats
		* Display defeat message
		* Gives player option the option to play again or quit the game
	*/

	public GameObject dummyTurret;
	public GameObject turret;

	enum GamePhase { PlaceMainTower, Build, Combat, WinCondition, LoseCondition };
	GamePhase currentPhase;
	bool gamePhaseChanged;

	public static GameManager Instance { get; private set; }

	private void Awake()
	{
		Instance = this;
	}

	// Use this for initialization
	void Start()
	{
		ChangeGamePhase( GamePhase.PlaceMainTower );
	}

	// Update is called once per frame
	void Update()
	{
		// This switch only happens once, when the game phase changes
		if( gamePhaseChanged )
		{
			switch( currentPhase )
			{
			case GamePhase.PlaceMainTower:
			{

				gamePhaseChanged = false;
				break;
			}
			case GamePhase.Build:
			{
				gamePhaseChanged = false;
				break;
			}
			case GamePhase.Combat:
			{
				// WaveManager.Instance.SpawnEnemy( 5 );
				gamePhaseChanged = false;
					44
				SpawnManager.Instance.SpawnFormations( 3 );
				
				break;
			}
			case GamePhase.WinCondition:
			{
				gamePhaseChanged = false;
				break;
			}
			case GamePhase.LoseCondition:
			{
				gamePhaseChanged = false;
				break;
			}
			default:
			{
				Debug.Log( "Error: GameManager.currentPhase set to an incorrect value" );
				gamePhaseChanged = false;
				break;
			}
			}
		}

		// this switch happens every frame
		switch( currentPhase )
		{
		case GamePhase.PlaceMainTower:
		{
			// Generate a plane that intersects the transform's position with an upwards normal.
			Plane groundPlane = new Plane( Vector3.up, transform.position );

			// Generate a ray from the cursor position
			Ray ray = Camera.main.ScreenPointToRay( Input.mousePosition );

			// Determine the point where the cursor ray intersects the plane.
			// Raycasting to a Plane object only gives us a distance, so we'll have to take the distance,
			//   then find the point along that ray that meets that distance.  This will be the point
			//   to spawn the turret
			float hitDist = 0.0f;
			Vector3 hitPoint = new Vector3( 0f, 0f, 0f );

			if( groundPlane.Raycast( ray, out hitDist ) )
			{
				// Get the point along the ray that hits the calculated distance
				hitPoint = ray.GetPoint( hitDist );
				dummyTurret.transform.position = hitPoint;
			}

			if( Input.GetButtonUp( "Fire1" ) )
			{
				// de-activate the dummy turret game object and put the real tower in its position
				dummyTurret.SetActive( false );
				turret.transform.position = hitPoint;
				// turret.SetActive( true );
				ChangeGamePhase( GamePhase.Combat );
			}


			break;
		}
		case GamePhase.Build:
		{
			break;
		}
		case GamePhase.Combat:
		{
			
			break;
		}
		case GamePhase.WinCondition:
		{
			break;
		}
		case GamePhase.LoseCondition:
		{
			break;
		}
		default:
		{
			break;
		}
		}
	}

	private void ChangeGamePhase( GamePhase newPhase )
	{
		currentPhase = newPhase;
		gamePhaseChanged = true;
	}

	void PlaceMainTowerPhase()
	{
		// Tell the user to place the main tower
		// Translate the location of the user selected on the screen to the game world
		// Show the user where the tower will be plased
		// Ask the user to confirm the tower position


	}

	public void WaveEnded()
	{
		print( "WaveEnded" );

		print( "New Wave starting in 5 seconds..." );

		StartCoroutine( TemporaryWaveRestart() );
	}

	IEnumerator TemporaryWaveRestart()
	{
		yield return new WaitForSeconds( 5 );
		ChangeGamePhase( GamePhase.Combat );
	}

	void StartBuildingPhase()
	{

	}

	void EndBuildingPhase()
	{

	}

	void StartWave()
	{

	}

	void EndWave()
	{

	}

	void GameOver()
	{

	}

}
