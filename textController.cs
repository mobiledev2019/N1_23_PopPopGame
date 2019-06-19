using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class textController : MonoBehaviour {

    private List<Animator> listAnim;

	// Use this for initialization
	void Start () {
        listAnim = new List<Animator>(GetComponentsInChildren<Animator>());
        StartCoroutine("doAnimator");
    }

    // Update is called once per frame
    void Update () {
		
	}

    IEnumerator doAnimator()
    {
        while (true)
        {
            foreach(Animator anim in listAnim)
            {
                anim.SetTrigger("state");
                yield return new WaitForSeconds(0.25f);
            }
            yield return new WaitForSeconds(0.5f);
        }
    }
}
