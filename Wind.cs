using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : MonoBehaviour {
	public Vector3 vectorWindForce;
	private List<GameObject> targets;

	// Use this for initialization
	void Start () {
		targets = new List<GameObject>();
	}
	
	// Update is called once per frame
	void Update () {
		////Debug.Log(targets.Count);
		foreach(GameObject ball in targets){
			if(ball != null)
				ball.GetComponent<Rigidbody>().AddForce(vectorWindForce);
		}
	}
	private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("ChildBall") || other.CompareTag("MotherBall") || 
		other.CompareTag("BossBall") || other.CompareTag("ExplosionBall")){

            targets.Add(other.gameObject);
        }
    }


	private void OnTriggerExit(Collider other)
	{
		if(other.CompareTag("ChildBall") || other.CompareTag("MotherBall") || 
		other.CompareTag("BossBall") || other.CompareTag("ExplosionBall")){

            targets.Remove(other.gameObject);
        }
	}
}
