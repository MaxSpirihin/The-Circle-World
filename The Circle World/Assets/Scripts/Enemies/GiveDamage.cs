using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


/// <summary>
/// убивает играка при столкновении с ним
/// </summary>

public class GiveDamage : MonoBehaviour {

    PlayerControl player;

    //были проблемы с реагированием несколько раз подряд
    private bool active = true;
    
    
    void Start () {
        player = GameObject.FindObjectOfType<PlayerControl>();
	}
	
	void Update () {
	}

    public void OnTriggerEnter(Collider other)
    {
        if (active)
            if (other.tag == "Player")
            {
                player.Kill();
                active = false;
                Invoke("ReActive", 3f);
            }
    }


    void ReActive()
    {
        active = true;
    }
}
