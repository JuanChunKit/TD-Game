using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildPhase : MonoBehaviour
{
	public static BuildPhase Instance { get; private set; }

	[SerializeField]
	private GameObject gateTowerDummy;
	[SerializeField]
	private GameObject damageBuffTowerDummy;
	[SerializeField]
	private GameObject duplicatorTowerDummy;

	[SerializeField]
	private GameObject gateTower;
	[SerializeField]
	private GameObject damageBuffTower;
	[SerializeField]
	private GameObject duplicatorTower;

	enum TowerType { None, Duplicator, DamageBuff, Gate }
	private TowerType selectedTT;

	private GameObject emptyGO;
	private Transform buildTransform;

	private bool isBuildPhase;
	/*
	 * We are having a couple of tower type.
	 * The user will be shown a transparent tower to represent where the tower will be placed
	 * The user will be able to rotate the tower on the y axis
	 * The user will be able to move the tower in the x and z axis
	 * When the user confirms the tower position. we'll use the dummy's position to place the real tower
	 * we'll then stop showing the transparent tower
	*/

	/// <summary>
	/// make the objects rotate
	/// exit the build phase and enter combat
	/// fix waves
	/// </summary>

	private int availableDuplicators = 1;
	private int availableDamageBuffs = 3;
	private int availableGates = 2;
	private float rotationSpeed = 90.0f;

	// Use this for initialization
	void Awake()
	{
		Instance = this;

		selectedTT = TowerType.Duplicator;
		ChangeSelectedTowerType( selectedTT );

		emptyGO = new GameObject();
		buildTransform = emptyGO.transform;

		isBuildPhase = false;
	}

	// Update is called once per frame
	void Update()
	{
		if( isBuildPhase )
		{
			// For details on the implementation of the screen to game world input capture,
			// reference GameManager::Update()
			Plane groundPlane = new Plane( Vector3.up, transform.position );
			Ray ray = Camera.main.ScreenPointToRay( Input.mousePosition );
			float hitDist = 0.0f;
			Vector3 hitPoint = new Vector3( 0f, 0f, 0f );

			if( groundPlane.Raycast( ray, out hitDist ) )
			{
				// Get the point along the ray that hits the calculated distance
				hitPoint = ray.GetPoint( hitDist );
				buildTransform.position = hitPoint;
			}

			///////////////////////////////////////////////////////////////////////////////
			// Check for the input of the left and right arrow keys and rotate accordingly
			if( Input.GetKeyDown( KeyCode.UpArrow ) )
			{
				GoToPrevTowerType();
			}
			else if( Input.GetKeyDown( KeyCode.DownArrow ) )
			{
				GoToPrevTowerType();
			}

			if( Input.GetKey( KeyCode.LeftArrow ) )
			{
				buildTransform.Rotate( Vector3.up * -Time.deltaTime * rotationSpeed, Space.Self );
			}
			else if( Input.GetKey( KeyCode.RightArrow ) )
			{
				buildTransform.Rotate( Vector3.up * Time.deltaTime * rotationSpeed, Space.Self );
			}


			RenderTowerType();

			if( Input.GetButtonUp( "Fire1" ) )
			{
				// Instansiate tower
				if( selectedTT != TowerType.None )
				{
					BuildTower();

					switch( selectedTT )
					{
						case TowerType.Duplicator:
						{
							if( availableDuplicators <= 0 )
								GoToNextTowerType();

							break;
						}
						case TowerType.DamageBuff:
						{
							if( availableDamageBuffs <= 0 )
								GoToNextTowerType();

							break;
						}
						case TowerType.Gate:
						{
							if( availableGates <= 0 )
								GoToNextTowerType();

							break;
						}
					}


					// check if we spawned everything
					if( availableDamageBuffs <= 0 && availableDuplicators <= 0 && availableGates <= 0 )
					{
						EndBuildPhase();
						Debug.Log( "All buildings built!" );
						Debug.Log( "Starting Combat phase!" );
						GameManager.Instance.EndBuildingPhase();
					}

				}
			}
		}
	}

	// IDEA ---> A disabled game object is still able to receive OnCollisionEnter events
	//				maybe it could be used to reactivate a game object
	//		
	//			While thinking about it... I think it wasn't OnCollisionEnter, but I'm sure there
	//			Was something that disabled GOs could react or detect
	// maybe refine and post solution here?
	// https://answers.unity.com/questions/372775/sendmessage-to-inactive-object.html
	public void StartBuildPhase()
	{
		Debug.Log( "Started build phase" );
		isBuildPhase = true;
	}

	public void EndBuildPhase()
	{
		Debug.Log( "Ended build phase " );
		ChangeSelectedTowerType( TowerType.None );
		isBuildPhase = false;
	}

	private void ChangeSelectedTowerType( TowerType newTT )
	{
		/*
		 * Show new TT
		 * Hide old TT
		 */

		selectedTT = newTT;

		switch( newTT )
		{
			case TowerType.None:
			{
				duplicatorTowerDummy.SetActive( false );
				damageBuffTowerDummy.SetActive( false );
				gateTowerDummy.SetActive( false );
				break;
			}
			case TowerType.Duplicator:
			{
				duplicatorTowerDummy.SetActive( true );
				damageBuffTowerDummy.SetActive( false );
				gateTowerDummy.SetActive( false );
				break;
			}
			case TowerType.DamageBuff:
			{
				duplicatorTowerDummy.SetActive( false );
				damageBuffTowerDummy.SetActive( true );
				gateTowerDummy.SetActive( false );
				break;
			}
			case TowerType.Gate:
			{
				duplicatorTowerDummy.SetActive( false );
				damageBuffTowerDummy.SetActive( false );
				gateTowerDummy.SetActive( true );
				break;
			}
			default:
			{
				Debug.Log( "Error BuildPhases::ChangeSelectedTowerType Incorrect parameter" );
				break;
			}
		}
	}

	private void RenderTowerType()
	{
		switch( selectedTT )
		{
			case TowerType.None:
			{
				// Don't draw anything
				break;
			}
			case TowerType.Duplicator:
			{
				duplicatorTowerDummy.transform.position = buildTransform.position;
				duplicatorTowerDummy.transform.rotation = buildTransform.rotation;
				break;
			}
			case TowerType.DamageBuff:
			{
				damageBuffTowerDummy.transform.position = buildTransform.position;
				damageBuffTowerDummy.transform.rotation = buildTransform.rotation;
				break;
			}
			case TowerType.Gate:
			{
				gateTowerDummy.transform.position = buildTransform.position;
				gateTowerDummy.transform.rotation = buildTransform.rotation;
				break;
			}
			default:
			{
				Debug.Log( "Error BuildPhases::RenderTowerType selectedTT wrong value" );
				break;
			}
		}
	}

	private void GoToNextTowerType()
	{
		switch( selectedTT )
		{
			case TowerType.None:
			{
				ChangeSelectedTowerType( TowerType.Duplicator );
				break;
			}
			case TowerType.Duplicator:
			{
				ChangeSelectedTowerType( TowerType.DamageBuff );
				break;
			}
			case TowerType.DamageBuff:
			{
				ChangeSelectedTowerType( TowerType.Gate );
				break;
			}
			case TowerType.Gate:
			{
				ChangeSelectedTowerType( TowerType.Duplicator );
				break;
			}
		}

	}

	private void GoToPrevTowerType()
	{
		switch( selectedTT )
		{
			case TowerType.None:
			{
				ChangeSelectedTowerType( TowerType.Duplicator );
				break;
			}
			case TowerType.Duplicator:
			{
				ChangeSelectedTowerType( TowerType.Gate );
				break;
			}
			case TowerType.DamageBuff:
			{
				ChangeSelectedTowerType( TowerType.Duplicator );
				break;
			}
			case TowerType.Gate:
			{
				ChangeSelectedTowerType( TowerType.DamageBuff );
				break;
			}
		}
	}
	/*
	private void SmartNextSelectTower()
	{
		switch( selectedTT )
		{
			case TowerType.None:
			{
				break;
			}
			case TowerType.Duplicator:
			{
				if( availableDuplicators > 0 )
				{
					return;
				}
				else
				{
					GoToNextTowerType();
					SmartNextSelectTower();
				}
				break;
			}
			case TowerType.DamageBuff:
			{
				if( availableDamageBuffs > 0 )
				{
					return;
				}
				else
				{
					GoToNextTowerType();
					SmartNextSelectTower();
				}
				break;
			}
			case TowerType.Gate:
			{
				if( availableGates > 0 )
				{
					return;
				}
				else
				{
					ChangeSelectedTowerType( TowerType.None );
				}
				break;
			}
		}
	}
	*/
	private void BuildTower()
	{
		switch( selectedTT )
		{
			case TowerType.None:
			{
				Debug.Log( "Yo! There's no tower selected, I don't know what to build " );
				break;
			}
			case TowerType.Duplicator:
			{
				if( availableDuplicators > 0 )
				{
					Instantiate( duplicatorTower, buildTransform.position, buildTransform.rotation );
					availableDuplicators -= 1;

					Debug.Log( "Tower Built!" );
					Debug.Log( "Available towers: DuplicatorTower( " + availableDuplicators.ToString() +
								" ),  DamageBuffTower( " + availableDamageBuffs.ToString() +
								" ),  GateTowers( " + availableGates.ToString() + " )" );
				}
				else
				{
					Debug.Log( "You can't build any more Duplicator towers!" );
					Debug.Log( "Available towers: DuplicatorTower( " + availableDuplicators.ToString() +
								" ),  DamageBuffTower( " + availableDamageBuffs.ToString() +
								" ),  GateTowers( " + availableGates.ToString() + " )" );
				}
				break;
			}
			case TowerType.DamageBuff:
			{
				if( availableDamageBuffs > 0 )
				{
					Instantiate( damageBuffTower, buildTransform.position, buildTransform.rotation );
					availableDamageBuffs -= 1;

					Debug.Log( "Tower Built!" );
					Debug.Log( "Available towers: DuplicatorTower( " + availableDuplicators.ToString() +
								" ),  DamageBuffTower( " + availableDamageBuffs.ToString() +
								" ),  GateTowers( " + availableGates.ToString() + " )" );
				}
				else
				{
					Debug.Log( "You can't build any more Damage Buff towers!" );
					Debug.Log( "Available towers: DuplicatorTower( " + availableDuplicators.ToString() +
								" ),  DamageBuffTower( " + availableDamageBuffs.ToString() +
								" ),  GateTowers( " + availableGates.ToString() + " )" );
				}
				break;
			}
			case TowerType.Gate:
			{
				if( availableGates > 0 )
				{
					Instantiate( gateTower, buildTransform.position, buildTransform.rotation );
					availableGates -= 1;

					Debug.Log( "Tower Built!" );
					Debug.Log( "Available towers: DuplicatorTower( " + availableDuplicators.ToString() +
								" ),  DamageBuffTower( " + availableDamageBuffs.ToString() +
								" ),  GateTowers( " + availableGates.ToString() + " )" );
				}
				else
				{
					Debug.Log( "You can't build any more Duplicator towers!" );
					Debug.Log( "Available towers: DuplicatorTower( " + availableDuplicators.ToString() +
								" ),  DamageBuffTower( " + availableDamageBuffs.ToString() +
								" ),  GateTowers( " + availableGates.ToString() + " )" );
				}
				break;
			}
		}
	}
}
