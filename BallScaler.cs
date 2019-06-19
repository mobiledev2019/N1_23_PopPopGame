using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScaler : MonoBehaviour {
    public float speed;
	// Use this for initialization
	void Start () {
        //transform.position += new Vector3(Random.Range(0f, 1f), Random.Range(0f, 1f), 0);
        transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        speed = Random.Range(0.07f, 0.1f);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if(transform.localScale.x < 1.6f)
        {
            transform.localScale += new Vector3(speed, speed, speed);
        }
	}
}
