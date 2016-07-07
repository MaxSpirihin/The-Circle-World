using UnityEngine;
using System.Collections;


/// <summary>
/// изменяем позицию камеры прямо на ходу для красивого перехода
/// ВНИМАНИЕ - угол наклона первой точки и камеры синхронизируйте вручную
/// </summary>
public class CameraInGameChanger : MonoBehaviour {

    public QuickCutsceneController path;//катсцена с переходом
    public bool BlockPlayerHorizontal = false;//заблокировать горизонтальное движение игрока при входе

    private Transform camera;
    private bool isPlay = false;


	void Start () {
        camera = GameObject.FindObjectOfType<Camera>().transform;
	}
	

	void Update () {
	    if(isPlay)
        {
            if(!path.playingCutscene)
            {
                //катчецна завершилась, теперь камера должна ехать сама
                isPlay = false;
                camera.GetComponent<CameraToPlayer>().enabled = true;
                camera.GetComponent<CameraToPlayer>().SetOldPos();
      //          follower.enabled = false;
            }
        }
	}

    //запуск перехода
    public void OnTriggerEnter(Collider other)
    {
        //камера больше не едет сама
        camera.GetComponent<CameraToPlayer>().enabled = false;
        //синхронизируем позиции и активируем катсцену
        path.transform.position = camera.position;
        Invoke("StartCS", 0);
        //блокируем игрока если надо
        if (BlockPlayerHorizontal)
            GameObject.FindObjectOfType<PlayerControl>().BlockHorizontal(true);
        isPlay = true;
    }


    void StartCS()
    {
        path.ActivateCutscene();
    }

}
