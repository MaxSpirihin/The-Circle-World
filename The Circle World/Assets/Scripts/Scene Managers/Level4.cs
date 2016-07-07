using UnityEngine;
using System.Collections;



/// <summary>
/// упроавляет сценой и уровнем на уровне 4
/// </summary>
public class Level4 : MonoBehaviour {

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
