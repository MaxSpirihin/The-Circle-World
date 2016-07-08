using UnityEngine;
using System.Collections;

public class ObjectFadeIn : MonoBehaviour {

    public float time = 0.5f;


    private Vector3 Source;
    private float timer = 0;
    private bool isPlay = false;


	void Start () {
        Source = transform.localScale;
        transform.localScale = new Vector3(0,0,0);
	}


    public void FadeIn()
    {
        isPlay = true;
    }

	
	void Update () {
	    if (isPlay)
        {
            timer += Time.deltaTime;

            if (timer > time)
            {
                transform.localScale = Source;
                return;
            }

            transform.localScale = Source*timer/time;
        }
	}
}
