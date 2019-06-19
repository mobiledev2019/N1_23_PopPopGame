using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBlock : MonoBehaviour {

	public float speed;
	public GameObject pos0;
	public GameObject pos1;
	public GameObject pos2;
	public GameObject pos3;

	// Use this for initialization
	private bool isMoving;
	private int destination;
	private float distance;
	private float tempSpeed;
	void Start () {
		isMoving = false;
		destination = 0;
		tempSpeed = speed;
	}
	
	// Update is called once per frame
	void Update () {
		if(isMoving && destination == 0){
			//Debug.Log("move0");
			//StartCoroutine(MoveTo(pos0.transform.position));
			MoveTo(pos0.transform.position);
			
		}
		if(isMoving && destination == 1){
			//Debug.Log("move1");
			//StartCoroutine(MoveTo(pos1.transform.position));
			MoveTo(pos1.transform.position);
		}
		if(isMoving && destination == 2){
			//Debug.Log("move2");
			//StartCoroutine(MoveTo(pos2.transform.position));
			MoveTo(pos2.transform.position);
		}
		if(isMoving && destination == 3){
			//Debug.Log("move3");
			//StartCoroutine(MoveTo(pos3.transform.position));
			MoveTo(pos3.transform.position);
		}

		//stop
		if(!isMoving){
			//Debug.Log("stop");
			StartCoroutine(Stop());
		}
	}

	// private IEnumerator MoveTo(Vector3 pos){
	// 	isMoving = true;
	// 	float distance = Vector3.Distance(this.transform.position, pos);
	// 	//Debug.Log(distance);
	// 	while(distance > 0){
	// 		//Debug.Log(distance);
	// 		Vector3 direction = (pos - this.transform.position).normalized;
	// 		this.transform.Translate(direction * speed * Time.deltaTime);
	// 		distance = Vector3.Distance(this.transform.position, pos);
	// 	}

	// 	yield return new WaitForSeconds(2f);
		

	// 	destination++;
	// 	destination %=4;
	// 	isMoving = false;
	// }

	private void MoveTo(Vector3 pos){
		isMoving = true;
		//distance = Vector3.Distance(this.transform.position, pos);
		//Debug.Log(distance);

		Vector3 direction = (pos - this.transform.position).normalized;
		this.transform.position = Vector3.MoveTowards(transform.position, pos, speed * Time.deltaTime);
		distance = Vector3.Distance(this.transform.position, pos);

		if(distance <= 0.1){
			destination++;
			destination %=4;
			isMoving = false;
		}
		
	}

	IEnumerator Stop(){
		yield return new WaitForSeconds(1f);
		isMoving = true;
		tempSpeed = speed;
	}
}
