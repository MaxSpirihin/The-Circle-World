using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


/// <summary>
/// убивает врага при столкновении с ним
/// </summary>
public class Bullet : MonoBehaviour {

    public bool isLevel = false;
    
    void Start () {
        Destroy(gameObject, 4f);
	}
	
	void Update () {
	}

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "FinalBoss")//вначале делал только босс, отсюда нелогичный тег((
        {
            if (!isLevel)
                GameObject.FindObjectOfType<SquareKing>().Shot();
            else
            {
                KillableEnemy en = other.GetComponentInParent<KillableEnemy>();
                if (en != null)
                    en.Shot();
            }
                

            Destroy(gameObject);
        }
    }

}
