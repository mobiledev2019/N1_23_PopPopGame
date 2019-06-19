using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundController : MonoBehaviour {

    //Var NewMusic: AudioClip; //Pick an audio track to play.

    //void Awake()
    //{
    //    GameObject go = GameObject.Find("Sound");
    //    if(!go.GetComponent<AudioSource>().isPlaying) go.GetComponent<AudioSource>().Play();
    //}


    // Use this for initialization
    void Start () {
        if (PlayerPrefs.GetInt("Sound", 1) == 1)
        {
            AudioListener.volume = 1;
        }
        else
        {
            AudioListener.volume = 0;
        }
        ////Debug.Log(AudioListener.volume);
	}
	
	// Update is called once per frame
	void Update ()
    {

    }
}
