using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vortex : MonoBehaviour {
    public float x;
    public float y;
    public int t;
    public Vector3 oldPos, newPos;

	// Use this for initialization
	void Start ()
    {
        getDirection();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        newPos = transform.position;
        if (t == 60 || oldPos == newPos)
        {
            getDirection();
        }
        transform.Translate(x * Time.deltaTime, y * Time.deltaTime, 0);
        oldPos = newPos;
        t++;
    }
    private void OnCollisionEnter(UnityEngine.Collision collision)
    {
        if (collision.collider.tag == "MotherBall" || collision.collider.tag == "ChildBall" || collision.collider.tag == "StickyBall" || collision.collider.tag == "ExplosionBall")
        {
            collision.collider.GetComponent<Rigidbody>().velocity = Vector3.Reflect(collision.collider.GetComponent<Rigidbody>().velocity * 1.5f, collision.contacts[0].normal);
        }
        getDirection();
    }
    public void getDirection()
    {
        x = Random.Range(-5f, 5f);
        y = Random.Range(-5f, 5f);
        if (Mathf.Abs(x) < 4 || Mathf.Abs(y) < 4)
        {
            getDirection();
        }
        t = 0;
    }
}
