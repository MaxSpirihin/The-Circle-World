using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using Assets.Scripts.gui;


/// <summary>
/// управляет некоторыми общими параметрами
/// </summary>
public class GameManager : MonoBehaviour {

    public static int native_width { get; private set; }
    public static int native_height { get; private set; }

	void Awake() {
        native_width = Screen.width;
        native_height = Screen.height;
        LocalizationText.SetLanguage(Application.systemLanguage.ToString().Substring(0, 2).ToUpper());
     //   PrefSaver.SetCompleteLevels(0);
   //     Debug.Log(PrefSaver.GetCompleteLevels());
        Screen.SetResolution(1280, 720, true);
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
        int level = PrefSaver.GetCompleteLevels();
        SceneManager.LoadScene("Load_" + (level+1));
    }


    public void NewGame()
    {
        PrefSaver.SetCompleteLevels(0);
        Continue();
    }
}
