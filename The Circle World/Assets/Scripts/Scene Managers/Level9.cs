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

    private PlayerControl player;
    private Transform camera;

    void Start()
    {
        camera = GameObject.FindObjectOfType<Camera>().transform;
        player = GameObject.FindObjectOfType<PlayerControl>();
        Invoke("StartFight", 0);
        
    }

    void StartFight()
    {
        MusicPlayer.Play();
        player.StartMove();
        SquareKing.GetComponent<SquareKing>().StartFight();
        SquareKing.SetTrigger("Attack");
    }

  


    void Update()
    {
      
    }


}
