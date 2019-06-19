using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrinkBall : MonoBehaviour {
    private float speed;
    public GameObject ripple;
	// Use this for initialization
	void Start () {
        speed = 0.002f;

    }
	
	// Update is called once per frame
	void Update () {
        if (this.transform.localScale.x < 0.3f)
        {
            Destroy(gameObject);
            GameObject shockwaveClone = Instantiate(ripple, transform.position, Quaternion.identity);
            Destroy(shockwaveClone, 2f);
        }
	}
    private void FixedUpdate()
    {
        transform.localScale -= new Vector3(speed, speed, speed);
    }
}
