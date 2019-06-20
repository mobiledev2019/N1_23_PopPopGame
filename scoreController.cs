using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scoreController : MonoBehaviour {
    public int maxScore;
    public Slider scoreBar;
    public GameObject star1, star2, star3;

    public static int currentScore;

	// Use this for initialization
	void Start () {
        maxScore = 150;
        currentScore = 0;
        showScore();

    }
	
	// Update is called once per frame
	void Update ()
    {

    }

    public void addScore(int i)
    {
        currentScore += i;
        showScore();
    }

    public void subScore()
    {
        if(SceneManager.GetActiveScene().buildIndex == 14)
        {
            levelController.endlessLife--;
        }
        currentScore -= 10;
        showScore();
    }
    public void showScore()
    {
        this.GetComponent<Text>().text ="" + currentScore;
        scoreBar.value = (float)currentScore / maxScore;
        if (currentScore >= 50)
        {
            star1.GetComponent<Image>().color = Color.white;
        }
        else
        {
            star1.GetComponent<Image>().color = Color.gray;
        };
        if(currentScore >= 100)
        {
            star2.GetComponent<Image>().color = Color.white;
        }
        else
        {
            star2.GetComponent<Image>().color = Color.gray;
        };
        if (currentScore >= maxScore)
        {
            star3.GetComponent<Image>().color = Color.white;
        }
        else
        {
            star3.GetComponent<Image>().color = Color.gray;
        };

    }
}
