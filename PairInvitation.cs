using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PairInvitation {
	private User sender;
	private User receiver;

	public PairInvitation(){}

	public PairInvitation(User sender, User receiver){
		this.sender = sender;
		this.receiver = receiver;
	}
	public User getSender(){
		return sender;
	}
	public User getReceiver(){
		return receiver;
	}

	public void setSender(User u){
		this.sender = u;
	}
	public void setReceiver(User u){
		this.receiver = u;
	}

	public Dictionary<string, object> toDictionary(){
		Dictionary<string, object> invitation = new Dictionary<string, object>();

		Dictionary<string, object> senderNode = new Dictionary<string, object>();
		senderNode["email"] = sender.getEmail();
		senderNode["id"] = sender.getId();
		senderNode["state"] = "notReady";

		Dictionary<string, object> receiverNode = new Dictionary<string, object>();
		receiverNode["email"] = receiver.getEmail();
		receiverNode["id"] = receiver.getId();
		receiverNode["state"] = "notReady";

		invitation["sender"] = senderNode;
		invitation["receiver"] = receiverNode;

		return invitation;
	}

}
