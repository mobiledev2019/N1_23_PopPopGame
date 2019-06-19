using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Unity.Editor;
using Firebase.Database;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LobbyHandler : MonoBehaviour {

	public Button btnNotReadySender;
	public Button btnReadySender;
	public Button btnNotReadyReceiver;
	public Button btnReadyReceiver;
	public Text emailSender;
	public Text emailReceiver;
	public Button btnPlay;
    public Button[] mapBtn;
    public int selectedMap = 0;

	private DatabaseReference reference;
	private GameObject roomHandlerObject;
	private Room roomTemp;
	private bool isReady;

	// Use this for initialization
	void Start () {
		isReady = false;
		btnPlay.interactable = false;

		FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://poppop-31e9c.firebaseio.com/");
        reference = FirebaseDatabase.DefaultInstance.RootReference;

		roomHandlerObject = GameObject.Find("RoomHandler");
		roomTemp = roomHandlerObject.GetComponent<RoomHandler>().getRoom();


		//LISTEN TO USER WHO HAS JUST CLICK READY/NOTREADY
		reference.Child("rooms").Child(roomTemp.getId()).Child("pair").ValueChanged += HandleValueChangedPairInRooms;
		//LEAVE ROOM
		reference.Child("rooms").ChildRemoved += HandleChildRemovedRooms;
		//MAP CHANGE / ROOM'S STATE
		reference.Child("rooms").Child(roomTemp.getId()).ValueChanged += HandleValueChangedMaps;
		//

		
		emailSender.text = roomTemp.getPair().getSender().getEmail();
		emailReceiver.text = roomTemp.getPair().getReceiver().getEmail();

        //ONLY SENDER CAN SELECT MAP, CHECK IF THIS IS A SENDER
        if (PlayerPrefs.GetString("Email") == roomTemp.getPair().getReceiver().getEmail())
        {
            for (int i = 0; i < 4; i++)
            {
                mapBtn[i].interactable = false;
            }
        }

        //ONLY SENDER CAN START THE GAME
        if (roomTemp.getPair().getSender().getEmail() == PlayerPrefs.GetString("Email")){
			btnPlay.gameObject.SetActive(true);
			btnPlay.interactable = false;
			// Debug.Log(btnPlay.IsInteractable());
		}
		else{
			btnPlay.gameObject.SetActive(false);
		}
	}
	
	// Update is called once per frame
	void Update () {
		isReady = (btnReadyReceiver.IsActive() && btnReadySender.IsActive() && selectedMap != 0)? true : false;

		if(isReady){
			btnPlay.interactable = true;
		}
		else{
			btnPlay.interactable = false;
		}

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            leaveLobby();
        }
    }

    //SENDER SELECTS A MAP
    public void mapSelected(Button button)
    {
        if (PlayerPrefs.GetString("Email") == roomTemp.getPair().getSender().getEmail())
        {
            for (int i = 0; i < 4; i++)
            {
                mapBtn[i].GetComponent<Image>().color = Color.white;
            }
            button.GetComponent<Image>().color = Color.gray;
            selectedMap = int.Parse(button.GetComponentInChildren<Text>().text);

			//ADD NODE MAP TO FIREBASE
			Dictionary<string, object> update = new Dictionary<string,object>();
			update["/rooms/"+roomTemp.getId()+"/map"] = selectedMap;

			reference.UpdateChildrenAsync(update);
        }
    }
	void OnDestroy()
	{
		
	}
	
	private void HandleValueChangedMaps(object sender, ValueChangedEventArgs arg){
		if(arg.DatabaseError!=null){
			return;
		}
		//MAP CHANGED
		selectedMap = int.Parse(arg.Snapshot.Child("map").Value.ToString());
		if(roomTemp.getPair().getReceiver().getEmail() == PlayerPrefs.GetString("Email")){
			//clear
			for (int i = 0; i < 4; i++)
            {
                mapBtn[i].GetComponent<Image>().color = Color.white;
            }
			mapBtn[selectedMap-1].GetComponent<Image>().color = Color.gray;
		}
		//ROOM'S STATE
		if(arg.Snapshot.Child("state").Value.ToString() == "playing"){
			SceneManager.LoadScene(selectedMap+11);
			Debug.Log("PLAYING");
		}
	}

	private void HandleChildRemovedRooms(object sender, ChildChangedEventArgs arg){
		if(arg.DatabaseError!= null){
			return;
		}

		// Debug.Log(arg.Snapshot.Key);
		//if the room which contains this player is removed (the other player leaved)
		if(arg.Snapshot.Child("pair").Child("sender").Child("email").Value.ToString() == PlayerPrefs.GetString("Email") ||
			arg.Snapshot.Child("pair").Child("receiver").Child("email").Value.ToString() == PlayerPrefs.GetString("Email")){
				SceneManager.LoadScene(0);
				MainMenu.playBtnClicked = 0;
				Destroy(roomHandlerObject);
			}
	}

	private void HandleValueChangedPairInRooms(object sender, ValueChangedEventArgs arg){
		if(arg.DatabaseError!= null){
            return;
        }

		foreach(DataSnapshot user in arg.Snapshot.Children){
			//JUST 						VERIFIY ROOM(SENDER , RECEIVER)
			if(user.Key == "receiver" && roomTemp.getPair().getReceiver().getEmail() == user.Child("email").Value.ToString()){
			//RECEIVER READY
				if(user.Child("state").Value.ToString() == "ready"){

					btnNotReadyReceiver.gameObject.SetActive(false);
					btnReadyReceiver.gameObject.SetActive(true);
					// Debug.Log("receiver ready");
				}
				//RECEIVER NOT READY
				else{
					btnNotReadyReceiver.gameObject.SetActive(true);
					btnReadyReceiver.gameObject.SetActive(false);
					// Debug.Log("receiver notready");
				}
			}
			else if(user.Key == "sender" && roomTemp.getPair().getSender().getEmail() == user.Child("email").Value.ToString()){
				//SENDER READY
				if(user.Child("state").Value.ToString() == "ready"){
					btnNotReadySender.gameObject.SetActive(false);
					btnReadySender.gameObject.SetActive(true);
					// Debug.Log("sender ready");
				}
				//SENDER NOT READY
				else{
					btnNotReadySender.gameObject.SetActive(true);
					btnReadySender.gameObject.SetActive(false);
					// Debug.Log("sender notready");
				}
			}
		}
		
	}

	public void btnNotReadySenderClick(){

		//IF THIS PLAYER IS SENDER
		if(PlayerPrefs.GetString("Email") == roomTemp.getPair().getSender().getEmail()){
			reference.Child("rooms").Child(roomTemp.getId()).Child("pair").Child("sender").Child("state").SetValueAsync("ready");
		}
	}

	public void btnReadySenderClick(){
		
		//IF THIS PLAYER IS SENDER
		if(PlayerPrefs.GetString("Email") == roomTemp.getPair().getSender().getEmail()){
			reference.Child("rooms").Child(roomTemp.getId()).Child("pair").Child("sender").Child("state").SetValueAsync("notReady");
		}
	}

	public void btnNotReadyReceiverClick(){

		//IF THIS PLAYER IS RECEIVER
		if(PlayerPrefs.GetString("Email") == roomTemp.getPair().getReceiver().getEmail()){
			reference.Child("rooms").Child(roomTemp.getId()).Child("pair").Child("receiver").Child("state").SetValueAsync("ready");
		}
	}

	public void btnReadyReceiverClick(){

		//IF THIS PLAYER IS RECEIVER
		if(PlayerPrefs.GetString("Email") == roomTemp.getPair().getReceiver().getEmail()){
			reference.Child("rooms").Child(roomTemp.getId()).Child("pair").Child("receiver").Child("state").SetValueAsync("notReady");
		}
	}	
    public void leaveLobby()
    {
        SceneManager.LoadScene(0);
        MainMenu.playBtnClicked = 0;
        // Debug.Log(readyCount);
        reference.Child("rooms").Child(roomTemp.getId()).RemoveValueAsync();
    }

	public void btnPlayClick(){
		roomHandlerObject.GetComponent<RoomHandler>().getRoom().setMapIndex(selectedMap);
		reference.Child("rooms").Child(roomTemp.getId()).Child("state").SetValueAsync("playing");
	}
}
