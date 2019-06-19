using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeTimer : MonoBehaviour {
    DateTime dFrom;
    DateTime dTo;
    string timeFrom;
    string sDateFrom;
    string sDateTo = "12:12:00";
    // Use this for initialization
    void Start () {
        timeFrom = PlayerPrefs.GetString("Life");
	}
	

	void FixedUpdate () {
        if (DateTime.TryParse(sDateFrom, out dFrom) && DateTime.TryParse(sDateTo, out dTo))
        {
            TimeSpan TS = dTo - dFrom;
            int hour = TS.Hours;
            int mins = TS.Minutes;
            int secs = TS.Seconds;
            string timeDiff = hour.ToString("00") + ":" + mins.ToString("00") + ":" + secs.ToString("00");
        }
    }
}
