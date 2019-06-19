using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour {

	public float slowdownFactor = 0.05f;
	public float slowdownLength = 2f;
	public static bool isSlowmotion;
	private float startTime;
	private bool isSavedTime;


	// Use this for initialization
	void Start () {
		isSlowmotion = false;
		isSavedTime = false;
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log(isSlowmotion);
		if(isSlowmotion){
			if(!isSavedTime){
				startTime = Time.realtimeSinceStartup;
				isSavedTime = true;
			}
			DoSlowmotion(1f);
			
		}
		else{
			Time.timeScale += (1f/ slowdownLength) * Time.unscaledDeltaTime;

			Time.timeScale = (Time.timeScale >= 1)? 1 : Time.timeScale;
		}
	}

	public void DoSlowmotion(float duration){
		if(startTime + duration < Time.realtimeSinceStartup){
			isSlowmotion = false;
			isSavedTime = false;
		}
		else{
			Time.timeScale = slowdownFactor;
			Time.fixedDeltaTime = Time.timeScale * 0.02f;
		}
		Debug.Log(startTime);
	}
}
