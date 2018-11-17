using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tower : MonoBehaviour
{

	[SerializeField]
	private float maxHealth = 100.0f;
	[SerializeField]
	private float flashDelay = 0.25f;
	[SerializeField]
	private Material normalMaterial;
	[SerializeField]
	private Material flashMaterial;

	

	private float health;

	private float flashDelayTimer;
	private bool isFlashing = false;

	protected virtual void Awake()
	{
		health = maxHealth;

		// start ready to flash
		flashDelayTimer = -1.0f;
		isFlashing = false;
	}

	protected virtual void Update()
	{
		if( isFlashing )
		{
			flashDelayTimer -= Time.deltaTime;

			if(flashDelayTimer <= 0.0f )
			{
				isFlashing = false;
				GetComponent<Renderer>().material = normalMaterial;
			}
		}


	}

	public void TakeDamage( float damage )
	{
		health -= damage;

		// don't flash again if you are in the middle of a flash
		if( isFlashing == false )
		{
			flashDelayTimer = flashDelay;
			isFlashing = true;
			GetComponent<Renderer>().material = flashMaterial;
		}

		if( health <= 0.0f )
		{
			DieDramatically();
		}
	}

	protected virtual void DieDramatically()
	{
		StartCoroutine( FlashyDeath() );

	}

	IEnumerator FlashyDeath()
	{
		GetComponent<Renderer>().material = flashMaterial;

		yield return new WaitForSeconds( flashDelay );
		Destroy( gameObject );
	}

}
