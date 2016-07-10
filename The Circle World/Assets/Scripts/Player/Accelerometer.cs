using UnityEngine;
using System.Collections;

public class Accelerometer : MonoBehaviour, IRespawnListener {

    public float acc = 0.1f;
    public float Max = 20;

    private PlayerControl player;
    private float StartSpeed;

	void Start () {
        player = GetComponent<PlayerControl>();
        StartSpeed = player.AutoSpeedForward;
	}
	
    
    void Update () {
        if (player.AutoSpeedForward < Max)
            player.AutoSpeedForward += acc * Time.deltaTime;
	}

    public void OnRespawn()
    {
        
    }

    public void OnRespawnEnd()
    {
        player.AutoSpeedForward = StartSpeed;
    }
}
