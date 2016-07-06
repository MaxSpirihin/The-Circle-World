using UnityEngine;
using System.Collections;


/// <summary>
/// движение камеры за персоажем - только в одном направлении
/// </summary>
public class CameraToPlayer : MonoBehaviour {

    public Transform target;
    
    private Vector3 oldPosition;

	void Start () {
        SetOldPos();
	}

    public void SetOldPos()
    {
        oldPosition = target.position;
    }

	void Update () {
        Vector3 translate = new Vector3(0, 0, (target.position - oldPosition).z);
        transform.position = transform.position + translate;
        oldPosition = target.position;
	}
}
