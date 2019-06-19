using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour {
    public GameObject shockwave;

	// Use this for initialization
	void Start () {
        GameObject shockwaveClone = Instantiate(shockwave, transform.position, Quaternion.identity);
        Destroy(shockwaveClone, 2f);
        Collider[] nearObjects = Physics.OverlapSphere(gameObject.transform.position, 4);
        foreach (Collider obj in nearObjects) {
            if (obj.tag == "MotherBall" || obj.tag == "ChildBall" || obj.tag == "ExplosionBall" || obj.tag == "StickyBall" || obj.tag == "BossBall")
            {
                Destroy(obj.gameObject);
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
