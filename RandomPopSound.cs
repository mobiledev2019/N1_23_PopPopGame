using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPopSound : MonoBehaviour {
    public AudioClip[] popSound;

	// Use this for initialization
	void Start () {
        GetComponent<AudioSource>().clip = popSound[Random.Range(0, 3)];
        GetComponent<AudioSource>().Play();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
