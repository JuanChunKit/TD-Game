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

	private Tower towerToAttack = null;

	private List<GameObject> obstacles;

	[SerializeField]
	private float attackPower = 5.0f;

	[SerializeField]
	private bool logDebugInfo = false;

	public int obsCount = 0;
	public int itemsInObs = 0;

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
			isAttacking = false;
			Debug.Log( "enemy defeated, glory to Arstotzka" );
		}

		if( isAttacking )
		{
			attackCooldownTimer -= Time.deltaTime;
		}


		if( obstacles.Count > 0 )
		{
			// Remove gameObjects that have been deleted
			for( int i = (obstacles.Count - 1); i > -1; i-- )
			{
				if( obstacles[ i ] == null )
				{
					obstacles.RemoveAt( i );
					obsCount -= 1;
					if( logDebugInfo )
					{
						Debug.Log( "(obstacles - 1) an obstacle dissapeared!" );
						Debug.Log( "obstacles remaining: " + obstacles.Count );
					}
				}

			}
		}
		else
		{
			if( (transform.position - targetLocation).magnitude > enemyOffset )
			{
				transform.position = Vector3.MoveTowards( transform.position, targetLocation, speed * Time.deltaTime );
			}
		}

		itemsInObs = obstacles.Count;
	}

	private void OnCollisionEnter( Collision collision )
	{

	}

	/// <Better movement control>
	/// Stop relaying on stopMoving bool to know when to stop moving
	/// Use a list instead to determmine when there are no longer obstacles
	/// </summary>
	/// <param name="other"></param>

	private void OnTriggerEnter( Collider other )
	{
		if( other.gameObject.tag == "DuplicatorTower" ||
			other.gameObject.tag == "GateTower" ||
			other.gameObject.tag == "DamageBuffTower" )
		{
			isAttacking = true;

			towerToAttack = other.gameObject.GetComponent<Tower>();


			obstacles.Add( other.gameObject );
			obsCount += 1;
			if( logDebugInfo )
			{
				Debug.Log( "(obstacles + 1) Attacking tower" );
				Debug.Log( "obstacles remaining: " + obstacles.Count );
			}
		}
		else if( other.gameObject.tag == "Enemy" )
		{
			obstacles.Add( other.gameObject );
			obsCount += 1;
			if( logDebugInfo )
			{
				Debug.Log( "(obstacles + 1) Comrade is blocking me" );
				Debug.Log( "obstacles remaining: " + obstacles.Count );
			}
		}
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

				Tower toAttack = other.gameObject.GetComponent<Tower>();
				if( toAttack != null )
				{
					toAttack.TakeDamage( attackPower );
				}
			}
		}
	}

	private void OnTriggerExit( Collider other )
	{
		if( other.gameObject.tag == "Enemy" )
		{
			obstacles.Remove( other.gameObject );
			obsCount -= 1;
			if( logDebugInfo )
			{
				Debug.Log( "(obstacles - 1) Comrade got out of the way" );
				Debug.Log( "obstacles remaining: " + obstacles.Count );
			}
		}
	}

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
