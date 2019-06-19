using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboInfo : MonoBehaviour {
    private float _comboTime;

    public float ComboTime
    {
        get { return _comboTime; }
        set { _comboTime = value; }
    }

    private int _combo;

    public int Combo
    {
        get { return _combo; }
        set { _combo = value; }
    }

    private bool _isMissed;

    public bool IsMissed
    {
        get { return _isMissed; }
        set { _isMissed = value; }
    }

    private bool _firstPoint;

    public bool FirstPoint
    {
        get { return _firstPoint; }
        set { _firstPoint = value; }
    }




    // Use this for initialization
    void Start () {
        _combo = -1;
        _comboTime = 0;
        _isMissed = false;
        _firstPoint = true;

        Debug.Log("comboinfo start");
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
