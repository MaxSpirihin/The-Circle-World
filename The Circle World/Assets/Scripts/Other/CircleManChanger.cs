using UnityEngine;
using System.Collections;


/// <summary>
/// осуществляет выбор анимации и лица массовки
/// </summary>
public class CircleManChanger : MonoBehaviour {

    //неизменяемые данные
    public Material[] faces;
    public MeshRenderer face;

    //изменяемые параметры
    public bool random = false; //лучше не трогать
    public int animationNumber = 1;
    public float animTime = 1;
    public int faceNumber;


	void Start () {
        GetComponent<Animator>().Play("Viewer" + animationNumber);
        GetComponent<Animator>().speed = animTime;
        face.material = faces[faceNumber];
    }
	
    void OnDrawGizmos()
	{
        if (random)
        {
            animationNumber = Random.Range(1, 6);
            faceNumber = Random.Range(0, faces.Length - 1);
        }
	}
}
