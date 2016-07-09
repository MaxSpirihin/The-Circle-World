using UnityEngine;
using System.Collections;

public class MoveTo : MonoBehaviour {

    public Vector3 speed;

    
    void Update () {
        transform.position = transform.position + speed * Time.deltaTime;
	}
}
