using UnityEngine;
using System.Collections;



/// <summary>
/// упроавляет сценой и уровнем на уровне 2
/// Качество кода минимальное т.к. это конечный скрипт без переиспользования и я торопился :(
/// </summary>
public class Level2 : MonoBehaviour {

    public QuickCutsceneController cutscene;
    private PlayerControl player;
    public Animator ShowMan;
    bool isStart = true;
    public float playerSpeed = 2f;
    public float playerRotSpeed = 100f;
    public Transform playerTarget;
    public CameraToPlayer cameraScript;

    private int currentEvent = 0;
    private int playerMoveMode = 0;

	void Start () {

       player = GameObject.FindObjectOfType<PlayerControl>();

      // Invoke("StartDebug", 0);
        

        ShowMan.SetTrigger("Talk");
        cameraScript.enabled = false;
        GetComponent<AudioSource>().Play();
        Subtitler.Play();
        Invoke("StartCS", 0);
    }

    void StartDebug()
    {
        player.transform.position = playerTarget.position;
        player.GetComponent<Animator>().SetTrigger("Step");
        OnCutsceneEnd();
    }

    void StartCS()
    {
        cutscene.ActivateCutscene();
    }

    void OnCutsceneEnd()
    {
        GameObject.FindObjectOfType<PlayerControl>().StartMove();
        cameraScript.GetComponent<OnRespawnReturn>().RememberStart();
        cameraScript.enabled = true;
        MusicPlayer.Play();
        cameraScript.SetOldPos();
    }
	

	void Update () {

        //действия при обновлении состояния
        int _event = cutscene.currentStateNumber;
        if (_event != currentEvent)
        {
            currentEvent = _event;
            switch (currentEvent)
            {
                case 1:
                    ShowMan.SetTrigger("Default");
                    break;
                case 2:
                    player.GetComponent<Animator>().SetTrigger("Hello");
                    break;
                case 4:
                    player.GetComponent<Animator>().SetTrigger("Default");
                    player.GetComponent<Animator>().SetTrigger("Step");
                    Subtitler.PlayNext();
                    break;
                case 7:
                    player.GetComponent<Animator>().SetTrigger("Default");
                    MusicPlayer.Stop(1f);
                    break;
            }
        }

        //действия в зависиомти от состояния
        if (currentEvent > 2)
        {
            switch (playerMoveMode)
            {
                case 0:
                    player.transform.Translate(new Vector3(0, 0, playerSpeed * Time.deltaTime/2));
                    if (player.transform.position.x > playerTarget.position.x)
                    {
                        player.transform.position = new Vector3(playerTarget.position.x,
                            player.transform.position.y,player.transform.position.z);
                        playerMoveMode = 1;
                    }
                    break;
                case 1:
                    player.transform.Rotate(new Vector3(0, playerRotSpeed * Time.deltaTime, 0));
                    if (player.transform.eulerAngles.y > 180)
                    {
                        player.transform.rotation = new Quaternion(player.transform.rotation.x, 1,
                            player.transform.rotation.z, player.transform.rotation.w);
                        playerMoveMode = 2;
                    }
                    break;
                case 2:
                    player.transform.Translate(new Vector3(0, 0, playerSpeed * Time.deltaTime));
                     if (player.transform.position.z < playerTarget.position.z)
                    {
                        player.transform.position = new Vector3(player.transform.position.x,
                            player.transform.position.y, playerTarget.position.z);
                        player.GetComponent<Animator>().SetTrigger("Default");
                        player.GetComponent<Animator>().SetTrigger("Talk");
                        Subtitler.PlayNext();
                        playerMoveMode = 3;
                    }
                    break;
            }
           
            
        }


        if (!cutscene.playingCutscene && isStart)
        {
            isStart = false;
            OnCutsceneEnd();
        }
	}
}
