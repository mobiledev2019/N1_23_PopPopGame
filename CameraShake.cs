using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 originalPos = transform.localPosition;

        float time = 0;
        while (time < duration)
        {
            //Debug.Log("abc0");
            float x = Random.Range(-1, 1) * magnitude;
            float y = Random.Range(-1, 1) * magnitude;

            transform.localPosition = new Vector3(x, y, originalPos.z);
            time += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = originalPos;
    }
}
