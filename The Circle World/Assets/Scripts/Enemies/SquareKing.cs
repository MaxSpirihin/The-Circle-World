using UnityEngine;
using System.Collections;

public class SquareKing : MonoBehaviour,IRespawnListener {

    public Transform Left;
    public Transform Right;
    public float horizontalSpeed;
    public Transform LeftBullet;
    public Transform RightBullet;
    public GameObject bullet;
    public AudioClip shotAudio;
    public float shootTime = 1f;
    public int StartLives = 30;
    public GameObject DeathEffect;


    private int lives;
    private int State = 0;
    private bool ToRight;
    private float timer = 0;

	void Start () {
	    
	}


    public void StartFight()
    {
        State = 1;
        lives = StartLives;
        ToRight = true;
    }
	
	void Update () {

        if (State == 1)
        {
            //движение
            float newX = transform.position.x + horizontalSpeed * Time.deltaTime * (ToRight ? 1 : -1);
            if (newX > Right.position.x)
            {
                newX = Right.position.x;
                ToRight = false;
            }
            if (newX < Left.position.x)
            {
                newX = Left.position.x;
                ToRight = true;
            }

            transform.position = new Vector3(newX, transform.position.y, transform.position.z);

            //выстрел
            timer += Time.deltaTime;
            if (timer > shootTime)
            {
                timer = 0;
                Instantiate(bullet, LeftBullet.position, LeftBullet.rotation);
                Instantiate(bullet, RightBullet.position, RightBullet.rotation);
                if (shotAudio != null)
                    GetComponent<AudioSource>().PlayOneShot(shotAudio);
            }

        }
	}


    public void Shot()
    {
        lives--;
        GetComponent<Animator>().SetTrigger("shot");

        if (lives == 0)
        {
            DeathEffect.SetActive(true);
            GameObject.FindObjectOfType<PlayerControl>().CantKill = true;
            State = 2;
            Invoke("Death", 2.5f);
        }
    }


    void Death()
    {
        GetComponent<ObjectFadeOut>().FadeOut();
    }

    public void OnRespawn()
    {
        lives = StartLives;
    }

    public void OnRespawnEnd()
    {
    }
}
