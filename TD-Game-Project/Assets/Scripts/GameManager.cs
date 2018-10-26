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

	enum GamePhase { PlaceMainTower, Build, Combat, WinCondition, LoseCondition };
	GamePhase currentPhase;
	bool gamePhaseChanged;

	// Use this for initialization
	void Start()
	{
		currentPhase = GamePhase.PlaceMainTower;
		gamePhaseChanged = true;
	}

	// Update is called once per frame
	void Update()
	{
		if( gamePhaseChanged )
		{
			switch( currentPhase )
			{
			case GamePhase.PlaceMainTower:
			{
				break;
			}
			case GamePhase.Build:
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
				Debug.Log( "Error: GameManager.currentPhase set to an incorrect value" );
				break;
			}
			}
		}
	}

	void PlaceMainTowerPhase()
	{
		// Tell the user to place the main tower
		// Translate the location of the user selected on the screen to the game world
		// Show the user where the tower will be plased
		// Ask the user to confirm the tower position


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
