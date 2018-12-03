using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleCheck : MonoBehaviour {



	public Material hitMaterial;
	public Material originalMaterial;
	private MeshRenderer mr;
	void Start()
	{

		mr = GetComponent<MeshRenderer>();
	}
	void OnParticleCollision(GameObject other)
	{

		StartCoroutine( changeMaterial() );
		
	}

	IEnumerator changeMaterial() {

		mr.material = hitMaterial;
		yield return new WaitForSeconds(1f);
		mr.material = originalMaterial;
	}
	

	
}
