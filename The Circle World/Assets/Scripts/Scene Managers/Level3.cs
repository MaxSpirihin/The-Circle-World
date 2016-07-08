using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;




[System.Serializable]
public class FadeInArray
{
    public ObjectFadeIn[] array;
}


/// <summary>
/// упроавляет сценой и уровнем на уровне 3
/// Качество кода минимальное т.к. это конечный скрипт без переиспользования и я торопился :(
/// </summary>
public class Level3 : MonoBehaviour {

    public Animator Player;
    public Animator CircleKing;
    public Animator BlackScreen;
    public Animator ShowMan;
    public GameObject SquareKing;
    public QuickCutsceneController cutScene1;
    public string NextLevelName;
    public AudioClip SquareKingTheme;
    public AudioClip AfterAttack;

    public float[] eventTimes;
    public Transform[] cameraPositions;
    public FadeInArray[] squarers;

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
        
        Subtitler.Play();
        ShowMan.SetTrigger("Talk");
        Invoke("State1", eventTimes[0]);
    }


    void State1()
    {
        
        ShowMan.SetTrigger("Default");
        Subtitler.PlayNext();
        CircleKing.SetTrigger("Talk");
        Invoke("State2", eventTimes[1]);
    }


    void State2()
    {
        CircleKing.SetTrigger("Default");
        Subtitler.StopCurrent();
        cutScene1.ActivateCutscene();
        Invoke("State3", eventTimes[2]);
    }


    void State3()
    {
        Subtitler.PlayNext();
        CircleKing.SetTrigger("Talk");
        Invoke("State4", eventTimes[3]);
        
    }

    void State4()
    {
        cutScene1.gameObject.SetActive(false);
        CircleKing.SetTrigger("Default");
        Subtitler.StopCurrent();
        NextCameraPosition();
        Invoke("State5", eventTimes[4]);
    }


    void State5()
    {
        SquareKing.GetComponent<ObjectFadeIn>().FadeIn();
        Invoke("State6", eventTimes[5]);
    }

    void State6()
    {
        SquareKing.GetComponent<Animator>().SetTrigger("Talk");
        Subtitler.PlayNext();
        Invoke("State7", eventTimes[6]);
    }

    void State7()
    {
        Subtitler.PlayNext();
        Invoke("State8", eventTimes[7]);
    }

    //выход квадратеров
    void State8()
    {
        Subtitler.StopCurrent();
        if (Squarer > 4)
        {
            Invoke("State9", eventTimes[13]);
        }
        else
        {
            NextCameraPosition();
            Invoke("NextFadeIn", 0.5f);
            Invoke("State8", eventTimes[8+Squarer]);
        }
    }


    void State9()
    {
        ShowMan.GetComponent<Animator>().SetTrigger("Talk");
        NextCameraPosition();
        Subtitler.PlayNext();
        Invoke("State10", eventTimes[14]);
    }


    void State10()
    {
        ShowMan.SetTrigger("Default");
        CircleKing.SetTrigger("Talk");
        Subtitler.PlayNext();
        Invoke("State11", eventTimes[15]);
    }


    void State11()
    {
        CircleKing.SetTrigger("Default");
        Player.SetTrigger("Talk");
        Subtitler.PlayNext();
        Invoke("State12", eventTimes[16]);
    }


    void State12()
    {
        Player.SetTrigger("Default");
        CircleKing.SetTrigger("Talk");
        Subtitler.PlayNext();
        Invoke("State13", eventTimes[17]);
    }


    void State13()
    {
        Subtitler.StopCurrent();
        CircleKing.SetTrigger("Default");
        Player.SetTrigger("Step");
        PlayerMove = true;
        Invoke("State14", eventTimes[18]);
    }

    void State14()
    {
        BlackScreen.speed = 0.2f;
        BlackScreen.SetBool("black", true);
        MusicPlayer.Stop(1f);
        Invoke("NextLevel", 1.5f);
    }


    void NextLevel()
    {
        SceneManager.LoadScene(NextLevelName);
    }


    void NextFadeIn()
    {
        foreach (var f in squarers[Squarer].array)
        {
            f .FadeIn();
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

        if (PlayerMove)
        {
            Player.transform.position = new Vector3(Player.transform.position.x + Player.GetComponent<PlayerControl>().AutoSpeedForward * Time.deltaTime,
                 Player.transform.position.y, Player.transform.position.z);
        }
	}
}
