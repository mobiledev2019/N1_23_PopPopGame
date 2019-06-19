using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeLayer : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine(ExecuteAfterTime(0.5f));
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator ExecuteAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        
        if(gameObject != null)
        {
            gameObject.layer = 0;
        }
    }
}
