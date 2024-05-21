using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
public class GuardAi : MonoBehaviour
{
    bool isRunning;
    [SerializeField] NavMeshAgent agent;
    public bool isKeyDown = false;
    [SerializeField] GameObject parentWaypoints;
    [SerializeField] List<GameObject> wayPoints;
    private int currentWaypointIndex = 0;
    [SerializeField] float min;
    [SerializeField] float max;
    Animator animator;
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
            animator.SetBool("walking", true);
        }
        if (agent.remainingDistance < 0.1f)
        {
            animator.SetBool("walking", false);

        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            isKeyDown = true;
        }


        if (Input.GetKeyUp(KeyCode.Q))
        {
            isKeyDown = false;
          
        }

        if (isKeyDown)
        {
            //  agent.SetDestination(FindObjectOfType<PlayerMovement>().transform.position);
        }
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
            if (!agent.pathPending && agent.remainingDistance < 0.1f)
            {
                currentWaypointIndex = (currentWaypointIndex + 1) % wayPoints.Count;
                agent.SetDestination(wayPoints[currentWaypointIndex].transform.position);
            }
            yield return new WaitForSeconds(Random.Range(min, max));
        }
    }
}

