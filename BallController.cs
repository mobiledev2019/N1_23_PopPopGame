using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour {
	public List<GameObject> listBalls;

	private int step;	//step jump
	private int numberOfBalls;
	private bool isWaiting; //waiting time to spawn next ball;
	private bool isDone; //done when all balls are spawned
	private int index; //index of list balls
	private int indexStart; //start index
	private GameObject tempBall; // current ball
	private bool isBegin;

	// Use this for initialization
	void Start () {
		
		numberOfBalls = listBalls.Count;
		step = Random.Range(1,(numberOfBalls-1)/2); //1-> ((count-1)/2) -1
		isWaiting = false;
		isBegin = true;
		isDone = false;
		indexStart = 0;
		index = indexStart;
	}
	
	// Update is called once per frame
	void Update () {

		if(isBegin){
			StartCoroutine(WaitAtBeginning());
		}

		if(!isWaiting && !isDone && !isBegin){
			StartCoroutine(ActiveBall());
		}
	}

	private IEnumerator ActiveBall(){
		//key -> only 1 ActiveBall function call at 1 time (in 1s);
		isWaiting = true;

		tempBall = listBalls[index];
		tempBall.SetActive(true);

		index += step;
		//if index out of range
		if(index >= numberOfBalls){
			//next indexstart
			indexStart++;
			//if indexstart out of step
			if(indexStart >= step){
				isDone = true;
				goto br;
			}
			else{
				index = indexStart;
			}
		}

		yield return new WaitForSeconds(1f);
		isWaiting = false;

        //break when done	
        br:;
		////Debug.Log("done");
	}

	private IEnumerator WaitAtBeginning(){
		yield return new WaitForSeconds(2f);
		isBegin = false;
		////////Debug.Log("BEGIN");
	}
}
