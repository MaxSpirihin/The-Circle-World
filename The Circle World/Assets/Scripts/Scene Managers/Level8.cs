using UnityEngine;
using System.Collections;
using System.Linq;

[System.Serializable]
public struct Phrase
{
    public string subtitle_id;
    public float time;
    public bool IsPlayer;
    public Transform CameraPos;
}



/// <summary>
/// упроавляет сценой на уровне 8
/// </summary>
public class Level8 : MonoBehaviour
{

    public Phrase[] dialog;
    public Animator wiseMan;
    public Animator player;
    public Subtitler subtitler;

    private Transform camera;
    private int currentDialog = 0;
    private float time = 0;

    void Start()
    {
        camera = GameObject.FindObjectOfType<Camera>().transform;

        //вбиваем субтитры
        Subtitler.Instance.subtitles = new Subtitle[dialog.Length];
        for (int i = 0; i < dialog.Length; i++)
        {
            Subtitler.Instance.subtitles[i] = new Subtitle()
            {
                id = dialog[i].subtitle_id,
                length = 1000
            };
        }

        Invoke("StartScene", 0);
    }

    void StartScene()
    {
        MusicPlayer.Play();
        Dialog();
    }


    void Update()
    {
        time += Time.deltaTime;
    }


    void Dialog()
    {

        if (currentDialog == 0)
            Subtitler.Play();
        else
            Subtitler.PlayNext();
        camera.position = dialog[currentDialog].CameraPos.position;
        camera.rotation = dialog[currentDialog].CameraPos.rotation;


        if (currentDialog == 0 || dialog[currentDialog].IsPlayer != dialog[currentDialog - 1].IsPlayer)
        {
            if (dialog[currentDialog].IsPlayer)
            {
                player.SetTrigger("Talk");
                wiseMan.SetTrigger("Default");
            }
            else
            {
                wiseMan.SetTrigger("Talk");

                if (currentDialog != 0)
                    player.SetTrigger("Default");
            }
        }


        currentDialog++;

        if (currentDialog < dialog.Length)
            Invoke("Dialog", dialog[currentDialog].time);
        else
            Invoke("End", dialog[currentDialog].time);
    }


    void End()
    {

    }


}
