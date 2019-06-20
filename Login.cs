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

    // Use this for initialization
    void Start () {
        string conn = "URI=file:" + Application.dataPath + "/StreamingAssets/" + "db.s3db";
        IDbConnection dbconn;
        dbconn = (IDbConnection)new SqliteConnection(conn);
        dbconn.Open(); //Open connection to the database.
        IDbCommand dbcmd = dbconn.CreateCommand();
        string sqlQuery = "SELECT * FROM user";
        dbcmd.CommandText = sqlQuery;
        IDataReader reader = dbcmd.ExecuteReader();
        while (reader.Read())
        {
            Debug.Log(reader.GetString(0) + reader.GetString(1) + reader.GetInt32(2));
        }
        reader.Close();
        reader = null;
        dbcmd.Dispose();
        dbcmd = null;
        dbconn.Close();
        dbconn = null;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void loginBtnClicked()
    {
        //if (checkUsernamePass(login_email.text, login_password.text))
        //{
        //    PlayerPrefs.SetInt("isLoggedIn", 1);
        //    SceneManager.LoadScene(0);
        //}
        //else 
        if (login_email.text.Equals(PlayerPrefs.GetString("User")))
        {
            PlayerPrefs.SetInt("isLoggedIn", 1);
            SceneManager.LoadScene(0);
        }
    }
    public bool checkUsernamePass(String myUsername, String myPass)
    {
        string conn = "URI=file:" + Application.streamingAssetsPath + "/db.s3db";
        IDbConnection dbconn;
        dbconn = (IDbConnection)new SqliteConnection(conn);
        Debug.Log("Connected to DB");
        dbconn.Open(); //Open connection to the database.
        IDbCommand dbcmd = dbconn.CreateCommand();
        string sqlQuery = "SELECT username, pass FROM user";
        dbcmd.CommandText = sqlQuery;
        IDataReader reader = dbcmd.ExecuteReader();
        while (reader.Read())
        {
            string username = reader.GetString(0);
            string pass = reader.GetString(1);

            if (username.Equals(myUsername) && pass.Equals(myPass))
            {
                reader.Close();
                reader = null;
                dbcmd.Dispose();
                dbcmd = null;
                dbconn.Close();
                dbconn = null;
                PlayerPrefs.SetString("CurrentUser", myUsername);
                return true;
            }
        }
        reader.Close();
        reader = null;
        dbcmd.Dispose();
        dbcmd = null;
        dbconn.Close();
        dbconn = null;
        return false;
    }
}
