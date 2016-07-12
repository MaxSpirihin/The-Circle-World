using UnityEngine;
using System.Collections;



/// <summary>
/// упроавляет сценой на обычном уровне
/// </summary>
public class Level : MonoBehaviour {

    private PlayerControl player;

	void Start () {
        Invoke("StartMove", 0);
    }

    void StartMove()
    {
        player = GameObject.FindObjectOfType<PlayerControl>();
        player.StartMove();
        MusicPlayer.Play();
    }

   
}
