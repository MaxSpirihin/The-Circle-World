using UnityEngine;
using System.Collections;
using System.Linq;
using UnityEngine.SceneManagement;

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
    public float playerRotSpeed = 100f;
    public Transform respawnPoint;
    public PlayerControl tank;
    public Animator BlackScreen;
    public string NextLevelName;

    private Transform camera;
    private int currentDialog = 0;
    private int State = 0;
    private bool IsDialog = true;

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
        if (!IsDialog)
        {
            switch (State)
            {
                case 1:
                    //вращаем персонажа
                    player.transform.Rotate(new Vector3(0, -playerRotSpeed * Time.deltaTime, 0));

                    if (player.transform.eulerAngles.y < 180)
                    {
                        player.transform.rotation = new Quaternion(player.transform.rotation.x, 1,
                            player.transform.rotation.z, player.transform.rotation.w);
                        State = 2;
                        player.GetComponent<PlayerControl>().StartMove();
                    }
                    break;
                case 2:
                    if (player.transform.position.z < respawnPoint.position.z)
                    {
                        player.gameObject.SetActive(false);
                        State = 3;
                        tank.gameObject.SetActive(true);
                        Invoke("State3", 3.5f);
                    }
                    break;
            }
        }
    }

    void State3()
    {
        tank.StartMove();
        Invoke("State4", 4f);
    }

    void State4()
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
            Invoke("Dialog", dialog[currentDialog - 1].time);
        else
            Invoke("End", dialog[currentDialog - 1].time);
    }


    void End()
    {
        Subtitler.StopCurrent();
        IsDialog = false;
        player.SetTrigger("Default");
        State = 1;
        player.SetTrigger("Step");
    }


}
