using UnityEngine;
using System.Collections;

public class MainMenuStarter : MonoBehaviour {

    public float MoveTime = 2f;

    private PlayerControl Player;
    private bool isMove = true;


	void Start () {
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
    }
}
