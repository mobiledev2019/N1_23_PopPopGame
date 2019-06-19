using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDrag : MonoBehaviour {

	private RaycastHit vision;
	//public Animator anim;
	public GameObject go;

	// Use this for initialization
	void Start () {
		//anim = go.GetComponent<Animator> ();
		//anim.SetInteger("level") = ...
	}
	
	// Update is called once per frame
	void Update () {
//		if (Input.touchCount > 0) {
//			//Debug.Log ("touchCount>0");
//			if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.GetTouch(0).deltaPosition), out vision)){
//				//Debug.Log ("RayCast");
//				//Debug.Log (vision.collider.transform.position);
//				if (Input.GetTouch (0).phase == TouchPhase.Moved) {
//					//Debug.Log (vision.collider.transform.position);
//				}
//			}
//		}
	}

	private void OnMouseDrag()
	{
		////Debug.Log ("Drag");
		if (Physics.Raycast (Camera.main.ScreenPointToRay (Input.mousePosition), out vision)) {
			//Debug.Log (vision.collider.name);
		}
	
	}

	private void OnMouseDown(){
		//Debug.Log ("DOWN");
	}

	private void OnMouseUp(){
		//Debug.Log ("UP");
	}
}
