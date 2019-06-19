using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MultiTouch : MonoBehaviour {

    public GameObject ball, bossBall;
    //public GameObject position;
    //public float time;
    public float speed;
    public GameObject floatingText;
    public GameObject timeTextPrefab;
    public GameObject explosion;
    public GameObject shockwave;

    private int amount;
    private int boss;
    private float timer, startTime;
    private TextMesh timeText;
    private scoreController score;
    private OnlineScoreController onlineScore;

    private bool isDragging;

    private Vector3 lastPos;
    private Vector3 currentPos;

    //public GameObject touchGameObject;

    //private static float comboTime = 0;
    //private static int combo = -1;
    private float t;
    private RotateText rotateText;
    private bool isMissed;
    private bool firstPoint;
    private int countPopStickyBall;

    public ComboInfo comboInfo;

    //public GameObject ballsHolder;

    // Use this for initialization
    void Start () {
        timer = 0;
        startTime = 0;
        score = FindObjectOfType<scoreController>();
        onlineScore = FindObjectOfType<OnlineScoreController>();
        isDragging = false;
        rotateText = FindObjectOfType<RotateText>();
        countPopStickyBall = 0;
        if (!gameObject.CompareTag("Ground") && !gameObject.CompareTag("WallCyan")
            && !gameObject.CompareTag("WallOrange") && !gameObject.CompareTag("Map")
            && !gameObject.CompareTag("plane")) comboInfo = GetComponentInParent<ComboInfo>();
    }
	
	// Update is called once per frame
	void Update () {
        if (timeText != null) timeText.transform.position += getDeltaPosition();

        //pop stickyball
        if(countPopStickyBall >= 3){
            Instantiate(explosion, gameObject.transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        
    }

    private Vector3 getDeltaPosition()
    {
        ////Debug.Log("abc " + ( - firstPos));
        return currentPos - lastPos;
    }
    void OnTouchDown()
    {
        switch (gameObject.tag)
        {
            case "MotherBall":
                //Debug.Log(gameObject.transform.position);
                Destroy(gameObject);
                SpawnChild(gameObject.transform.position);
                //level1Controller.numberOfBalls--;
                break;

            case "ChildBall":
                ////Debug.Log("child");
                if (comboInfo.FirstPoint)
                {
                    //Debug.Log("first point hit");
                    comboInfo.ComboTime = Time.realtimeSinceStartup;
                    comboInfo.FirstPoint = false;
                }
                Combo();
                PointText(comboInfo.Combo);
                //level1Controller.numberOfBalls--;
                Instantiate(explosion, gameObject.transform.position, Quaternion.identity);
                Destroy(gameObject);
                ////Debug.Log("huy");
                break;

			case "ExplosionBall":
                ////Debug.Log("explosion");
				Explosion (5.0f, 2000.0f);
				//level1Controller.numberOfBalls--;

                //slowMotion
                // TimeManager.isSlowmotion = true;

				Instantiate (explosion, gameObject.transform.position, Quaternion.identity);
				Destroy (gameObject);
                GameObject shockwaveClone = Instantiate (shockwave, transform.position, Quaternion.identity);
                Destroy(shockwaveClone, 2f);
				break;

            case "StickyBall":
                countPopStickyBall++;
                GameObject go = Instantiate(floatingText, gameObject.transform.position, Quaternion.identity);
                go.GetComponent<TextMesh>().text = countPopStickyBall.ToString();
                break;

            case "Ground":
            case "WallOrange":
            case "WallCyan":
                //Debug.Log("hit ground");
                comboInfo.Combo = 0;
                comboInfo.IsMissed = true;
                //comboInfo.FirstPoint = true;
                break;
        }
    }

    void OnTouchUp()
    {
        if (gameObject.CompareTag("BossBall"))
        {
            timer = 0;
            if (timeText != null)
                Destroy(timeText.gameObject);
            isDragging = false;
            ////Debug.Log ("UP");
        }
    }

    void OnTouchHold()
    {
        ////Debug.Log ("drag");
        switch (gameObject.tag)
        {
            case "BossBall":
                //generate Time count
                if (!isDragging)
                {
                    startTime = Time.unscaledTime;
                    GameObject go = Instantiate(timeTextPrefab, gameObject.transform.position, Quaternion.identity);
                    timeText = go.GetComponent<TextMesh>();
                    lastPos = currentPos;
                    currentPos = gameObject.transform.position;

                    ShowFloatingText(timeText);
                }
                ////Debug.Log ("DRAG");
                isDragging = true;
                timer = Time.unscaledTime - startTime;
                lastPos = currentPos;
                currentPos = gameObject.transform.position;
                ShowFloatingText(timeText);
                //if time >=0.5f -> pop
                if (timer >=0.5f)
                {
                    Instantiate(explosion, gameObject.transform.position, Quaternion.identity);
                    //subtract number of balls
                    //level1Controller.numberOfBalls--;
                    if (comboInfo.FirstPoint)
                    {
                        comboInfo.ComboTime = Time.realtimeSinceStartup;
                        comboInfo.FirstPoint = false;
                    }
                    Combo();
                    PointText(comboInfo.Combo, true);
                    //yield return new WaitForSeconds(1);
                    //RecourageText();
                    Destroy(gameObject);
                    Destroy(timeText.gameObject);
                    startTime = 0;
                }
                break;


            //not boss ball
            default:
                timer = 0;
                isDragging = false;
                startTime = 0;
                if (timeText != null)
                {
                    Destroy(timeText.gameObject);
                }
                break;
        }

    }

	void Explosion(float radius, float power){
		Vector3 pos = transform.position;
		Collider[] colliders = Physics.OverlapSphere (pos, radius);
		foreach (Collider hit in colliders) {
			Rigidbody rb = hit.GetComponent<Rigidbody> ();
			if (rb != null)
				rb.AddExplosionForce (power, pos, radius);
		}
	}

    void SpawnChild(Vector3 point)
    {
        ////Debug.Log("spawn");
        amount = Random.Range(3, 6);
        boss = Random.Range(0, 4);
        //add number of balls
        //level1Controller.numberOfBalls += amount;

        for (int i = 0; i < amount; i++)
        {
            GameObject g = gameObject.transform.parent.gameObject;
            ////Debug.Log(g + "hello");
            GameObject child = Instantiate(ball, point, Quaternion.identity, g.transform);
            Rigidbody rg = child.GetComponent<Rigidbody>();
            rg.AddForce(Random.insideUnitCircle * speed);
            ////Debug.Log(point);
        }
        if (boss == 0)
        {
            Instantiate(bossBall, point, Quaternion.identity, gameObject.transform);
            //level1Controller.numberOfBalls++;
        }


        Instantiate(explosion, gameObject.transform.position, Quaternion.identity);

    }

    void ShowFloatingText(TextMesh textM)
    {
        textM.text = (Time.unscaledTime - startTime).ToString("F1");
    }

    void RecourageText()
    {
        GameObject go = Instantiate(floatingText, gameObject.transform.position, Quaternion.identity);
        go.GetComponent<TextMesh>().text += " Awesome!";
    }

    void PointText(int i, bool b = false)
    {
        GameObject go = Instantiate(floatingText, gameObject.transform.position, Quaternion.identity);
        if (i >= 1 && !b)
        {
            ////Debug.Log("2");
            go.GetComponent<TextMesh>().text = "+" + (10 + i * 2) + " combo x" + i;
            if (SceneManager.GetActiveScene().buildIndex <= 10) { }
            score.addScore(10 + i * 2);
        }
        else if ((i == 0 || i == -1) && !b)
        {
            ////Debug.Log("3");
            go.GetComponent<TextMesh>().text = "+10";
            score.addScore(10);
        }
        else if (b)
        {
            ////Debug.Log("4");
            go.GetComponent<TextMesh>().text = "+150 Awesome!";
            score.addScore(150);
        }
    }

    void Combo()
    {
        t = Time.realtimeSinceStartup - comboInfo.ComboTime;
        comboInfo.ComboTime = Time.realtimeSinceStartup;
        ////Debug.Log(t);
        if (t < 3 && !comboInfo.IsMissed)
        {
            ////Debug.Log("1");
            comboInfo.Combo += 1;
            comboInfo.IsMissed = false;
            if (comboInfo.Combo == 4)
            {
                rotateText.Cool();
            }
            else if (comboInfo.Combo == 8)
            {
                rotateText.Great();
            }
            else if (comboInfo.Combo == 12 || comboInfo.Combo == 16) rotateText.Excellent();
            //Debug.Log(combo);
        }
        else if(t >= 3)
        {
            comboInfo.Combo = 0;
            comboInfo.IsMissed = true;
            //Debug.Log(combo);
        }
        else if (comboInfo.IsMissed)
        {
            comboInfo.Combo = 0;
            comboInfo.IsMissed = false;
        }
    }

    private void OnCollisionEnter(UnityEngine.Collision collision)
    {
        if (this.CompareTag("MotherBall"))
        {
            if (collision.collider.CompareTag("MotherBall") || collision.collider.CompareTag("ChildBall")
                || collision.collider.CompareTag("BossBall"))
            {

                Destroy(gameObject);
                SpawnChild(collision.contacts[0].point);
                //level1Controller.numberOfBalls--;
            }
        }
    }
}
