using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.ProBuilder.AutoUnwrapSettings;

public class GuardAi : MonoBehaviour
{
    bool isRunning;
    [SerializeField] NavMeshAgent agent;
    public bool isfollow;
    [SerializeField] GameObject parentWaypoints;
    [SerializeField] List<GameObject> wayPoints;
    private int currentWaypointIndex = 0;
    [SerializeField] float min;
    [SerializeField] float max;
    Animator animator;
    public bool isTrackingPlayer;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        for (int i = 0; i < parentWaypoints.transform.childCount; i++)
        {
           wayPoints.Add(parentWaypoints.transform.GetChild(i).gameObject);
        }

        StartWayPoints();
    }

    // Update is called once per frame
    void Update()
    {
        if (agent.remainingDistance > 0.2f)
        {
            animator.SetBool("idle", false);
            animator.SetBool("walking", true);

        }
        if (agent.remainingDistance < 0.1f)
        {
        
            animator.SetBool("walking", false);
            animator.SetBool("idle", true);

        }


        if (isTrackingPlayer)
        {
            agent.ResetPath();

            if (Vector3.Distance(transform.position, FindObjectOfType<PlayerMovement>().transform.position) < 4)
            {
                 transform.LookAt(FindObjectOfType<PlayerMovement>().transform, Vector3.up);
            }
            else
            {
                var lookPos = FindObjectOfType<PlayerMovement>().transform.position - transform.position;
                lookPos.y = 0;
                var rotation = Quaternion.LookRotation(lookPos);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime);
            }
      
            
       
             
        }
        if (isTrackingPlayer && (agent.remainingDistance < 1))
        { 
           // isTrackingPlayer = false;
        }
    }
    private void FaceTarget(Vector3 destination)
    {
        Vector3 lookPos = destination - transform.position;
        lookPos.y = 0;
        Quaternion rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 5);
    }
    private void StartWayPoints()
    {
        float closestDistance = Mathf.Infinity;
        GameObject closestObject = null;

        foreach (GameObject waypoint in wayPoints)
        {
            float distance = Vector3.Distance(transform.position, waypoint.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestObject = waypoint;
            }
        }

        agent.SetDestination(closestObject.transform.position);
        StartCoroutine(PatrolWaypoints());
    }

    private IEnumerator PatrolWaypoints()
    {
        while (true)
        {
            if (!agent.pathPending && agent.remainingDistance < 0.1f && !isTrackingPlayer)
            {
                currentWaypointIndex = (currentWaypointIndex + 1) % wayPoints.Count;
                agent.SetDestination(wayPoints[currentWaypointIndex].transform.position);
            }
            yield return new WaitForSeconds(Random.Range(min, max));
        }
    }
}

