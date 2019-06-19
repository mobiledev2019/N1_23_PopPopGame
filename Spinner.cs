using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MonoBehaviour {
    private bool spinning;

	// Use this for initialization
	void Start () {
        spinning = false;
	}
	
	// Update is called once per frame
	void Update () {
        //if (spinning)
        //{
            transform.Rotate(Vector3.forward * Time.deltaTime * 150);
        //}
	}
}
