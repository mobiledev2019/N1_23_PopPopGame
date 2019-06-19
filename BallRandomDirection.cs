using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallRandomDirection : MonoBehaviour {

	// Use this for initialization
	void Start () {
        this.GetComponent<Rigidbody>().AddForce(Random.Range(-25f, 25f), Random.Range(-25f, 25f), 0);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
