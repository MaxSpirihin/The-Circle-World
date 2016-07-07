using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class MainMenuStarter : MonoBehaviour {

    public float MoveTime = 2f;
    public GameObject screen; 

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
        screen.SetActive(true);
    }
}
