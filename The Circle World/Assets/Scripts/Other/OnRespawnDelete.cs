using UnityEngine;
using System.Collections;


/// <summary>
/// удаляет объект при респавнне
/// </summary>
public class OnRespawnDelete : MonoBehaviour, IRespawnListener {


    void IRespawnListener.OnRespawn()
    {
        Destroy(gameObject);
    }

    void IRespawnListener.OnRespawnEnd()
    {
    }
}
