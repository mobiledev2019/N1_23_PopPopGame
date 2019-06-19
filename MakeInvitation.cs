using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;

public class MakeInvitation : MonoBehaviour {

	public Text receiverInfo;
	public GameObject roomHandlerObject;

	private PairInvitation tempPair;
	
	private DatabaseReference reference;

	// Use this for initialization
	void Start () {
		reference = FirebaseDatabase.DefaultInstance.RootReference;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//TODO : receive text from button then reference to VVV receiver VVV...
    public void onInvitationClick(){
		
        string key = reference.Push().Key.ToString();
		// Debug.Log(key);
		User sender = new User(PlayerPrefs.GetInt("Id"), "", PlayerPrefs.GetString("Email"),"");
		User receiver = splitInfoUser(receiverInfo.text);

		tempPair = new PairInvitation(sender, receiver);

        // Dictionary<string, object> entryValue = new Dictionary<string, object>();
        // entryValue["/sender/email"] = PlayerPrefs.GetString("Email");
		// entryValue
        // entryValue["receiver"] = receiverInfo.text;

        Dictionary<string, object> update = new Dictionary<string, object>();
        update["/invitations/"+key] = tempPair.toDictionary();

        reference.UpdateChildrenAsync(update);

    }

	private User splitInfoUser(string str){
		string[] cutStr = str.Split('/');
		User u = new User(int.Parse(cutStr[0]), "", cutStr[1], "");
		
		return u;
	}
}
