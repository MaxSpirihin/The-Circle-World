using UnityEngine;
using System.Collections;


/// <summary>
/// враг, убивающийся пулями с нескольких выстрелов
/// </summary>
public class KillableEnemy : MonoBehaviour,IRespawnListener {

    public int StartLives = 3;
    public GameObject DeathEffect;
    public Transform DeathPoint;

    private int lives;


	void Start () {
        lives = StartLives;
	}


    /// <summary>
    /// при попадании во врага пули
    /// </summary>
    public void Shot()
    {
        lives--;
        GetComponent<Animator>().SetTrigger("shot");

        if (lives == 0)
        {
            DeathEffect.SetActive(true);
            GameObject.FindObjectOfType<PlayerControl>().GetComponent<AudioSource>().
                PlayOneShot(GameObject.FindObjectOfType<PlayerControl>().Death);
            Invoke("Death", 0.1f);
        }
    }

    void Death()
    {
        transform.position = DeathPoint.position;
    }
	
	void Update () {
	}



    public void OnRespawn()
    {
        DeathEffect.SetActive(false);
        lives = StartLives;
    }

    public void OnRespawnEnd()
    {
    }
}
