using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessSpawner : MonoBehaviour {
    public List<GameObject> balls;
    public float spawnDelay = 4;
    public float spawnRate = 3f;
    public int stage = 0;
    public float spawnX = 0;

    // Use this for initialization
    void Start ()
    {
        Debug.Log("Start spawning");
        StartCoroutine(Wait());
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if (stage == 0)
        {
            InvokeRepeating("Spawn", spawnDelay, spawnRate);
            stage++;
        }
        if(Time.realtimeSinceStartup > (60*stage) && spawnRate > 0.5f && levelController.endlessLife > 0)
        {
            CancelInvoke();
            stage++;
            InvokeRepeating("Spawn", 0, spawnRate - stage * 0.25f);
        }
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(spawnX, 12, -1.5f), 5 * Time.deltaTime);
        if(levelController.endlessLife == 0)
        {
            CancelInvoke();
        }
	}
    public void Spawn()
    {
        GameObject ball = Instantiate(balls[Random.Range(0,balls.Count)], transform.position, transform.rotation);
        Rigidbody rb = ball.GetComponent<Rigidbody>();
        rb.AddForce(Random.Range(-60, 60), Random.Range(-125, -175), 0);
        spawnX = Random.Range(-5.5f, 5.5f);
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(2);
    }
}
