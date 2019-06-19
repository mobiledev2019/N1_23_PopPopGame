using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour {

	public GameObject theOtherPortal;

	private Vector3 vectorGoIn; //position ball go in - position of this portal
	private Vector3 vectorAfterRotating; // vector of out Ball after rotate between two portal;
	private Vector3 posGoOut; //local pos of Ball -> the other portal
	private float angleBetween2Portal; //angle in degree
	
	private Vector3 vectorFrontOfThisPortal; // vector phap tuyen
	private Vector3 vectorFrontOfTheOtherPortal;
	// Use this for initialization
	void Start () {
		// vectorFrontOfThisPortal = Quaternion.Euler(0,0,transform.rotation.eulerAngles.z) * new Vector3(1,0,0);
		// vectorFrontOfTheOtherPortal = Quaternion.Euler(0,0,theOtherPortal.transform.rotation.eulerAngles.z) * new Vector3(1,0,0);

		// angleBetween2Portal = Vector3.Angle(vectorFrontOfThisPortal, vectorFrontOfTheOtherPortal);
		
	}
	
	// Update is called once per frame
	void Update () {

		vectorFrontOfThisPortal = Quaternion.Euler(0,0,transform.rotation.eulerAngles.z) * new Vector3(1,0,0);
		vectorFrontOfTheOtherPortal = Quaternion.Euler(0,0,theOtherPortal.transform.rotation.eulerAngles.z) * new Vector3(1,0,0);

		angleBetween2Portal = Vector3.Angle(vectorFrontOfThisPortal, vectorFrontOfTheOtherPortal);

		////Debug.Log(angleBetween2Portal);
		//ve vector phap tuyen
		////Debug.DrawLine(transform.position, transform.position + vectorFrontOfThisPortal * 5.0f) ;

		////Debug.DrawLine(theOtherPortal.transform.position, posGoOut, Color.red);
		// //Debug.DrawLine(theOtherPortal.transform.position, vectorAfterRotating, Color.cyan);
	}

	void OnTriggerEnter(Collider ball)
	{
		vectorGoIn = ball.transform.position - transform.position ;

		float degreeBetweenInAndFront = Vector3.Angle(vectorGoIn, vectorFrontOfThisPortal);
		vectorAfterRotating = Quaternion.Euler(0,0,angleBetween2Portal + 2 * degreeBetweenInAndFront) * vectorGoIn;

		// //Debug.Log(Quaternion.AngleAxis(angleBetween2Portal, vectorFrontOfThisPortal).eulerAngles);

		posGoOut = theOtherPortal.transform.position + vectorAfterRotating;

		ball.transform.position = posGoOut + vectorFrontOfTheOtherPortal;

		//change direction of ball's velocity
		ball.GetComponent<Rigidbody>().velocity = Quaternion.Euler(0,0,angleBetween2Portal - 180) * ball.GetComponent<Rigidbody>().velocity;

	}
}
