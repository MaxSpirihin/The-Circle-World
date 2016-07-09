using UnityEngine;
using System.Collections;



/// <summary>
/// проигрывает музыку (не звуки) на сцене
/// </summary>
public class MusicPlayer : MonoBehaviour,IRespawnListener {


    //Singleton
    private static MusicPlayer _instance;
    private static MusicPlayer Instance
    {
        get
        {
            //Если объекта еще нет, находим его на сцене
            if (_instance == null)
                _instance = GameObject.FindObjectOfType<MusicPlayer>();
            return _instance;
        }
    }

    public bool NotStopMusic = false;
    
    private AudioSource source;


	void Awake()
    {
        source = GetComponent<AudioSource>();
    }


    /// <summary>
    /// начать воспроизведение
    /// </summary>
    public static void Play()
    {
        Instance._Play();
    }


    /// <summary>
    /// плавно прервать воспроизведение
    /// </summary>
    /// <param name="timeOut">время выхода</param>
    public static void Stop(float timeOut)
    {
        Instance.StartCoroutine(FadeOut(Instance.source, timeOut));
    }


    private void _Play()
    {
        source.Play();
    }


    public void OnRespawnEnd()
    {
        if (!NotStopMusic)
            _Play();
    }

    public static void Pause()
    {
        Instance.source.Pause();
    }

    public static void UnPause()
    {
        Instance.source.UnPause();
    }


    public static IEnumerator FadeOut(AudioSource audioSource, float FadeTime)
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / FadeTime;
            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume;
    }

    public void OnRespawn()
    {
    }
}
