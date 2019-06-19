using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyText : MonoBehaviour {

    public float destroyTime;

    private Vector3 offset;
    private Vector3 rand = new Vector3(0.8f, 0, 0);

	// Use this for initialization
	void Start () {
        Destroy(gameObject, destroyTime);
        offset = new Vector3(0, 2, 0);
        transform.localPosition += offset;
        transform.localPosition += new Vector3(Random.Range(-rand.x, rand.x), Random.Range(-rand.y, rand.y), Random.Range(-rand.z, rand.z));
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
