using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


/// <summary>
/// убивает играка при столкновении с ним
/// </summary>

public class GiveDamage : MonoBehaviour {

    PlayerControl player;
    public bool DestroyOnCollide = false;

    //были проблемы с реагированием несколько раз подряд
    private static bool active = true;

    void Awake()
    {
        //при перезагрзуке сцены
        active = true;
    }
    
    
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
                if (DestroyOnCollide)
                    Destroy(gameObject);
                Invoke("ReActive", 2.5f);
            }
    }


    void ReActive()
    {
        active = true;
    }
}
