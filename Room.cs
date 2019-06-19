using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room  {
	private string id;
	private PairInvitation pair;
	private int mapIndex;

	public Room(){}
	public Room(string id, PairInvitation pair){
		this.id = id;
		this.pair = pair;
		this.mapIndex = 0;
	}

	public string getId(){
		return this.id;
	}
	public PairInvitation getPair(){
		return this.pair;
	}
	public void setId(string id){
		this.id = id;
	}
	public void setPair(PairInvitation pair){
		this.pair = pair;
	}
	public int getMapIndex(){
		return this.mapIndex;
	}
	public void setMapIndex(int mapIndex){
		this.mapIndex = mapIndex;
	}
}
