using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PathFollow : MonoBehaviour,IRespawnListener
{
	
	public enum FollowType
	{
		MoveTowards,
		Lerp
	}
	
	public FollowType Type = FollowType.MoveTowards;
	public PathDefinition Path;
	public float Speed = 1;
	public float MaxDistanceToGoal = .1f;
    public bool OnRespawnAgain = false;
    public bool StopOnEnd = false;
	
	private IEnumerator<Transform> _currentPoint;
	
	public void Start ()
	{
		if(Path == null)
		{
			Debug.LogError("Path Cannot be null", gameObject);
			return;
		}
		
		_currentPoint = Path.GetPathEnumerator();
		_currentPoint.MoveNext();
        
		
		if(_currentPoint.Current == null)
			return;
			
		transform.position = _currentPoint.Current.position;
	}
	
	public void Update ()
	{
        if (StopOnEnd && Path.Complete)
            return;

		if(_currentPoint == null || _currentPoint.Current == null)
			return;
		
		if(Type == FollowType.MoveTowards)
			transform.position = Vector3.MoveTowards(transform.position, _currentPoint.Current.position, Time.deltaTime * Speed);
		else if(Type == FollowType.Lerp)
			transform.position = Vector3.Lerp(transform.position, _currentPoint.Current.position, Time.deltaTime * Speed);
		
		var distanceSquared = (transform.position - _currentPoint.Current.position).sqrMagnitude;
		if(distanceSquared < MaxDistanceToGoal * MaxDistanceToGoal)
			_currentPoint.MoveNext();
	}


    void IRespawnListener.OnRespawn()
    {
        if (OnRespawnAgain)
        {
            _currentPoint = Path.GetPathEnumerator();
            _currentPoint.MoveNext();
            transform.position = _currentPoint.Current.position;
        }
    }

    void IRespawnListener.OnRespawnEnd()
    {
    }
}
