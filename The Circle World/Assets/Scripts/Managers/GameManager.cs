using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


/// <summary>
/// управляет некоторыми общими параметрами
/// </summary>
public class GameManager : MonoBehaviour {

	void Awake() {
        LocalizationText.SetLanguage(Application.systemLanguage.ToString().Substring(0, 2).ToUpper());
	}

    void Start()
    {
        
    }
	
    public void Quit()
    {
        Application.Quit();
    }


    public void Continue()
    {
        SceneManager.LoadScene("Level 2");
    }
}
