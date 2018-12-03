using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Porta2 : MonoBehaviour {

	public Transform p2;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	
	void OnCollisionStay(Collision other)
	{
		if (other.gameObject.tag == "Player") {
			// Debug.Log("zxcvvzv");
			
			other.gameObject.transform.position = new Vector3(p2.position.x, p2.position.y, p2.position.z);
			
		}
	}
}
