using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
[CreateAssetMenu(fileName = "PathFinding", menuName = "SteeringBehaviours/PathFinding", order = 1)]
public class PathFinding : SteeringBehaviours
{
	public float nodeRadius = .1f, targetRadius = 3f;
	private int currentNode = 0;
	private bool isAtTarget = false;

	private NavMeshPath path;

	public override void OnDrawGizmosSelected(AI owner)
	{
		if(path != null)
		{
			Vector3[] points = path.corners;
			for (int i = 0; i < points.Length - 1; i++)
			{
				Vector3 pointA = points[i];
				Vector3 pointB = points[i + 1];
				Gizmos.color = Color.blue;
				Gizmos.DrawLine(pointA, pointB);

				Gizmos.color = Color.red;
				Gizmos.DrawSphere(pointA, nodeRadius);
			}
		}


	}
	public override Vector3 GetForce(AI owner)
	{
		Vector3 force = Vector3.zero;

		NavMeshAgent agent = owner.agent;

		if (owner.hasTarget)
		{
			path = new NavMeshPath();
			//Get path of target
			if (agent.CalculatePath(owner.target.position, path))
			{
				//ckeck if path has completed calculating
				if (path.status == NavMeshPathStatus.PathComplete)
				{
					Vector3[] points = path.corners;
					//if there are points in the path
					if (points.Length > 0)
					{
						//get last node in array
						int lastNode = points.Length - 1;
						//select the minimum value of the twovalues
						currentNode = Mathf.Min(currentNode, lastNode);
						//gets the current point
						Vector3 currentPoint = points[currentNode];
						//check if its is the last node
						isAtTarget = currentNode == lastNode;
						//get the distance to current point
						float distanceTonode = Vector3.Distance(owner.transform.position, currentPoint);
						if (distanceTonode < nodeRadius)
						{
							currentNode++;
						}
						force = currentPoint - owner.transform.position;
					}
				}
			}
		}

		return force.normalized;
	}
}
