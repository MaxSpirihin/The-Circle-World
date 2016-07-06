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
	
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<PlayerControl>().AutoSpeedForward *= playerSpeedMultiplier;
            BlackScreen.speed = 0.2f;
            BlackScreen.SetBool("black", true);
            MusicPlayer.Stop(1f);
            Invoke("NextLevel", 1.5f);
        }
    }

    void NextLevel()
    {
        SceneManager.LoadScene(NextLevelName);
    }
}
