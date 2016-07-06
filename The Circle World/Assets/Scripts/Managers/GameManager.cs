using UnityEngine;
using System.Collections;


/// <summary>
/// управляет некоторыми общими параметрами
/// </summary>
public class GameManager : MonoBehaviour {

	void Awake() {
        LocalizationText.SetLanguage(Application.systemLanguage.ToString().Substring(0, 2).ToUpper());
	}

    void Start()
    {
        Subtitler.Play();
    }
	
    public void Quit()
    {
        Application.Quit();
    }
}
