using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeCircle : MonoBehaviour {

	float radius = 5f;
	public Transform parentPos;
	void Start () {
		
	}
	
	// Update is called once per frame
	Vector3 pos;
	bool check;

	void Update () {
		
		if (check) {
			pos.x = radius * Mathf.Sin(Time.fixedTime * 14f) + parentPos.position.x ;
			pos.z = radius * Mathf.Cos(Time.fixedTime * 14f) + parentPos.position.z ;
			transform.position = pos;
		}
		
	}

	void OnEnable()
	{
		check = true;
		
	}

	
	void OnDisable()
	{
		check = false;
	}
}
