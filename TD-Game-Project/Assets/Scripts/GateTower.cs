﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateTower : Tower {

	protected override void Awake()
	{
		base.Awake();
	}

	// Update is called once per frame
	protected override void Update()
	{
		base.Update();

	}

	private void OnTriggerEnter( Collider other )
	{
		if( other.gameObject.tag == "Bullet" )
		{
			other.gameObject.GetComponent<BulletBehavior>().MakeBigBomb();
		}
	}
}
