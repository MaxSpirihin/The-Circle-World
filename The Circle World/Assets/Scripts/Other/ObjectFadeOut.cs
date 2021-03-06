﻿using UnityEngine;
using System.Collections;



/// <summary>
/// позволяет объекту плавно исчезнуть растягиванием scale
/// </summary>
public class ObjectFadeOut : MonoBehaviour {

    public float time = 0.5f;


    private Vector3 Source;
    private float timer = 0;
    private bool isPlay = false;


	void Start () {
        Source = transform.localScale;
	}

    /// <summary>
    /// начать исезание
    /// </summary>
    public void FadeOut()
    {
        isPlay = true;
    }

	
	void Update () {
	    if (isPlay)
        {
            timer += Time.deltaTime;

            if (timer > time)
            {
                transform.localScale = new Vector3(0,0,0);
                return;
            }

            transform.localScale = Source*(1-timer/time);
        }
	}
}
