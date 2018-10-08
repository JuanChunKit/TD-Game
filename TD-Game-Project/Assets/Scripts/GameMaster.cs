using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour {

	public int currentScore { get; set; }
	public int playerHealth { get; private set; }

	public static GameMaster Instance { get; private set; }

	private void Awake()
	{
		Instance = this;
		ResetRound();
	}

	void ResetRound()
	{
		playerHealth = 100;
		currentScore = 0;
	}

	public void DamagePlayer( int damageAmount )
	{
		playerHealth -= damageAmount;
		if( playerHealth <= 0 )
		{
			playerHealth = 0;
			
			// add some game over things

		}
	}

}
