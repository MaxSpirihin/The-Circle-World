using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Assets.Scripts.gui;

public class MainMenuStarter : MonoBehaviour {

    public float MoveTime = 2f;
    public Animator screen;
    public Animator AreYouSure;


    private PlayerControl Player;
    private bool isMove = true;


	void Start () {

        if (FindObjectOfType<EventSystem>() == null)
        {
            var es = new GameObject("EventSystem", typeof(EventSystem));
            es.AddComponent<StandaloneInputModule>();
        }

        Invoke("PlayMenu", MoveTime);
        Invoke("StartMove", 0);
        Player = GameObject.FindObjectOfType<PlayerControl>();
    }
	
	void Update () {
	    
	}

    void StartMove()
    {
        Player.StartMove();
    }

    void PlayMenu()
    {
        Player.AutoSpeedForward = 0;
        Player.GetComponent<Animator>().SetTrigger("Win");

        if (PrefSaver.GetCompleteLevels() == 0)
            screen.SetTrigger("OneBtn");
        else
            screen.SetTrigger("TwoBtn");
    }


    public void NewGameSure()
    {
        AreYouSure.SetBool("show", true);
    }

    public void CancelNewGame()
    {
        AreYouSure.SetBool("show", false);
    }

}
