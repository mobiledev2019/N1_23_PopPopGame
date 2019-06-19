using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Collision : MonoBehaviour {

    public float force;
    public float maxForce;
	public GameObject ripple;

	public GameObject explosion;
    
    public GameObject floatingText;

    private Rigidbody rg;
    private scoreController score;

    // Use this for initialization
    void Start()
    {
        rg = GetComponent<Rigidbody>();
        score = FindObjectOfType<scoreController>();
    }
	// Update is called once per frame
	void Update () {

	}

    private void OnCollisionEnter(UnityEngine.Collision collision)
    {
        if (!collision.collider.CompareTag("Entrance")) {
            Vector3 a = rg.velocity;
            a.x *= force;
            a.y *= force;
            rg.velocity = Vector3.ClampMagnitude(a, maxForce);
        }
        else
        {
            GameObject go = Instantiate(floatingText, gameObject.transform.position, Quaternion.identity);
            go.GetComponent<TextMesh>().text = "-10";
            score.subScore();
            Destroy(gameObject);
			//subtract number of balls
			Instantiate (explosion, gameObject.transform.position, Quaternion.identity);
        }

		Vector3 p = collision.contacts [0].point;

        switch(collision.collider.tag){
            case "WallOrange":
			AddRipple (p, new Color(255, 164, 0));
            break;

            case "WallCyan":
			AddRipple (p, new Color(0, 202, 255));
            break;

            case "MovingBlock":
            AddRipple(p, new Color(189, 0, 255), collision.collider.gameObject.transform);
            break;

            case "StickyBall":
            // StartCoroutine(StickToBall(collision.collider.gameObject));
            FixedJoint joint = gameObject.AddComponent<FixedJoint>();
            joint.connectedBody = collision.collider.gameObject.GetComponent<Rigidbody>();
            // Time.timeScale = 0.1f;
            break;

        }


    }

	private void AddRipple(Vector3 p, Color color){
		GameObject rippleClone = Instantiate (ripple, p, transform.rotation);
		ParticleSystem ps = rippleClone.GetComponent<ParticleSystem> ();
		ps.startColor = color;

		Destroy (rippleClone, 1.5f);
	}

    private void AddRipple(Vector3 p, Color color, Transform parent)
    {
        GameObject rippleClone = Instantiate(ripple, p, transform.rotation, parent);
        ParticleSystem ps = rippleClone.GetComponent<ParticleSystem>();
        ps.startColor = color;

        Destroy(rippleClone, 1.5f);
    }

    // IEnumerator StickToBall(GameObject stickyBall){
        
    //     yield return new WaitForSeconds(0.001f);

    //     FixedJoint joint = gameObject.AddComponent<FixedJoint>();
    //     joint.connectedBody = stickyBall.GetComponent<Rigidbody>();
    // }

}
