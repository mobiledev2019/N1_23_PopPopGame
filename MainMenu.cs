using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements;
using Mono.Data.Sqlite;
using System.Data;
using System;

public class MainMenu : MonoBehaviour {
    public GameObject backgroundRight;
    public GameObject backgroundLeft;
    public GameObject playBtn;
    public GameObject muteBtn, unmuteBtn;
    public GameObject logOutBtn;
    public GameObject logInBtn;
    public GameObject playOnlBtn;
    public Text lifeCounter;
    public GameObject noti;
    public static int playBtnClicked = 0;

    public GameObject purchaseMenu;
    public Button life1;
    public Button life2;
    public Button life3;

    public GameObject levelMenu;
    public GameObject waitingRoom;

    public GameObject[] level;
    public Sprite lockedLevel;
    public Sprite oneStar;
    public Sprite twoStars;
    public Sprite threeStars;
    public Sprite noStar;
    public Image page1;
    public Image page2;
    public int currentPage;

    public int sound;

    private int count;
    public Text endlessHighScore;
    // Use this for initialization
    void Start()
    {
        //PlayerPrefs.DeleteAll();

        if (PlayerPrefs.GetInt("LifeCounter", -1) == -1)
        {
            PlayerPrefs.SetInt("LifeCounter", 3);
        }
        if (PlayerPrefs.GetInt("BombCounter", -1) == -1)
        {
            PlayerPrefs.SetInt("BombCounter", 3);
        }
        if (PlayerPrefs.GetInt("FrozenCounter", -1) == -1)
        {
            PlayerPrefs.SetInt("FrozenCounter", 3);
        }

        lifeCounter.text = PlayerPrefs.GetInt("LifeCounter", 0).ToString();
        waitingRoom.SetActive(false);
        levelMenu.SetActive(true);
        //playOnlBtn.SetActive(true);
        purchaseMenu.SetActive(false);
        currentPage = 1;
        page1.color = new Color(1f, 0.3737f, 0f, 1f);
        page2.color = new Color(1f, 0.3737f, 0f, 0.5f);
        if(PlayerPrefs.GetInt("isLoggedIn", 0) == 0)
        {
            logInBtn.SetActive(true);
            logOutBtn.SetActive(false);
            //playOnlBtn.GetComponent<Button>().interactable = false;
        }
        else
        {
            logInBtn.SetActive(false);
            logOutBtn.SetActive(true);
            //playOnlBtn.GetComponent<Button>().interactable = true;
            string conn = "URI=file:" + "jar:file://" + Application.dataPath + "!/assets/" + "db.s3db";
            IDbConnection dbconn;
            dbconn = (IDbConnection)new SqliteConnection(conn);
            dbconn.Open(); //Open connection to the database.
            IDbCommand dbcmd = dbconn.CreateCommand();
            string sqlQuery = "SELECT score FROM user WHERE username =" + PlayerPrefs.GetString("CurrentUser", "");
            dbcmd.CommandText = sqlQuery;
            IDataReader reader = dbcmd.ExecuteReader();
            while (reader.Read())
            {
                endlessHighScore.text = "Play Endlessn\n"+reader.GetInt32(0);
            }
            reader.Close();
            reader = null;
            dbcmd.Dispose();
            dbcmd = null;
            dbconn.Close();
            dbconn = null;
        }
        for (int i = 0; i < 12; i++)
        {
            checkStar(level[i], i);
            if (level[0].GetComponent<Image>().sprite == lockedLevel)
            {
                level[0].GetComponent<Image>().sprite = noStar;
            }
            if(PlayerPrefs.GetInt("Star" + (i + 1) + "") > 0 && PlayerPrefs.GetInt("Star" + (i + 2) + "", -1) == -1)
            {
                PlayerPrefs.SetInt("Star" + (i + 2), 0);
                level[i + 1].GetComponent<Image>().sprite = noStar;
            }
        }

    }
	// Update is called once per frame
	void Update ()
    {
        sound = PlayerPrefs.GetInt("Sound", 1);
        if (sound == 1)
        {
            muteBtn.SetActive(true);
            unmuteBtn.SetActive(false);
            AudioListener.volume = 1;
        }
        else
        {
            muteBtn.SetActive(false);
            unmuteBtn.SetActive(true);
            AudioListener.volume = 0;
        }
        if (playBtnClicked == 1  && -350 - backgroundLeft.transform.position.x < Screen.width / 2)
        {
            //title.transform.Translate(Vector3.up * 10);
            backgroundLeft.transform.Translate(Vector3.left * 10);
            backgroundRight.transform.Translate(Vector3.right * 10);
            playBtn.SetActive(false);
            playOnlBtn.SetActive(false);
            muteBtn.transform.Translate(Vector3.right * 10);
            unmuteBtn.transform.Translate(Vector3.right * 10);
            logOutBtn.transform.Translate(Vector3.left * 10);
            logInBtn.transform.Translate(Vector3.left * 10);
        }
        lifeCounter.text = PlayerPrefs.GetInt("LifeCounter", 0).ToString();
        if (Input.GetKeyDown(KeyCode.Escape) && playBtnClicked == 1)
        {
            playBtnClicked = 0;
            SceneManager.LoadScene(0);
        }
        if (Input.touchCount > 0 && playBtnClicked == 1 && currentPage == 1 && Input.GetTouch(0).deltaPosition.x < 0)
        {
            currentPage = 2;
            page1.color = new Color(1f, 0.3737f, 0f, 0.5f);
            page2.color = new Color(1f, 0.3737f, 0f, 1f);
            for (int i = 0; i < level.Length; i++)
            {
                level[i].transform.Translate(-1000, 0, 0);
            }
        }
        if (Input.touchCount > 0 && playBtnClicked == 1 && currentPage == 2 && Input.GetTouch(0).deltaPosition.x > 0)
        {
            currentPage = 1;
            page1.color = new Color(1f, 0.3737f, 0f, 1f);
            page2.color = new Color(1f, 0.3737f, 0f, 0.5f);
            for (int i = 0; i < level.Length; i++)
            {
                level[i].transform.Translate(1000, 0, 0);
            }
        }
        // for (int i = 0; i < 9; i++)
        // {
        //     checkStar(level[i], i);
        //     if (level[0].GetComponent<Image>().sprite == lockedLevel)
        //     {
        //         level[0].GetComponent<Image>().sprite = noStar;
        //     }
        //     if(PlayerPrefs.GetInt("Star" + (i + 1) + "") > 0 && PlayerPrefs.GetInt("Star" + (i + 2) + "", -1) == -1)
        //     {
        //         PlayerPrefs.SetInt("Star" + (i + 2), 0);
        //         level[i + 1].GetComponent<Image>().sprite = noStar;
        //     }
        // }
    }
    public void OnApplicationQuit()
    {
        playBtnClicked = 0;
        //DatabaseHandler data = FindObjectOfType<DatabaseHandler>();
        //data.UploadPlayerPref();
    }

    private int CountLevelPlayerpref()
    {
        return DatabaseHandler.levels.Count;
    }

    public void checkStar(GameObject gameObject, int i)
    {
        ////V//
        int t;
        //if(PlayerPrefs.GetInt("isLoggedIn", 0) == 1)
        //{
        //    //Debug.Log("here");
        //    if (i < DatabaseHandler.levels.Count)
        //    {
        //        t = DatabaseHandler.levels[i].getStar();
        //    }
        //    else t = PlayerPrefs.GetInt("Star" + (i + 1) + "", -1);
        //}
        //else
            t = PlayerPrefs.GetInt("Star" + (i+1) + "", -1);
        PlayerPrefs.SetInt("Star" + (i+1) + "", t);
        ///////
        switch (t)
        {
            case -1:
                gameObject.GetComponent<Image>().sprite = lockedLevel;
                break;
            case 0:
                gameObject.GetComponent<Image>().sprite = noStar;
                break;
            case 1:
                gameObject.GetComponent<Image>().sprite = oneStar;
                break;
            case 2:
                gameObject.GetComponent<Image>().sprite = twoStars;
                break;
            case 3:
                gameObject.GetComponent<Image>().sprite = threeStars;
                break;
            default:
                gameObject.GetComponent<Image>().sprite = lockedLevel;
                break;
        }
        
    }
    IEnumerator showNoti()
    {
        noti.SetActive(true);
        yield return new WaitForSeconds(1);
        noti.SetActive(false);
    }
    public void play()
    {
        playBtnClicked = 1;
    }
    public void mute()
    {
        PlayerPrefs.SetInt("Sound", 0);
        ////Debug.Log(PlayerPrefs.GetInt("Sound"));
        AudioListener.volume = 0;
    }
    public void unmute()
    {
        PlayerPrefs.SetInt("Sound", 1);
        ////Debug.Log(PlayerPrefs.GetInt("Sound"));
        AudioListener.volume = 1;
    }
    public void logIn()
    {
        SceneManager.LoadScene(1);
    }
    public void logOut()
    {
        PlayerPrefs.SetString("Email", "");
        PlayerPrefs.SetString("Password", "");
        PlayerPrefs.SetInt("isLoggedIn", 0);
        logInBtn.SetActive(true);
        logOutBtn.SetActive(false);
        //playOnlBtn.GetComponent<Button>().interactable = false;
    }
    public void playOnline()
    {
        waitingRoom.SetActive(true);
        levelMenu.SetActive(false);
    }
    public void openPurchaseMenu()
    {
        if (PlayerPrefs.GetInt("LifeCounter") < 3)
        {
            purchaseMenu.SetActive(true);
        }
        switch (PlayerPrefs.GetInt("LifeCounter"))
        {
            case 2:
                life2.interactable = false;
                life3.interactable = false;
                break;
            case 1:
                life3.interactable = false;
                break;
            default:
                life3.interactable = true;
                life2.interactable = true;
                life1.interactable = true;
                break;
        }
    }
    public void closePurchaseMenu()
    {
        purchaseMenu.SetActive(false);
    }
    public void purchase1life()
    {
        PlayerPrefs.SetInt("LifeCounter", PlayerPrefs.GetInt("LifeCounter") + 1);
        if (PlayerPrefs.GetInt("LifeCounter") == 3)
        {
            PlayerPrefs.SetString("LifeTimer", "Full");
        }
        purchaseMenu.SetActive(false);
    }
    public void purchase2life()
    {
        PlayerPrefs.SetInt("LifeCounter", PlayerPrefs.GetInt("LifeCounter") + 2);
        if (PlayerPrefs.GetInt("LifeCounter") == 3)
        {
            PlayerPrefs.SetString("LifeTimer", "Full");
        }
        purchaseMenu.SetActive(false);
    }
    public void purchase3life()
    {
        PlayerPrefs.SetInt("LifeCounter", PlayerPrefs.GetInt("LifeCounter") + 3);
        PlayerPrefs.SetString("LifeTimer", "Full");
        purchaseMenu.SetActive(false);
    }
    public void level1()
    {
        if(PlayerPrefs.GetInt("LifeCounter", 0) > 0)
        {
            SceneManager.LoadScene(2);

        }
        else
        {
            StartCoroutine(showNoti());
        }
    }
    public void level2()
    {
        if (PlayerPrefs.GetInt("Star2", -1) > -1 && PlayerPrefs.GetInt("LifeCounter", 0) > 0)
        {
            SceneManager.LoadScene(3);

        }
        else
        {
            StartCoroutine(showNoti());
        }
    }
    public void level3()
    {
        if (PlayerPrefs.GetInt("Star3", -1) > -1 && PlayerPrefs.GetInt("LifeCounter", 0) > 0)
        {
            SceneManager.LoadScene(4);

        }
        else
        {
            StartCoroutine(showNoti());
        }
    }
    public void level4()
    {
        if (PlayerPrefs.GetInt("Star4", -1) > -1 && PlayerPrefs.GetInt("LifeCounter", 0) > 0)
        {
            SceneManager.LoadScene(5);

        }
        else
        {
            StartCoroutine(showNoti());
        }
    }
    public void level5()
    {
        if (PlayerPrefs.GetInt("Star5", -1) > -1 && PlayerPrefs.GetInt("LifeCounter", 0) > 0)
        {
            SceneManager.LoadScene(6);

        }
        else
        {
            StartCoroutine(showNoti());
        }
    }
    public void level6()
    {
        if (PlayerPrefs.GetInt("Star6", -1) > -1 && PlayerPrefs.GetInt("LifeCounter", 0) > 0)
        {
            SceneManager.LoadScene(7);

        }
        else
        {
            StartCoroutine(showNoti());
        }
    }
    public void level7()
    {
        if (PlayerPrefs.GetInt("Star7", -1) > -1 && PlayerPrefs.GetInt("LifeCounter", 0) > 0)
        {
            SceneManager.LoadScene(8);

        }
        else
        {
            StartCoroutine(showNoti());
        }
    }
    public void level8()
    {
        if (PlayerPrefs.GetInt("Star8", -1) > -1 && PlayerPrefs.GetInt("LifeCounter", 0) > 0)
        {
            SceneManager.LoadScene(9);

        }
        else
        {
            StartCoroutine(showNoti());
        }
    }
    public void level9()
    {
        if (PlayerPrefs.GetInt("Star9", -1) > -1 && PlayerPrefs.GetInt("LifeCounter", 0) > 0)
        {
            SceneManager.LoadScene(10);

        }
        else
        {
            StartCoroutine(showNoti());
        }
    }
    public void level10()
    {
        if (PlayerPrefs.GetInt("Star10", -1) > -1 && PlayerPrefs.GetInt("LifeCounter", 0) > 0)
        {
            SceneManager.LoadScene(11);

        }
        else
        {
            StartCoroutine(showNoti());
        }
    }
    public void level11()
    {
        if (PlayerPrefs.GetInt("Star11", -1) > -1 && PlayerPrefs.GetInt("LifeCounter", 0) > 0)
        {
            SceneManager.LoadScene(12);

        }
        else
        {
            StartCoroutine(showNoti());
        }
    }
    public void level12()
    {
        if (PlayerPrefs.GetInt("Star12", -1) > -1 && PlayerPrefs.GetInt("LifeCounter", 0) > 0)
        {
            SceneManager.LoadScene(13);

        }
        else
        {
            StartCoroutine(showNoti());
        }
    }
}
