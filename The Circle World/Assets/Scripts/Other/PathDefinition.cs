using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

public class PathDefinition : MonoBehaviour
{
	public Transform[] Points;
    public bool Cycle;
    public bool Complete { get; private set; }
	
	public IEnumerator<Transform> GetPathEnumerator()
	{
        Complete = false;

		if(Points == null || Points.Length < 1)
		{
			yield break;
		}
						
		int direction = 1;
		int index = 0;
		while (true)
		{
			yield return Points[index];
			
			if(Points.Length == 1)
				continue;

            if (!Cycle)
            {
                if (index <= 0)
                    direction = 1;
                else if (index >= Points.Length - 1)
                    direction = -1;
                index = index + direction;
            }
            else
            {
                index++;
                if (index >= Points.Count())
                    index -= Points.Count();
            }

            if (index == Points.Length - 1)
                Complete = true;
		}
	}
	
	public void OnDrawGizmos()
	{
		
		if(Points == null || Points.Length < 2)
			return;			
		
		var points = Points.Where(t=> t != null).ToList();
		
		if (points.Count < 2)
			return;
		
		for (var i = 1; i < points.Count; i++)
		{
			Gizmos.DrawLine(points[i - 1].position, points[i].position);
		}
		
	}
}
