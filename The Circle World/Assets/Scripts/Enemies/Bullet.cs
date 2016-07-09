using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


/// <summary>
/// убивает играка при столкновении с ним
/// </summary>

public class Bullet : MonoBehaviour {


    
    void Start () {
       
	}
	
	void Update () {
	}

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "FinalBoss")
        {
            GameObject.FindObjectOfType<SquareKing>().Shot();
            Destroy(gameObject);
        }
    }

}
