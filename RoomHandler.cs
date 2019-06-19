using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomHandler : MonoBehaviour {

	private Room room;
	private PairInvitation pair;
	private User sender;
	private List<Obstacle> listObstacles;

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad(this);
		room = new Room();
		pair = new PairInvitation();
		listObstacles = new List<Obstacle>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public Room getRoom(){
		return this.room;
	}
	public void setRoom(Room room){
		this.room = room;
	}
	public PairInvitation getPair(){
		return this.pair;
	}

	public void setPair(PairInvitation pair){
		this.pair = pair;
	}
	public void setSender(User u){
		this.sender = u;
	}
	public User getSender(){
		return this.sender;
	}

	public List<Obstacle> getListObstacles(){
		return this.listObstacles;
	}
	public void setListObstacles(List<Obstacle> list){
		this.listObstacles = list;
	}

	public void addToListObstacles(Obstacle obstacle){
		this.listObstacles.Add(obstacle);
	}
}
