using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBehavior : MonoBehaviour {

	public Material normalColor;
	public Material FlashColor;

	private void Awake()
	{
		GetComponent<Renderer>().material.CopyPropertiesFromMaterial( normalColor );
	}

	private void OnCollisionEnter( Collision collision )
	{
		if( collision.gameObject.tag == "Bullet ")
			StartCoroutine( FlashBack() );
	}

	IEnumerator FlashBack()
	{
		GetComponent<Renderer>().material.CopyPropertiesFromMaterial( FlashColor );
		yield return new WaitForSeconds( .1f );
		GetComponent<Renderer>().material.CopyPropertiesFromMaterial( normalColor );
	}
}
