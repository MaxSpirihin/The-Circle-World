using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Respawner : MonoBehaviour {

    //Singleton
    private static Respawner _instance;
    private static Respawner Instance
    {
        get
        {
            //Если объекта еще нет, находим его на сцене
            if (_instance == null)
                _instance = GameObject.FindObjectOfType<Respawner>();
            return _instance;
        }
    }

	void Start () {
	    
	}
	
	void Update () {
	    
	}

    private void Respawn()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    public static void Respawn(float delay)
    {
        Instance.Invoke("Respawn", delay);
    }
}
