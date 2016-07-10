using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;





[System.Serializable]
public class FadeOutArray
{
    public ObjectFadeOut[] array;
}


/// <summary>
/// упроавляет сценой и уровнем на уровне 10
/// Качество кода минимальное т.к. это конечный скрипт без переиспользования и я торопился :(
/// </summary>
public class Level10 : MonoBehaviour {

    public Animator CircleKing;
    public Animator BlackScreen;
    public string NextLevelName;
    
    public float[] eventTimes;
    public Transform[] cameraPositions;
    public FadeOutArray[] squarers;

    public Text subtitileText;
    public Font AppNameFont;
    public Font buttonFont;

    private Transform camera;
    private int CameraPos = 0;
    private int Squarer = 0;
    private bool PlayerMove = false;

	void Start () {
        camera = GameObject.FindObjectOfType<Camera>().transform;
        Invoke("StartScene", 1.5f);
        MusicPlayer.Play();
    }


    void StartScene()
    {
        //Subtitler.Play();
        Invoke("State0", eventTimes[0]);
    }


    //уход квадратеров
    void State0()
    {
        Subtitler.StopCurrent();
        if (Squarer > 4)
        {
            Invoke("State1", eventTimes[5]);
        }
        else
        {
            NextCameraPosition();
            Invoke("NextFadeOut", 0.5f);
            Invoke("State0", eventTimes[Squarer]);
        }
    }

    void State1()
    {
        Subtitler.Play();
        NextCameraPosition();
        CircleKing.SetTrigger("Talk");
        Invoke("State2", eventTimes[6]);
    }


    void State2()
    {
        CircleKing.SetTrigger("Default");
        NextCameraPosition();
        Subtitler.PlayNext();
        Invoke("State4", eventTimes[7]);
    }

    void State4()
    {
        Subtitler.StopCurrent();
        BlackScreen.speed = 0.2f;
        BlackScreen.SetBool("black", true);
        Invoke("State5", eventTimes[8]);
    }


    void State5()
    {
        subtitileText.font = AppNameFont;
        subtitileText.color = Color.yellow;
        subtitileText.fontSize *= 2;
        subtitileText.alignment = TextAnchor.MiddleCenter;
        Subtitler.PlayNext();
        Invoke("State6", eventTimes[9]);
    }

    void State6()
    {
        subtitileText.color = Color.white;
        subtitileText.fontSize = (int)(0.7f*subtitileText.fontSize);
        subtitileText.font = buttonFont;
        Subtitler.PlayNext();
        Invoke("State6_1", eventTimes[10]);
    }

    void State6_1()
    {
        Subtitler.PlayNext();
        Invoke("State7", eventTimes[11]);
    }

    void State7()
    {
        Subtitler.PlayNext();
        Invoke("State8", eventTimes[12]);
    }

    void State8()
    {
        Subtitler.StopCurrent();
        Invoke("NextLevel", eventTimes[13]);
    }

    void NextLevel()
    {
        SceneManager.LoadScene(NextLevelName);
    }


    void NextFadeOut()
    {
        foreach (var f in squarers[Squarer].array)
        {
            f .FadeOut();
        }
        Squarer++;
    }

    void NextCameraPosition()
    {
        camera.position = cameraPositions[CameraPos].position;
        camera.rotation = cameraPositions[CameraPos].rotation;
        CameraPos++;
    }
        

	void Update () {
	}
}
