using UnityEngine;
using System.Collections;

public class CameraToPlayer : MonoBehaviour {

    public Transform target;


    private Vector3 oldPosition;

	void Start () {
        oldPosition = target.position;
	}
	
	void Update () {

        Vector3 translate = new Vector3(0, 0, (target.position - oldPosition).z);

        transform.position = transform.position + translate;
        oldPosition = target.position;
	}
}
