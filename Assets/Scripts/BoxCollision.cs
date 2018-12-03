using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxCollision : MonoBehaviour {

	
	int count = 0;
	void OnParticleCollision(GameObject other)
	{
		count++;
		if (count > 30) {
			StartCoroutine( hideBox() );
		}
	}

	IEnumerator hideBox() {
		
		count = 0;
		gameObject.SetActive(false);
		yield return new WaitForSeconds(5f);
		gameObject.SetActive(true);
	}
}
