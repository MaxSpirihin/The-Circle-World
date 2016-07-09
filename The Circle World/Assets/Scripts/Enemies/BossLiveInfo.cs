using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BossLiveInfo : MonoBehaviour {


    private SquareKing boss;

	void Start () {
        boss = GameObject.FindObjectOfType<SquareKing>();
    }
	
    
    void Update () {
        GetComponent<RectTransform>().offsetMin = new Vector2(Screen.width * (1 - boss.lives * 1f / boss.StartLives) / 2, Screen.height * 0.98f);
        GetComponent<RectTransform>().offsetMax = new Vector2(-Screen.width * (1 - boss.lives * 1f / boss.StartLives) / 2, 10);
	}
}
