using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMove : MonoBehaviour {

	float rx;
	float speed = 5f;
	Rigidbody rb;
	public GameObject cc;
	private float length = 5f;
	void Start () {
		transform.LookAt(Vector3.zero);
		rx = Random.Range(1.2f, 5f);
		rb = GetComponent<Rigidbody>();
		// StartCoroutine (circle());
	}

	IEnumerator circle() {
		int i = 0;
		while (i < 100) {

		var p =	Vector2.zero + Random.insideUnitCircle.normalized * length;
		Instantiate(cc,new Vector3(p.x, 0f, p.y),Quaternion.identity);
		yield return new WaitForSeconds(1f);
		}
	}

	
	float time;
	void Update () {
		// rb.velocity = transform.forward * 50f;
		time += Time.deltaTime;
		
		// rb.AddRelativeForce(transform.forward * 10f);
		// transform.Translate(new Vector3(0f,0f,1f) * speed * Time.deltaTime );
		// transform.Rotate(new Vector3(rx,0f,0f));
		// transform.LookAt(Vector3.zero);
		// transform.Rotate(transform.forward);
		
		Vector3 target = Vector3.zero - transform.position;
		// Vector3 newDir = Vector3.RotateTowards(transform.forward, target, time, 0f);
		// transform.rotation = Quaternion.LookRotation(newDir);
		// Quaternion tt = Quaternion.LookRotation(transform.forward);
		// transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.identity, speed);
	}

	/// <summary>
	/// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
	/// </summary>
	void FixedUpdate()
	{
		// Debug.Log("fixed");
		// rb.velocity = transform.forward * 3 * time;
		// rb.AddForce(transform.forward * 15f, ForceMode.Acceleration);
	}
}
