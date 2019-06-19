using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour {

    private static DontDestroy instance = null;
    public static DontDestroy Instance
    {
        get { return instance; }
    }
    void Awake()
    {
        if (instance != null && instance != gameObject)
        {
            ////Debug.Log("123456");
            Destroy(gameObject);
            return;
        }
        else
        {
            ////Debug.Log("asfdsdgf");
            instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
