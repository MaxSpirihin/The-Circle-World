using UnityEngine;
using System.Collections;
using Assets.Scripts.gui;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public int levelNumber;
    private AsyncOperation async = null;
    private bool isLoading = false;
    public string levelName = "";
    public Color bgLine;
    public Color loadLine;
    public Texture GUITextureBackground;

    private IEnumerator _Start()
    {
        //сохраняем прогресс
        if (PrefSaver.GetCompleteLevels() < levelNumber - 1)
            PrefSaver.SetCompleteLevels(levelNumber - 1);

        async = SceneManager.LoadSceneAsync(levelName);
        while (!async.isDone)
        {
            yield return null;
        }
        isLoading = false;
        yield return async;
    }

    private void OnGUI()
    {
        if (GUITextureBackground != null)
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), GUITextureBackground, ScaleMode.ScaleAndCrop);
        if (!isLoading)
        {
            {
                isLoading = true;
                StartCoroutine("_Start");
            }
        }
        else
        {
            DrawQuad(new Rect(Screen.width * 0.1f, Screen.height * 0.8f, Screen.width * 0.8f, Screen.height * 0.03f), bgLine);
            DrawQuad(new Rect(Screen.width * 0.1f + 1, Screen.height * 0.8f + 1, async.progress * Screen.width * 0.8f-2, Screen.height * 0.03f - 2), loadLine);
        }
    }

    void DrawQuad(Rect position, Color color)
    {
        Texture2D texture = new Texture2D(1, 1);
        texture.SetPixel(0, 0, color);
        texture.Apply();
        GUI.skin.box.normal.background = texture;
        GUI.Box(position, GUIContent.none);
    }

}

