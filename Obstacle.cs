using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle{
	private GameObject obj;
	private Vector3 position;

	public Obstacle(){}
	public Obstacle(GameObject obj, Vector3 pos){
		this.obj = obj;
		this.position = pos;
	}

	public void setObj(GameObject obj){
		this.obj = obj;
	}
	public GameObject getObj(){
		return this.obj;
	}
	public void setPosition(Vector3 pos){
		this.position = pos;
	}
	public Vector3 getPosition(){
		return this.position;
	}

	public Dictionary<string, object> toDictionary(){
		Dictionary<string, object> obstacle = new Dictionary<string,object>();
		obstacle["name"] = obj.name;
		obstacle["position"] = position.x +","+ position.y+","+position.z;

		return obstacle;
	}

}
