using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossAI : MonoBehaviour
{
    #region VARIABLES
    // Declaration
    public enum State // The behaviour states of the enemy AI.
    {
        Patrol,
        Vent,
        Engage,

    }

    [Header("Components")]
    public NavMeshAgent agent; // Unity component reference
    public Transform waypointParent; // Reference one waypoint Parent (used to get children in array).
    public BossFoV_SearchLight fov; // Reference FieldOfView Script (used for line of sight player detection).
    public GameObject vent;

    [Header("Behaviours")]
    public State currentState = State.Patrol; // The default/start state set to Patrol.
    public bool openVent = false;
    public float speedPatrol = 4f, speedSeek = 4f; // Movement speeds for different states (up to you).
    public float stoppingDistance = 1f; // Enemy AI's required distance to clear/'pass' a waypoint.

    public Vector3 targetPos;
    public float pauseDuration; // Time to wait before going to the next waypoint.
    public float waitTime, lookTime; // Defined later as UnityEngine 'Time.time'.
    private BossHealth bossHP;
    // Creates a collection of Transforms
    private Transform[] waypoints; // Transform of (child) waypoints in array.
    private int currentIndex = 1; // Counts sequential waypoints of array index.

    public GameObject laser;

    #endregion VARIABLES

    #region Patrol
    void Patrol()
    {
        openVent = false;
        waitTime = pauseDuration;
        // Transform(s) of each waypoint in the array.
        Transform point = waypoints[currentIndex];
        agent.speed = speedPatrol; // NavMeshAgent movement speed during patrol.

        // Gets the distance between enemy and waypoint.
        float distance = Vector3.Distance(new Vector3 (transform.position.x,0,transform.position.z), new Vector3(point.position.x, 0, point.position.z));
        #region if statement logic
        // if statement reads as:
        /*
         *  if the enemy AI's distance to the waypoint is less than 0.5...
         *      and (&& breaks in previous argument) if curTime's equality is 0...
         *          curTime = Time(using Unity Engine's time).time(get the time at beginning of this frame in seconds since the start of the game).
         *      
         *      if the time is greater than or equal to the pauseDuration...
         *          add +1 to currentIndex (move to next waypoint in array).
         *          reset curTime time to 0.
         *          
         *          if enemy AI clears the final waypoint in array...
         *              reset currentIndex to 1 (return/repeat cycle).
        */
        #endregion
        if (distance < .5f)
        {

            currentState = State.Vent;

        }
        agent.SetDestination(point.position); // (NavMeshAgent) agent: move to the Transform position of current waypoint.

        // Gets the distance between enemy and player.
        //float distToTarget = Vector3.Distance(transform.position, targetPos);

        /*if (fov.visibleTargets.Count > 0 || bossHP.curHealth < bossHP.maxHealth)
        {
            currentState = State.Engage;
            target = fov.visibleTargets[0];
        }*/

        //fov.viewRadius = 6f; // FieldOfView arc radius during 'Patrol'.
    }
    #endregion

    #region Vent
    public void Vent()
    {
        openVent = true;
        waitTime -= Time.deltaTime;
        if (waitTime <= 0)
        {
            currentIndex++;
            if (currentIndex >= waypoints.Length)
            {
                currentIndex = 1;
            }
            currentState = State.Patrol;
        }
    }
    #endregion

    #region Engage
    public void Engage()
    {
        
        openVent = false;
        waitTime = pauseDuration;
        agent.SetDestination(targetPos);
        
        float distance = Vector3.Distance(transform.position, targetPos);
        if (distance < 50)
        {
            agent.speed--;
            lookTime -= Time.deltaTime;
            if (lookTime <= 0)
            {
                currentState = State.Patrol;
                lookTime = pauseDuration;
            }
        }
        else {agent.speed = speedSeek; }
    }
    #endregion
    public void Start()
    {
        waypoints = waypointParent.GetComponentsInChildren<Transform>();
        currentState = State.Patrol;
        openVent = false;
        waitTime = pauseDuration;
        lookTime = pauseDuration;
    }
    public void Update()
    {
        switch (currentState)
        {
            case State.Patrol:
                // Patrol state
                Patrol();
                break;
            case State.Vent:
                // Vent state
                Vent();
                break;
            case State.Engage:
                // Seek state
                Engage();
                break;

            default:
                break;
        }
       // if (openVent) { vent.SetActive(false); }
       // else { vent.SetActive(true); }
       //// if (currentState == State.Engage) { laser.GetComponent<LaserCannon>().enabled = true; }
       // else
       // {
       //     //laser.GetComponent<LaserCannon>().enabled = false;
       //     laser.transform.localRotation = Quaternion.Euler(0,0,0);
       // }
    }
    public void Receive(Vector3 playerPos)
    {
        targetPos = playerPos;
        currentState = State.Engage;
    }
}
