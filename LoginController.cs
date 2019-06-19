using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginController : MonoBehaviour {
	public GameObject loginForm;
	public GameObject registerForm;
	public GameObject alertPanel;

	// Use this for initialization
	void Start () {
		loginForm.SetActive(true);
		registerForm.SetActive(false);
		alertPanel.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		if(DatabaseHandler.isLoginFailed){
			alertPanel.SetActive(true);
			alertPanel.GetComponentInChildren<Text>().text = "Login failed!";
		}
			
		else
			alertPanel.SetActive(false);
	}

	public void SignupClick(){
		loginForm.SetActive(false);
		registerForm.SetActive(true);
	}

	public void RegisterClick(){
		loginForm.SetActive(true);
		registerForm.SetActive(false);
	}

	public void CancelClick(){
		loginForm.SetActive(true);
		registerForm.SetActive(false);
		DatabaseHandler.isLoginFailed = false;
		
		InputField[] ifs = loginForm.GetComponentsInChildren<InputField>();
		ifs[0].text = "";
		ifs[1].text = "";
	}
}
