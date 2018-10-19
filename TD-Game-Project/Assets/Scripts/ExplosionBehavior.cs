using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionBehavior : MonoBehaviour
{

	// Use this for initialization
	void Start()
	{
		Destroy( gameObject, 0.4f );

	}

	// Update is called once per frame
	void Update()
	{

	}

	private void OnTriggerEnter( Collider other )
	{
		if( other.gameObject.tag == "Enemy" )
		{
			EnemyBehavior enemyScript = other.gameObject.GetComponent<EnemyBehavior>();
			enemyScript.TakeDamage( 100.0f );
		}
	}
}
