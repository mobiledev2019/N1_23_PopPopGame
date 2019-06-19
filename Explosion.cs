using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {

	public float radius = 5.0f;
	public float power = 5000.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseDown(){
		Collider[] colliders = Physics.OverlapSphere (transform.position, radius);
		foreach (Collider hit in colliders) {
			Rigidbody rb = hit.GetComponent<Rigidbody> ();
			if (rb != null)
				rb.AddExplosionForce (power, transform.position, radius);
		}
		Destroy (gameObject);
	}
}
