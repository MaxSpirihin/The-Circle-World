using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


/// <summary>
/// управляет окончанием уровня
/// </summary>
public class Finisher : MonoBehaviour {

    public string NextLevelName;
    public Animator BlackScreen;
    public float playerSpeedMultiplier = 0.5f;
    public bool StopCamera = false;
	
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<PlayerControl>().AutoSpeedForward *= playerSpeedMultiplier;
            other.GetComponent<PlayerControl>().shot = false;
            other.GetComponent<PlayerControl>().BlockHorizontal(true);
            other.GetComponent<PlayerControl>().blockVerticalMove = true;
            BlackScreen.speed = 0.2f;
            BlackScreen.SetBool("black", true);
            MusicPlayer.Stop(1f);
            Invoke("NextLevel", 1.5f);

            if (StopCamera)
            {
                GameObject.FindObjectOfType<Camera>().GetComponent<CameraToPlayer>().enabled = false;
            }
        }
    }

    void NextLevel()
    {
        SceneManager.LoadScene(NextLevelName);
    }
}
