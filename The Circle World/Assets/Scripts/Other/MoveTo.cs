using UnityEngine;
using System.Collections;


/// <summary>
/// реализует движение объкута в нужном направлении
/// </summary>
public class MoveTo : MonoBehaviour {

    public Vector3 speed;

    
    void Update () {
        transform.position = transform.position + speed * Time.deltaTime;
	}
}
