﻿using UnityEngine;
using System.Collections;



/// <summary>
/// тип свайпа по экрану
/// </summary>
public enum Swipe
{
    Up,
    Down,
    Left,
    Right,
    None
}


/// <summary>
/// отвечает за ввод
/// </summary>
public class InputManager : MonoBehaviour {

    //Singleton
    private static InputManager _instance;
    private static InputManager Instance
    {
        get
        {
            //Если объекта еще нет, находим его на сцене
            if (_instance == null)
                _instance = GameObject.FindObjectOfType<InputManager>();
            return _instance;
        }
    }

	void Start () {
	}
	
	void Update () {
	}


    public static bool GetSwipe(Swipe swipe)
    {

        //проверяем мобильный ввод
        if (MobileInputManager.GetSwipe(swipe))
            return true;


        //ВВОД С пк
        switch (swipe)
        {
            case Swipe.Up:
                return Input.GetButtonDown("Jump") ||
                    Input.GetAxis("Vertical") > 0.1;
            case Swipe.Down:
                return Input.GetAxis("Vertical") < -0.1;
            case Swipe.Left:
                return Input.GetAxis("Horizontal") < -0.1;
            case Swipe.Right:
                return Input.GetAxis("Horizontal") > 0.1;
        }

        return false;
    }


    public static float GetAcceleration(bool horizontal)
    {
        if (Input.acceleration.x != 0)
            return Input.acceleration.x;

        return Input.GetAxis("Horizontal");
    }


    public static bool GetShot()
    {

        if (Input.GetMouseButtonDown(0))
            return true;

        return Input.GetButtonDown("Jump");
    }
}
