using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using Mono.Data.Sqlite;
using System.Data;

public class levelController : MonoBehaviour {
    public GameObject pauseBtn;
    public GameObject resumeBtn;
    public GameObject quitBtn;
    public GameObject restartBtn;
    public GameObject bombItem;
    public GameObject frozenItem;
    public GameObject frostScreen;
    public Text frozenTimer;
    public Text bombCounter;
    public Text frozenCounter;
    public GameObject bomb;

    public GameObject bombPurchaseMenu;
    public GameObject frozenPurchaseMenu;

    public GameObject noti;
    public Vector3 touchPos;
    public int selectingItemPos;

    public Button nextLevelBtn;
    public Text score;
    public GameObject star1, star2, star3;
	public GameObject star11, star22, star33;
    public GameObject gameOverMenu;

    public GameObject ballsHolder;
	public int numberOfBalls = 7;

	public GameObject scoreShow;
	private Animator animScore;
    public Text lifeCounter;
    public GameObject reward;
    public GameObject lifeReward;
    public GameObject bombReward;
    public GameObject frozenReward;

    public static int endlessLife;
    public GameObject endlessLife1;
    public GameObject endlessLife2;
    public GameObject endlessLife3;

    private int t = 0;

    public Text notbad;

    public GameObject plane;
    
    public static bool isGameOver;
    public bool isPaused = false;

    public LevelChanger lvlChanger;


	// Use this for initialization
	void Start ()
    {
        numberOfBalls = ballsHolder.transform.childCount + 1;
        pauseBtn.SetActive(true);
        bombItem.SetActive(true);
        frozenItem.SetActive(true);
        resumeBtn.SetActive(false);
        quitBtn.SetActive(false);
        restartBtn.SetActive(false);
        gameOverMenu.SetActive(false);
        reward.SetActive(false);
        frostScreen.SetActive(false);
        frozenTimer.text = "";

        selectingItemPos = 0;
        touchPos = new Vector2(0, 0);

        star1.GetComponent<Image> ().color = Color.gray;
		star2.GetComponent<Image> ().color = Color.gray;
		star3.GetComponent<Image> ().color = Color.gray;

		animScore = scoreShow.GetComponent<Animator> ();
		animScore.SetInteger ("score", 0);
        isGameOver = false;

        endlessLife = 3;
    }
	
	// Update is called once per frame
	void Update () {
        if (SceneManager.GetActiveScene().buildIndex != 14)
        {
            if (numberOfBalls > 0)
            {
                numberOfBalls = ballsHolder.transform.childCount;
                //            ////Debug.Log(numberOfBalls);
            }
            
            //game over
            if (numberOfBalls == 0)
            {
                numberOfBalls = -1;
                gameOver();
                isGameOver = true;
                ////Debug.Log("GameOver");
            }
            //score board
            if (numberOfBalls == -1)
            {
                if (t < scoreController.currentScore)
                {
                    t++;
                }
                score.text = "Score: " + t;
            }
        }
        else
        {
            if (endlessLife == 2)
            {
                endlessLife3.SetActive(false);
            }
            if (endlessLife == 1)
            {
                endlessLife3.SetActive(false);
                endlessLife2.SetActive(false);
            }
            if (endlessLife == 0)
            {
                endlessLife3.SetActive(false);
                endlessLife2.SetActive(false);
                endlessLife1.SetActive(false);
                endlessGameOver();
            }
        }
        bombCounter.text = PlayerPrefs.GetInt("BombCounter").ToString();
        frozenCounter.text = PlayerPrefs.GetInt("FrozenCounter").ToString();
        if (selectingItemPos == 1)
        {
            bombItem.GetComponent<Button>().interactable = false;
            if (Input.GetButtonDown("Fire1"))
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit))
                {
                    touchPos = hit.point;
                }
                //touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                selectingItemPos = 0;
                Debug.Log(touchPos);
                GameObject newBomb = Instantiate(bomb, new Vector3(touchPos.x, touchPos.y, -2f), Quaternion.identity);
                Destroy(newBomb, 0.2f);
                touchPos = new Vector3(0, 0, 0);
                bombItem.GetComponent<Button>().interactable = true;
                noti.SetActive(false);
            }

        }

        if (Input.GetKey(KeyCode.Escape) && pauseBtn.activeSelf == true)
        {
            pause();
        }
        if (Input.GetKey(KeyCode.Escape) && resumeBtn.activeSelf == true)
        {
            resume();
        }
        if (Input.GetKey(KeyCode.Escape) && gameOverMenu.activeSelf == true)
        {
            quit();
        }
        if (isPaused && Time.timeScale != 0)
        {
            pause();
        }
    }
    IEnumerator FrozenItemActive()
    {
        frozenItem.GetComponent<Button>().interactable = false;
        frostScreen.SetActive(true);
        Time.timeScale = 0.1f;
        for(int i = 5; i > 0; i--)
        {
            frozenTimer.text = i.ToString();
            yield return new WaitForSeconds(0.1f);
        }
        if (pauseBtn.activeSelf)
        {
            frozenItem.GetComponent<Button>().interactable = true;
            frostScreen.SetActive(false);
            Time.timeScale = 1;
            frozenTimer.text = "";
        }
    }
    
    public void endlessGameOver()
    {
        if (t < scoreController.currentScore)
        {
            t++;
        }
        if (t > scoreController.currentScore)
        {
            t--;
        }
        score.text = "Score: " + t;
        gameOverMenu.SetActive(true);
        pauseBtn.SetActive(false);
        bombItem.SetActive(false);
        frozenItem.SetActive(false);
        frostScreen.SetActive(false);
        Time.timeScale = 0;
        int highScore = PlayerPrefs.GetInt("HighScore", 0);
        if (scoreController.currentScore > highScore)
        {
            PlayerPrefs.SetInt("HighScore", scoreController.currentScore);
        }
    }
    public void gameOver()
    {
        gameOverMenu.SetActive(true);
        pauseBtn.SetActive(false);
        bombItem.SetActive(false);
        frozenItem.SetActive(false);
        frostScreen.SetActive(false);
        Time.timeScale = 1;
        //if(DatabaseHandler.levels != null)
        //{
        //    //If its new level -> Add to Level list -> not null
        //    if (SceneManager.GetActiveScene().buildIndex - 2 == DatabaseHandler.levels.Count)
        //        DatabaseHandler.levels.Add(new Level(SceneManager.GetActiveScene().buildIndex - 2, 0));
        //}

        //int t = DatabaseHandler.levels[SceneManager.GetActiveScene().buildIndex - 2].getStar();
        int t = PlayerPrefs.GetInt("Star" + (SceneManager.GetActiveScene().buildIndex - 1), -1);
        int sc = scoreController.currentScore;

        if (sc < 50) {
            notbad.text = "Try more";
            PlayerPrefs.SetInt("LifeCounter", PlayerPrefs.GetInt("LifeCounter") - 1);
            if (PlayerPrefs.GetInt("LifeCounter") == 2)
            {
                PlayerPrefs.SetString("LifeTimer", DateTime.Now.ToString("h:mm:ss"));
            }
            nextLevelBtn.interactable = false;
			star11.SetActive (false);
			star22.SetActive (false);
			star33.SetActive (false);
            if (t < 0)
            {
                //DatabaseHandler.levels[SceneManager.GetActiveScene().buildIndex - 2].setStar(0);
                PlayerPrefs.SetInt("Star" + (SceneManager.GetActiveScene().buildIndex - 1), 0);
            }
		} 
		else {
			//50 -> 100
			if (sc < 100) {
                notbad.text = "Not bad";
				nextLevelBtn.interactable = true;

				star22.SetActive (false);
				star33.SetActive (false);

                animScore.SetInteger("score", 1);
                if (t < 1)
                {
                    //DatabaseHandler.levels[SceneManager.GetActiveScene().buildIndex - 2].setStar(1);
                    PlayerPrefs.SetInt("Star" + (SceneManager.GetActiveScene().buildIndex - 1), 1);
                }
			} 
			else {
				//100 -> 150
				if (sc < 150) {
                    notbad.text = "Good";
					star33.SetActive (false);

                    animScore.SetInteger("score", 2);
                    if (t < 2)
                    {
                        //DatabaseHandler.levels[SceneManager.GetActiveScene().buildIndex - 2].setStar(2);
                        PlayerPrefs.SetInt("Star" + (SceneManager.GetActiveScene().buildIndex - 1), 2);
                    }
				} 
				else {
                    // >150
                    notbad.text = "Impressive";
                    animScore.SetInteger("score", 3);
                    if (PlayerPrefs.GetInt("LifeCounter") < 3)
                    {
                        PlayerPrefs.SetInt("LifeCounter", PlayerPrefs.GetInt("LifeCounter") + 1);
                        if (PlayerPrefs.GetInt("LifeCounter") == 3)
                        {
                            PlayerPrefs.SetString("LifeTimer", "Full");
                        }
                        reward.SetActive(true);
                        lifeReward.SetActive(true);
                    }
                    else
                    {
                        lifeReward.SetActive(false);
                    }
                    if (t < 3)
                    {
                        //DatabaseHandler.levels[SceneManager.GetActiveScene().buildIndex - 2].setStar(3);
                        PlayerPrefs.SetInt("Star" + (SceneManager.GetActiveScene().buildIndex - 1), 3);
                        if (sc > 300)
                        {
                            PlayerPrefs.SetInt("BombCounter", PlayerPrefs.GetInt("BombCounter") + 1);
                            PlayerPrefs.SetInt("FrozenCounter", PlayerPrefs.GetInt("FrozenCounter") + 1);
                            reward.SetActive(true);
                            bombReward.SetActive(true);
                            frozenReward.SetActive(true);
                        }
                    }
                    else
                    {
                        bombReward.SetActive(false);
                        frozenReward.SetActive(false);
                    }
				}
			}
        }
        lifeCounter.text = PlayerPrefs.GetInt("LifeCounter", 0).ToString();
	}
    public void useBomb()
    {
        if (PlayerPrefs.GetInt("BombCounter") == 0)
        {
            Time.timeScale = 0;
            bombPurchaseMenu.SetActive(true);
            pauseBtn.SetActive(false);
        }
        else
        {
            selectingItemPos = 1;
            noti.SetActive(true);
            PlayerPrefs.SetInt("BombCounter", PlayerPrefs.GetInt("BombCounter") - 1);

        }
    }
    public void useFrozen()
    {
        if (PlayerPrefs.GetInt("FrozenCounter") == 0)
        {
            Time.timeScale = 0;
            frozenPurchaseMenu.SetActive(true);
            pauseBtn.SetActive(false);
        }
        else
        {
            PlayerPrefs.SetInt("FrozenCounter", PlayerPrefs.GetInt("FrozenCounter") - 1);
            StartCoroutine(FrozenItemActive());
        }
    }
    public void closePurchaseMenu()
    {
        if (bombPurchaseMenu.activeSelf)
        {
            bombPurchaseMenu.SetActive(false);
        }
        if (frozenPurchaseMenu.activeSelf)
        {
            frozenPurchaseMenu.SetActive(false);
        }
        Time.timeScale = 1;
        pauseBtn.SetActive(true);
    }
    public void purchase1bomb()
    {
        PlayerPrefs.SetInt("BombCounter", PlayerPrefs.GetInt("BombCounter") + 1);
        closePurchaseMenu();
    }
    public void purchase2bomb()
    {
        PlayerPrefs.SetInt("BombCounter", PlayerPrefs.GetInt("BombCounter") + 2);
        closePurchaseMenu();
    }
    public void purchase3bomb()
    {
        PlayerPrefs.SetInt("BombCounter", PlayerPrefs.GetInt("BombCounter") + 3);
        closePurchaseMenu();
    }
    public void purchase1frozen()
    {
        PlayerPrefs.SetInt("FrozenCounter", PlayerPrefs.GetInt("FrozenCounter") + 1);
        closePurchaseMenu();
    }
    public void purchase2frozen()
    {
        PlayerPrefs.SetInt("FrozenCounter", PlayerPrefs.GetInt("FrozenCounter") + 2);
        closePurchaseMenu();
    }
    public void purchase3frozen()
    {
        PlayerPrefs.SetInt("FrozenCounter", PlayerPrefs.GetInt("FrozenCounter") + 3);
        closePurchaseMenu();
    }
    public void nextLevel()
    {
        isGameOver = false;
        //SceneManager.LoadScene(1 + SceneManager.GetActiveScene().buildIndex);
        lvlChanger.FadeToLvl(1 + SceneManager.GetActiveScene().buildIndex);
    }
    public void OnApplicationPause()
    {
        //pause();
    }
    public void pause()
    {
        pauseBtn.SetActive(false);
        bombItem.SetActive(false);
        frozenItem.SetActive(false);
        resumeBtn.SetActive(true);
        quitBtn.SetActive(true);
        restartBtn.SetActive(true);
        Time.timeScale = 0;
        Debug.Log("pause");
        plane.SetActive(true);
    }
    public void resume()
    {
        Time.timeScale = 1;
        pauseBtn.SetActive(true);
        bombItem.SetActive(true);
        frozenItem.SetActive(true);
        resumeBtn.SetActive(false);
        quitBtn.SetActive(false);
        restartBtn.SetActive(false);
        plane.SetActive(false);
    }
    public void quit()
    {
        isGameOver = false;
        Time.timeScale = 1;
        if (!gameOverMenu.activeSelf)
        {
            PlayerPrefs.SetInt("LifeCounter", PlayerPrefs.GetInt("LifeCounter") - 1);
            Debug.Log(PlayerPrefs.GetInt("LifeCounter"));
            if (PlayerPrefs.GetInt("LifeCounter") == 2)
            {
                PlayerPrefs.SetString("LifeTimer", DateTime.Now.ToString("h:mm:ss"));
            }
        }
        //lvlChanger.FadeToLvl(0);
        SceneManager.LoadScene(0);
    }
    public void restart()
    {
        isGameOver = false;
        Time.timeScale = 1;
        PlayerPrefs.SetInt("LifeCounter", PlayerPrefs.GetInt("LifeCounter") - 1);
        if (PlayerPrefs.GetInt("LifeCounter") == 2)
        {
            PlayerPrefs.SetString("LifeTimer", DateTime.Now.ToString("h:mm:ss"));
        }

        if (PlayerPrefs.GetInt("LifeCounter") == 0)
        {
            lvlChanger.FadeToLvl(0);
        }
        else
        {
            numberOfBalls = ballsHolder.transform.childCount;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        
    }
    public void quitEndless()
    {
        isGameOver = false;
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
    public void restartEndless()
    {
        isGameOver = false;
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    void OnApplicationFocus(bool hasFocus)
    {
        isPaused = !hasFocus;
    }

    void OnApplicationPause(bool pauseStatus)
    {
        isPaused = pauseStatus;
    }
    private void OnApplicationQuit()
    {
        MainMenu.playBtnClicked = 0;
        if (!isGameOver)
        {
            PlayerPrefs.SetInt("LifeCounter", PlayerPrefs.GetInt("LifeCounter") - 1);
            if (PlayerPrefs.GetInt("LifeCounter") == 2)
            {
                PlayerPrefs.SetString("LifeTimer", DateTime.Now.ToString("h:mm:ss"));
            }
        }
    }
}
