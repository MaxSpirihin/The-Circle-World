using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;


/// <summary>
/// выполняет респавн
/// </summary>
public class Respawner : MonoBehaviour {

    //Singleton
    private static Respawner _instance;
    private static Respawner Instance
    {
        get
        {
            //Если объекта еще нет, находим его на сцене
            if (_instance == null)
                _instance = GameObject.FindObjectOfType<Respawner>();
            return _instance;
        }
    }


    public Animator BlackScreen;
    public float delay = 0.5f;//задержка

    private float DelayInside = 0.3f;
	
	void Update () {
	    
	}


    public static void Respawn(float _delay)
    {
        Instance.Invoke("Respawn1", _delay);
    }



    //первый шаг
    private void Respawn1()
    {
        BlackScreen.SetBool("black", true);
        Instance.Invoke("Respawn2", delay);
    }

    //второй шаг
    public void Respawn2()
    {
        IRespawnListener[] listeners = GameObject.FindObjectsOfType<MonoBehaviour>().OfType<IRespawnListener>().ToArray();

        try
        {
        foreach (var l in listeners)
            l.OnRespawn();
        }
        catch (Exception) { }
        Instance.Invoke("Respawn3", DelayInside);
    }


    //третий шаг
    public void Respawn3()
    {
        BlackScreen.SetBool("black", false);
        Instance.Invoke("Respawn4", delay);
    }

    //четвертый шаг
    public void Respawn4()
    {
        IRespawnListener[] listeners = GameObject.FindObjectsOfType<MonoBehaviour>().OfType<IRespawnListener>().ToArray();

        try
        {
            foreach (var l in listeners)
                l.OnRespawnEnd();
        }
        catch (Exception) { }
    }


}
