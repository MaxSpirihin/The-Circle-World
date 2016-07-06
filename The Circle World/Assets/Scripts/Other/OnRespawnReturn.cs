using UnityEngine;
using System.Collections;


/// <summary>
/// возвращае объект в начальное положение при респавнне
/// </summary>
public class OnRespawnReturn : MonoBehaviour, IRespawnListener {

	private Vector3 StartPos;
    private Quaternion StartRot;


	void Start () {
        RememberStart();
	}

    /// <summary>
    /// использовать текущую позицию при респавне
    /// </summary>
    public void RememberStart()
    {
        StartPos = transform.position;
        StartRot = transform.rotation;
    }
	

    void IRespawnListener.OnRespawn()
    {
        transform.position = StartPos;
        transform.rotation = StartRot;
    }

    void IRespawnListener.OnRespawnEnd()
    {
    }
}
