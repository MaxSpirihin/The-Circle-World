using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GiveDamage : MonoBehaviour {

    PlayerControl player;

	void Start () {
        player = GameObject.FindObjectOfType<PlayerControl>();
	}
	
	void Update () {
	
	}

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            player.Kill();
    }
}
