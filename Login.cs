using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Login : MonoBehaviour{
    public InputField login_email;
    public InputField login_password;
    public Text endlessButton;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void LoginDB()
    {
        string conn = "URI=file:" + "jar:file://" + Application.dataPath + "!/assets/" + "db.s3db";
        IDbConnection dbconn;
        dbconn = (IDbConnection)new SqliteConnection(conn);
        dbconn.Open(); //Open connection to the database.
        IDbCommand dbcmd = dbconn.CreateCommand();
        string sqlQuery = "SELECT username, pass FROM user WHERE username =" + login_email.text + " AND pass =" + login_password.text;
        dbcmd.CommandText = sqlQuery;
        IDataReader reader = dbcmd.ExecuteReader();
        while (reader.Read())
        {
            PlayerPrefs.SetString("CurrentUser", reader.GetString(0));
            PlayerPrefs.SetInt("isLoggedIn", 1);
            SceneManager.LoadScene(0);
        }
        reader.Close();
        reader = null;
        dbcmd.Dispose();
        dbcmd = null;
        dbconn.Close();
        dbconn = null;
    }
}
