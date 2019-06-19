using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Touch : MonoBehaviour
{
    //multi touch
    private GameObject[] touchOld;
    private List<GameObject> listTouch;
    private RaycastHit vision;

    //combo variables
    //private static float comboTime = 0;
    //private static int combo = 0;
    //private float t;
    //private RotateText rotateText;

    // Use this for initialization
    void Start()
    {
        listTouch = new List<GameObject>();
        //rotateText = FindObjectOfType<RotateText>();
    }
		
    private void Update()
    {
		//TOUCH

		if(Input.touchCount > 0)
        {
            touchOld = new GameObject[listTouch.Count];
            listTouch.CopyTo(touchOld);
            listTouch.Clear();
            for(int i = 0; i<Input.touchCount; i++)
            {
                if(Physics.SphereCast(Camera.main.ScreenPointToRay(Input.GetTouch(i).position), 1, out vision))
                {
                    GameObject objectTouch = vision.transform.gameObject;
                    listTouch.Add(objectTouch);
                    switch (Input.GetTouch(i).phase)
                    {
                        case TouchPhase.Began:
                            objectTouch.SendMessage("OnTouchDown", vision.point, SendMessageOptions.DontRequireReceiver);
                            break;

                        case TouchPhase.Ended:
                            objectTouch.SendMessage("OnTouchUp", vision.point, SendMessageOptions.DontRequireReceiver);
                            break;

                        case TouchPhase.Moved:
                        case TouchPhase.Stationary:
                            objectTouch.SendMessage("OnTouchHold", vision.point, SendMessageOptions.DontRequireReceiver);
                            break;
                    }
                }
            }
            foreach(GameObject g in touchOld)
            {
                if (!listTouch.Contains(g) && g != null)
                {
                    g.SendMessage("OnTouchUp", vision.point, SendMessageOptions.DontRequireReceiver);
                }
            }
		} 
        
    }

    //public void Combo()
    //{
    //    t = Time.realtimeSinceStartup - comboTime;
    //    comboTime = Time.realtimeSinceStartup;
    //    if (t < 3)
    //    {
    //        combo += 1;
    //        if (combo == 3)
    //        {
    //            rotateText.Cool();
    //        }
    //        else if (combo == 5)
    //        {
    //            rotateText.Great();
    //        }
    //        else if (combo > 5) rotateText.Excellent();
    //        //            //Debug.Log(combo);
    //    }
    //    else
    //    {
    //        combo = 0;
    //        //            //Debug.Log(combo);
    //    }
    //}

    //public int getCombo()
    //{
    //    return combo;
    //}

    //public void resetCombo()
    //{
    //    combo = 0;
    //}
    /*
    private void OnMouseDown()
    {
		if (Physics.SphereCast (Camera.main.ScreenPointToRay (Input.mousePosition), 1, out vision)) {
			//Debug.Log ("DOWN");
			switch (vision.collider.tag) {
				case "MotherBall":
					Destroy (vision.collider.gameObject);
					SpawnChild (vision.collider.gameObject.transform.position);
					level1Controller.numberOfBalls--;
					
					break;

				case "ChildBall":
					Combo ();
					PointText (combo);
					level1Controller.numberOfBalls--;
					Instantiate (explosion, vision.collider.gameObject.transform.position, Quaternion.identity);
					Destroy (vision.collider.gameObject);
					break;
			}
		}
    }

    private void OnMouseDrag()
    {
		if (Physics.SphereCast(Camera.main.ScreenPointToRay(Input.mousePosition),1, out vision))
        {
			switch (vision.collider.tag) {
				case "BossBall":
					//generate Time count
						
					if (!isDragging) {
						startTime = Time.time;
						GameObject go = Instantiate (timeTextPrefab, vision.collider.gameObject.transform.position, Quaternion.identity);
						timeText = go.GetComponent<TextMesh> ();
						lastPos = currentPos;
						currentPos = vision.collider.gameObject.transform.position;

						ShowFloatingText (timeText);
					}
						////Debug.Log ("DRAG");
					isDragging = true;
					timer = Time.time - startTime;
					lastPos = currentPos;
					currentPos = vision.collider.gameObject.transform.position;
					ShowFloatingText (timeText);
						//if time >=2 -> pop
					if (timer >= 2) {
						Instantiate (explosion, vision.collider.gameObject.transform.position, Quaternion.identity);
						//subtract number of balls
						level1Controller.numberOfBalls--;
						Combo ();
						PointText (combo, true);
						//yield return new WaitForSeconds(1);
						//RecourageText();
						Destroy (vision.collider.gameObject);
						Destroy (timeText.gameObject);
						startTime = 0;
					}
					break;


				//not boss ball
			default:
				timer = 0;
				isDragging = false;
				startTime = 0;
				if (timeText != null) {
					Destroy (timeText.gameObject);
				}
				break;
			}

        }
    }

    private void OnMouseUp()
    {
		if (Physics.SphereCast (Camera.main.ScreenPointToRay (Input.mousePosition), 1, out vision)) {
			if (vision.collider.gameObject.CompareTag ("BossBall")) {
				timer = 0;
				if (timeText != null)
					Destroy (timeText.gameObject);
				isDragging = false;
				////Debug.Log ("UP");
			}
		}
    }
	*/
}
