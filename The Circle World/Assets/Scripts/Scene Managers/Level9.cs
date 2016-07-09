using UnityEngine;
using System.Collections;
using System.Linq;
using UnityEngine.SceneManagement;


/// <summary>
/// упроавляет сценой на уровне 9
/// </summary>
public class Level9 : MonoBehaviour
{
    public Animator SquareKing;
    public Transform[] cameraPos;
    public Transform PlayerPos;
    public GameObject Line;

    private int PosNum = 0;
    private PlayerControl player;
    private Transform camera;
    private int State = 0;

    void Start()
    {
        camera = GameObject.FindObjectOfType<Camera>().transform;
        player = GameObject.FindObjectOfType<PlayerControl>();
        NextCameraPosition();
        Invoke("StartScene", 0);
    }


    void StartScene()
    {
        MusicPlayer.Play();
        Invoke("KingTalk", 3f);
    }


    void KingTalk()
    {
        SquareKing.SetTrigger("Talk");
        Subtitler.Play();
        Invoke("PlayerEnter", 3.5f);
    }

    void PlayerEnter()
    {
        Subtitler.StopCurrent();
        NextCameraPosition();
        player.StartMove();
        State = 1;
    }


    void StartFight()
    {
        NextCameraPosition();
        player.controlType = PlayerStandartControlType.Continous;
        player.StartMove();
        player.GetComponent<Animator>().SetTrigger("Default");
        SquareKing.GetComponent<SquareKing>().StartFight();
        Line.SetActive(true);
    }

    void KingTalk2()
    {
        NextCameraPosition();
        Subtitler.PlayNext();
        Invoke("BeforeFight", 3.5f);
    }

    void BeforeFight()
    {
        Subtitler.StopCurrent();
        SquareKing.SetTrigger("Default");
        SquareKing.SetTrigger("Attack");
        Invoke("StartFight", 2f);
    }
    
    
    void Update()
    {
          if (State == 1 && player.transform.position.z < PlayerPos.position.z)
          {
              State = 2;
              player.AutoSpeedForward = 0;
              Subtitler.PlayNext();
              player.GetComponent<Animator>().SetTrigger("Talk");
              Invoke("KingTalk2", 2.5f);
          }
    }





    void NextCameraPosition()
    {
        camera.position = cameraPos[PosNum].position;
        camera.rotation = cameraPos[PosNum].rotation;
        PosNum++;
    }

}
