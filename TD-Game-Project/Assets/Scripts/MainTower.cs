using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainTower : MonoBehaviour
{
	enum CurrentShot { none, weak, normal, strong };

	// speed is the rate at which the object will rotate
	public float rotationSpeed          = 25.0f;

	public float beforeOptimalTime  = 1.0f;
	public float afterOptimalTime   = 2.0f;

	public float weakShot           = 80.0f;
	public float normalShot			= 100.0f;
	public float strongShot         = 150.0f;

	public float weakShotSuccessRate    = 90.0f;
	public float strongShotSuccessRate  = 85.0f;

	public Material restingColor;
	public Material weakShotColor;
	public Material normalShotColor;
	public Material strongShotColor;

	private CurrentShot currentShot;
	private bool isChargingShot;
	

	private float shotTime          = 0.0f;

	public Rigidbody mainBulletPrefab;

	private bool canShoot = false;

	[SerializeField]
	private float coolDown = .25f;
	private float coolDownTimer;

	private void Awake()
	{
		coolDownTimer = coolDown;
	}

	private void Start()
    {
		currentShot = CurrentShot.none;
		shotTime = 0.0f;
		isChargingShot = false;
    }
    

	private void Update()
	{
		coolDownTimer -= Time.deltaTime;

		if( canShoot && coolDownTimer < 0.0f )
		{
			if( Input.GetButton( "Fire1" ) )
			{
				shotTime += Time.deltaTime;

				if( currentShot != CurrentShot.strong )
				{
					if( shotTime < beforeOptimalTime )
					{
						if( currentShot != CurrentShot.weak )
						{
							GetComponent<Renderer>().material.CopyPropertiesFromMaterial( weakShotColor );
							currentShot = CurrentShot.weak;
							//Debug.Log( "weak" );
						}
					}
					else if( shotTime > afterOptimalTime )
					{
						// Once we get to this point we don't need to update the color anymore until the player shoots
						// and starts charging their shot again
						GetComponent<Renderer>().material.CopyPropertiesFromMaterial( strongShotColor );
						currentShot = CurrentShot.strong;
						//Debug.Log( "strong" );
					}
					else
					{
						if( currentShot != CurrentShot.normal )
						{
							GetComponent<Renderer>().material.CopyPropertiesFromMaterial( normalShotColor );
							currentShot = CurrentShot.normal;
							//Debug.Log( "normal" );
						}
					}
				}
			}

			if( Input.GetButtonUp( "Fire1" ) )
			{
				FireMainBullet();
				shotTime = 0.0f;
				isChargingShot = false;

				coolDownTimer = coolDown;

				// return the tower the the idle color
				GetComponent<Renderer>().material.CopyPropertiesFromMaterial( restingColor );
				currentShot = CurrentShot.none;
			}
		}

		if( isChargingShot )
		{
			
		}

		

	}

	// Stop the turret from shooting
	public void SafetyOn( )
	{
		canShoot = false;
	}

	public void SafetyOff()
	{
		canShoot = true;
	}

	void FixedUpdate()
	{
		// Generate a plane that intersects the transform's position with an upwards normal.
		Plane playerPlane = new Plane( Vector3.up, transform.position );

		// Generate a ray from the cursor position
		Ray ray = Camera.main.ScreenPointToRay( Input.mousePosition );

		// Determine the point where the cursor ray intersects the plane.
		// This will be the point that the object must look towards to be looking at the mouse.
		// Raycasting to a Plane object only gives us a distance, so we'll have to take the distance,
		//   then find the point along that ray that meets that distance.  This will be the point
		//   to look at.
		float hitdist = 0.0f;
		// If the ray is parallel to the plane, Raycast will return false.
		if( playerPlane.Raycast( ray, out hitdist ) )
		{
			// Get the point along the ray that hits the calculated distance.
			Vector3 targetPoint = ray.GetPoint( hitdist );

			// Determine the target rotation.  This is the rotation if the transform looks at the target point.
			Quaternion targetRotation = Quaternion.LookRotation( targetPoint - transform.position );

			// Smoothly rotate towards the target point.
			transform.rotation = Quaternion.Slerp( transform.rotation, targetRotation, rotationSpeed * Time.deltaTime );
		}
	}

	void FireMainBullet()
	{
		// Make an empty game object in the editor and attach it to the tip of the gun.
		// Use that GO position to instantiate the bullet
		Transform bulletSpawnTransform = this.gameObject.transform.GetChild( 0 );

		Rigidbody bulletClone;

		float shittyRandomNumber = Random.Range( 0.0f, 100.0f );


		if( currentShot == CurrentShot.weak )
		{
			if( shittyRandomNumber <= weakShotSuccessRate )
			{
				bulletClone = Instantiate( mainBulletPrefab, bulletSpawnTransform.position, transform.rotation );
				bulletClone.GetComponent<BulletBehavior>().bulletDamage = weakShot;
			}
		}
		else if( currentShot == CurrentShot.strong )
		{
			if( shittyRandomNumber <= strongShotSuccessRate )
			{
				bulletClone = Instantiate( mainBulletPrefab, bulletSpawnTransform.position, transform.rotation );
				bulletClone.GetComponent<BulletBehavior>().bulletDamage = strongShot;
			}
		}
		else
		{
			bulletClone = Instantiate( mainBulletPrefab, bulletSpawnTransform.position, transform.rotation );
			bulletClone.GetComponent<BulletBehavior>().bulletDamage = normalShot;
		}

	}

}
