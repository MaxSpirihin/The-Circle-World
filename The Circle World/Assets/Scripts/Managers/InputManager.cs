using UnityEngine;
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

    //тип управления
    public enum ControlType
    {
        PC, Mobile
    }

    public ControlType controlType;
	
	void Start () {
	
	}
	
	void Update () {
	
	}


    public static bool GetSwipe(Swipe swipe)
    {

        //проверяем мобильный ввод
        if (MobileInputManager.GetSwipe(swipe))
            return true;


        //ВВОД С pc
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
}
