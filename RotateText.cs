using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RotateText : MonoBehaviour {

    public List<AudioClip> listAudio;

    private AudioSource source;

    float t;
    Quaternion initial;
    Quaternion target;
    Animator anim;

    public void Start()
    {
        initial = Quaternion.Euler(0, 0, -10);
        target = Quaternion.Euler(0, 0, 10);
        if (SceneManager.GetActiveScene().buildIndex <= 10)
        {
            gameObject.GetComponent<TextMesh>().text = "Level " + (SceneManager.GetActiveScene().buildIndex - 1);
        }
        else
        {
            gameObject.GetComponent<TextMesh>().text = "Ready";
        }
        anim = gameObject.GetComponent<Animator>();
        source = GetComponent<AudioSource>();
        StartCoroutine(Wait());
    }

    public void Update()
    {
        t += Time.deltaTime;
        transform.rotation = Quaternion.Lerp(initial, target, t);
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1);
        gameObject.GetComponent<TextMesh>().text = "Start";
        anim.Play("cool", -1, 0);

    }
    public void Cool()
    {
        ////Debug.Log("why dont");
        gameObject.GetComponent<TextMesh>().text = "Cool";
        anim.Play("cool", -1, 0);
        source.clip = listAudio[0];
        source.volume = 10f;
        source.Play();
    }

    public void Great()
    {
        gameObject.GetComponent<TextMesh>().text = "Great";
        anim.Play("great", -1, 0);
        source.clip = listAudio[1];
        source.volume = 10f;
        source.Play();
    }

    public void Excellent()
    {
        gameObject.GetComponent<TextMesh>().text = "Excellent";
        anim.Play("excellent", -1, 0);
        source.clip = listAudio[2];
        source.volume = 10f;
        source.Play();
    }
}
