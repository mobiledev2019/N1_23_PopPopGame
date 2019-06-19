using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User{

	private int id;
	private string email;
	private string name;
	private string password;

	public User(int id, string name, string email, string password){
		this.id = id;
		this.email = email;
		this.name = name;
		this.password = password;
	}

	public string getEmail(){
		return this.email;
	}
	public string getName(){
		return this.name;
	}

	public int getId(){
		return this.id;
	}
	
}
