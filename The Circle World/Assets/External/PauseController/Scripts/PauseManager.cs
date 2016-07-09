using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class PauseManager : MonoBehaviour
{
    public GameObject pausable;
    public Canvas pauseCanvas;

    public bool isPaused { get; private set; }
    private Animator anim;
    private Component[] pausableInterfaces;

    void Start()
    {
        isPaused = false;

        // PauseManager requires the EventSystem - make sure there is one
        if (FindObjectOfType<EventSystem>() == null)
        {
            var es = new GameObject("EventSystem", typeof(EventSystem));
            es.AddComponent<StandaloneInputModule>();
        }

        pausableInterfaces = pausable.GetComponents(typeof(IPausable));
        anim = pauseCanvas.GetComponent<Animator>();

        pauseCanvas.enabled = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                OnUnPause();
            }
            else
            {
                OnPause();
            }
        }

        pauseCanvas.enabled = isPaused;
        anim.SetBool("IsPaused", isPaused);
    }

    public void OnUnPause()
    {
        isPaused = false;
        MusicPlayer.UnPause();

        foreach (var pausableComponent in pausableInterfaces)
        {
            IPausable pausableInterface = (IPausable)pausableComponent;
            if (pausableInterface != null)
                pausableInterface.OnUnPause();
        }
    }

    public void OnPause()
    {
        isPaused = true;
        MusicPlayer.Pause();

        foreach (var pausableComponent in pausableInterfaces)
        {
            IPausable pausableInterface = (IPausable)pausableComponent;
            if (pausableInterface != null)
                pausableInterface.OnPause();
        }
    }
}
