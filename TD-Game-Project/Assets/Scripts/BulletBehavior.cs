using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
		// Set the bullet's velocity

	}
	
	// Update is called once per frame
	void Update ()
    {
        // Move the object forward along its z axis 1 unit/second.
        transform.Translate( 0, 0, Time.deltaTime );

        // add some code to make the bullet self destroy after x amount of time or
        // make bullet destroy itself after hitting the edge of the map

    }
}
