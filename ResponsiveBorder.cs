using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResponsiveBorder : MonoBehaviour {
	public Camera cam;
	private MeshRenderer mr;
	// Use this for initialization
	void Start () {
        ////Debug.Log(gameObject.name);
		mr = GetComponent<MeshRenderer> ();
    }
	
	// Update is called once per frame
	void FixedUpdate () {
//		//Debug.Log (mr.isVisible);
		if (!mr.isVisible)
        {
            ////Debug.Log("aaaa");
            cam.transform.Translate(Vector3.back);
        }
    }
}
