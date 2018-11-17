using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{

	// public Transform target;

	public float speed = 1.0f;
	// We want the enemy to always be at the same y position
	public float groundOffset = 0.3f;
	public float enemyOffset = 1.0f;

	private Vector3 targetLocation;
	public float health = 100.0f;

	[SerializeField]
	private float attackCooldown = 3.0f;
	private float attackCooldownTimer;
	private bool isAttacking = false;

	private bool stopMoving = false;

	private Tower towerToAttack = null;

	private List<GameObject> obstacles;

	[SerializeField]
	private float attackPower = 5.0f;

	// Use this for initialization
	void Start()
	{
		// Make it get the location of the target via script
		targetLocation = GameObject.FindWithTag( "MainTower" ).transform.position;

		transform.position = new Vector3( transform.position.x, groundOffset, transform.position.z );
		targetLocation.y = groundOffset;

		attackCooldownTimer = -1.0f;

		obstacles = new List<GameObject>();

		// (task) Rotate the enemy to face the target
	}

	// Update is called once per frame
	void Update()
	{
		Debug.DrawRay( transform.position, 1.0f * transform.forward, Color.blue, 0, false );   // z axis

		if( isAttacking && towerToAttack == null )
		{
			stopMoving = false;
			isAttacking = false;
			Debug.Log( "enemy defeated, glory to Arstotzka" );
		}

		if( stopMoving == false )
		{
			// move movement code here
			// Check if the enemy is within x distance of the target
			// If the enemy has reached that distance, stop moving
			if( (transform.position - targetLocation).magnitude > enemyOffset )
			{
				transform.position = Vector3.MoveTowards( transform.position, targetLocation, speed * Time.deltaTime );
			}
		}

		if( isAttacking )
		{
			attackCooldownTimer -= Time.deltaTime;
		}

	}

	private void OnCollisionEnter( Collision collision )
	{

	}


	private void OnTriggerStay( Collider other )
	{
		if( other.gameObject.tag == "DuplicatorTower" ||
			other.gameObject.tag == "GateTower" ||
			other.gameObject.tag == "DamageBuffTower" )
		{
			// possible bug, if there's more than one tower within the collider,
			// the enemy will only attack one per cooldown
			// lets call that a feature for now...
			if( attackCooldownTimer <= 0.0f )
			{
				attackCooldownTimer = attackCooldown;
				
				Tower towerToAttack = other.gameObject.GetComponent<Tower>();
				if( towerToAttack != null )
				{
					towerToAttack.TakeDamage( attackPower );
				}
			}

			stopMoving = true;
		}
		//else if( other.gameObject.tag == "Enemy" )
		//{
		//	stopMoving = true;
		//}

	}

	private void OnTriggerEnter( Collider other )
	{
		if( other.gameObject.tag == "DuplicatorTower" ||
			other.gameObject.tag == "GateTower" ||
			other.gameObject.tag == "DamageBuffTower" )
		{
			stopMoving = true;
			isAttacking = true;

			towerToAttack = other.gameObject.GetComponent<Tower>();
			// obstacles.Add( other.gameObject )
		}
		//else if( other.gameObject.tag == "Enemy" )
		//{
		//	stopMoving = true;
		//}
	}

	//private void OnTriggerExit( Collider other )
	//{
	//	if( other.gameObject.tag == "Enemy" )
	//	{
	//		stopMoving = false;
	//	}
	//}

	public void TakeDamage( float amount )
	{
		health -= amount;

		if( health <= 0.0f )
		{
			// the enemy has taken fatal damage, terminate it
			SpawnManager.Instance.EnemyDied();
			Destroy( gameObject );
		}
	}
}
