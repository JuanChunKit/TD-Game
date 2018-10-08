using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBehavior : MonoBehaviour {

	public Material normalColor;
	public Material FlashColor;

	private void OnCollisionEnter( Collision collision )
	{
		StartCoroutine( FlashBack() );
	}

	IEnumerator FlashBack()
	{
		GetComponent<Renderer>().material.CopyPropertiesFromMaterial( FlashColor );
		yield return new WaitForSeconds( .1f );
		GetComponent<Renderer>().material.CopyPropertiesFromMaterial( normalColor );
	}
}
