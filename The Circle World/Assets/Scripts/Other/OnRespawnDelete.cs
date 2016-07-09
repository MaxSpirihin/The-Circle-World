using UnityEngine;
using System.Collections;


/// <summary>
/// возвращае объект в начальное положение при респавнне
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
