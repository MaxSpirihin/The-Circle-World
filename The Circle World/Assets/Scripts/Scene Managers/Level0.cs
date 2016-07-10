using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Level0 : MonoBehaviour {


    public QuickCutsceneController cs;
    public RectTransform image;
    public Vector2 imageSpeed;
    public Animator BlackScreen;
    public string NextLevelName = "Level 2";
    public Text subtitileText;
    public Font AppNameFont;

    int State = 0;


    void Start()
    {
        State = 1;
        MusicPlayer.Play();
        Subtitler.Play();
        Invoke("StartScene", 7f);
    }

    
    void StartScene () {
        State = 2;
        image.gameObject.SetActive(false);
        cs.ActivateCutscene();
        GameObject.FindObjectOfType<PlayerControl>().StartMove();
        Invoke("End", 14f);
	}


    void End()
    {
        MusicPlayer.Stop(6f);
        BlackScreen.speed = 0.2f;
        BlackScreen.SetBool("black", true);
        subtitileText.font = AppNameFont;
        subtitileText.color = Color.yellow;
        subtitileText.fontSize *= 2;
        subtitileText.alignment = TextAnchor.MiddleCenter;
        Invoke("NextLevel", 6f);
    }


    void NextLevel()
    {
        GameObject.FindObjectOfType<PlayerControl>().StopMove();
        SceneManager.LoadScene(NextLevelName);
    }


	void Update () {
        if (State == 1)
            image.anchoredPosition = image.anchoredPosition + imageSpeed * Time.deltaTime;
	}
}
