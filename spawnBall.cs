using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class spawnBall : MonoBehaviour {

    [System.Serializable]
    public class Spawn
    {
        public GameObject ball;
        //public float chance;
        public bool isSpawned = false;
    }

    private float totalChance;

    public Vector3 left, right;
    public List<Spawn> balls;
    public GameObject ballsHolder;

    private Vector3 pos;
    private GameObject t;

    private void Awake()
    {
        OnValidate();
    }

    private void OnValidate()
    {
        //totalChance = 0;
        //foreach(Spawn s in balls)
        //{
        //    totalChance += s.chance;
        //}
    }

    // Use this for initialization
    void Start () {
        GameObject t1 = new GameObject("temp");
        t = Instantiate(t1, ballsHolder.transform);
        StartCoroutine("SpawnBall");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator SpawnBall()
    {
        yield return new WaitForSeconds(2);
        //while (true)
        //{
        for (int i = 0; i < balls.Count; i++)
        {
        //float pick = Random.value * totalChance;
        //int chosenIndex = 0;
        //float cumulativeWeight = balls[0].chance;

        //while (pick > cumulativeWeight && chosenIndex < balls.Count - 1)
        //{
        //    chosenIndex++;
        //    cumulativeWeight += balls[chosenIndex].chance;
        //}
        //pos = new Vector3(Random.Range(left.x, right.x), left.y, left.z);
        //GameObject go = Instantiate(balls[chosenIndex].ball, pos, Quaternion.identity, ballsHolder.transform);
        //Rigidbody rb = go.GetComponent<Rigidbody>();
        //rb.AddForce(0, Random.Range(-100, -200), 0);
        //yield return new WaitForSeconds(1f);

            int index = Random.Range(0, balls.Count);
            while (balls[index].isSpawned)
            {
                index = Random.Range(0, balls.Count);
            }
            pos = new Vector3(Random.Range(left.x, right.x), left.y, left.z);
            GameObject go = Instantiate(balls[index].ball, pos, Quaternion.identity, ballsHolder.transform);
            Rigidbody rb = go.GetComponent<Rigidbody>();
            rb.AddForce(Random.Range(-60, 60), Random.Range(-125, -175), 0);
            balls[index].isSpawned = true;
            if (index == 9)
            {
                yield return new WaitForSeconds(2f);
            }
            else
            {
                yield return new WaitForSeconds(1.25f);
            }
            if (i == balls.Count - 1)
            {
                Destroy(t);
            }
        }


        //yield return new WaitForSeconds(0.7f);
        //}
    }
}
