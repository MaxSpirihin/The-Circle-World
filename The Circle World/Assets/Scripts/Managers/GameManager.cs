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

    public const int max_screen_width = 1280;

	void Awake() {
        native_width = Screen.width;
        native_height = Screen.height;
        LocalizationText.SetLanguage(Application.systemLanguage.ToString().Substring(0, 2).ToUpper());

        if (Screen.width > max_screen_width)
            Screen.SetResolution(max_screen_width, (max_screen_width*Screen.height)/Screen.width, true);
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
