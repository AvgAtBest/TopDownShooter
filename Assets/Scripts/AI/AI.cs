using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class AI : MonoBehaviour
{
	public Transform player;
	NavMeshAgent enemy;
	public bool hasTarget;
	public Transform target;
	public float maxVelocity = 15f;
	public float maxDistance = 10f;
	public SteeringBehaviour[] behaviours;
	public NavMeshAgent agent;
	protected Vector3 velocity;
	// Use this for intialization
	void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
		enemy = GetComponent<NavMeshAgent>();
		//loot = GameObject.FindGameObjectWithTag("Loot").GetComponent<Loot>();
		target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
	}


	//enemy.destination = player.transform.position;

	private void Awake()
	{
		agent = GetComponent<NavMeshAgent>();
	}

	private void OnDrawGizmosSelected()
	{
		Vector3 desiredPosition = transform.position + velocity * Time.deltaTime;
		Gizmos.color = Color.red;
		Gizmos.DrawSphere(desiredPosition, .1f);
		foreach (var behaviour in behaviours)
		{
			behaviour.OnDrawGizmosSelected(this);
		}
	}
	void Update()
	{
		//Step 1). Zero out velocity
		velocity = Vector3.zero;
		//Step 2). Loop through all behaviours and get forces
		foreach (var behaviour in behaviours)
		{
			float percentage = maxVelocity * behaviour.weighting;
			velocity += behaviour.GetForce(this) * percentage;
		}
		//Step 3). Limit Velocity to max velocity
		velocity = Vector3.ClampMagnitude(velocity, maxVelocity);
		//Step 4). Apply velocity to navmeshagent
		Vector3 desiredPos = transform.position + velocity * Time.deltaTime;
		NavMeshHit hit;
		if (NavMesh.SamplePosition(desiredPos, out hit, maxDistance, -1))
		{
			agent.SetDestination(hit.position);
		}
		
	}

}

