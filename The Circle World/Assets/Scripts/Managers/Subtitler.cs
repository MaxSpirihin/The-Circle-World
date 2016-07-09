using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

[System.Serializable]
public struct Subtitle
{
    public string id;
    public float length;
    public float wait;
}


/// <summary>
/// управляет воспроизведением субтитров
/// </summary>
public class Subtitler : MonoBehaviour
{

    //Singleton
    private static Subtitler _instance;
    public static Subtitler Instance
    {
        get
        {
            //Если объекта еще нет, находим его на сцене
            if (_instance == null)
                _instance = GameObject.FindObjectOfType<Subtitler>();
            return _instance;
        }
    }

    public Text textBox;//поле с текстом
    //есть 2 режима, в автоматическом все субтитры идут подряд сам, в ручном
    //нужно вызывать PlayNext() извне
    public bool autoMode = false;
    public Subtitle[] subtitles;

    private float timer = 0;
    
    //для автоматического режиима
    private List<float> eventsTime;
    private int currentEvent;
    
    //для русного режима
    private int currentSubtitle;

    /// <summary>
    /// субтитры в данный момент на экране
    /// </summary>
    public bool IsPlaying
    {
        get;
        private set;
    }

    void Start()
    {
       
    }

    /// <summary>
    /// начать воспроизведение
    /// </summary>
    public static void Play()
    {
        
        Instance.StartPlay();
    }

    /// <summary>
    /// ручной режим - показать следущую
    /// </summary>
    public static void PlayNext()
    {
        Instance._PlayNext();
    }

    /// <summary>
    /// ручной режим - остановить текущую
    /// </summary>
    public static void StopCurrent()
    {
        Instance._StopCurrent();
    }

    private void StartPlay()
    {
        Invoke("_StartPlay", 0.1f);

    }

    private void _StartPlay()
    {
        textBox.rectTransform.sizeDelta = new Vector2(Screen.width * 0.85f, Screen.height * 0.88f);
        textBox.fontSize = 20 * Screen.width / 600;


        IsPlaying = true;
        timer = 0;


        if (autoMode)
        {
            //запоминаем моменты времени, когда менять субтитры
            eventsTime = new List<float>() { 0 };
            float t = 0;
            for (int i = 0; i < subtitles.Length; i++)
            {
                t += subtitles[i].length;
                eventsTime.Add(t);
                t += subtitles[i].wait;
                eventsTime.Add(t);
            }
            eventsTime.RemoveAt(eventsTime.Count - 1);

            currentEvent = 0;
        }
        else
        {
            //начинаем воспроизведение первого субтитра
            currentSubtitle = -1;
            PlayNext();
        }
    }

    public void _PlayNext()
    {
        currentSubtitle++;
        textBox.text = LocalizationText.GetText(subtitles[currentSubtitle].id);
        timer = 0;
    }


    public void _StopCurrent()
    {
        textBox.text = "";
    }


    void Update()
    {

        if (autoMode)
        {
            //управляем автоматическим воспроизведением
            if (IsPlaying)
            {
                timer += Time.deltaTime;

                if (timer > eventsTime[currentEvent])
                {
                    int mode = currentEvent % 2;
                    int sNum = currentEvent / 2;

                    switch (mode)
                    {
                        case 0:
                            textBox.text = LocalizationText.GetText(subtitles[sNum].id);
                            break;
                        case 1:
                            textBox.text = "";
                            break;
                    }
                    currentEvent++;

                    if (currentEvent >= eventsTime.Count)
                    {
                        IsPlaying = false;
                    }
                }
            }
        }
        else
        {
            //тормозим текущую если пора
            timer += Time.deltaTime;

            if (timer > subtitles[currentSubtitle].length)
                _StopCurrent();
        }
    }
}
