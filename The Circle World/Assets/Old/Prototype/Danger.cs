using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Danger : MonoBehaviour {

	void Start () {
	
	}
	
	void Update () {
	
	}

    public void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Player")
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
