using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour {

    public Animator lvlAnim;

    private int Tolvl;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void FadeToLvl(int lvlIndex)
    {
        Tolvl = lvlIndex;
        lvlAnim.SetTrigger("FadeOut");
    }

    public void OnFadeComplete()
    {
        SceneManager.LoadScene(Tolvl);
    }
}
