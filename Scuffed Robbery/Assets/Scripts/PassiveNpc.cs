using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PassiveNpc : MonoBehaviour
{
    bool isRunning;
    [SerializeField] NavMeshAgent agent;
    public bool isKeyDown = false;
    [SerializeField] GameObject parentWaypoints;
    [SerializeField] List<GameObject> wayPoints;
    [SerializeField] List<AnimationClip> animEvents;
    private int currentWaypointIndex = 0;
    [SerializeField] float min;
    [SerializeField] float max;
    Animation anim;
    Animator animator;
    bool animated;
    float velo;
    void Start()
    {
        animator = GetComponent<Animator>();
        anim = GetComponent<Animation>();
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
        
        if (animated)
        {
            anim.clip = animEvents[currentWaypointIndex];
            anim.Play();
            if (!anim.isPlaying)
            { 
             currentWaypointIndex = (currentWaypointIndex + 1) % wayPoints.Count;
              agent.SetDestination(wayPoints[currentWaypointIndex].transform.position);
            }
            
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
                if (animEvents[currentWaypointIndex] != null)
                {
                    animated = true;
                    yield return animated = false;
                    
                }
                else 
                {
                    currentWaypointIndex = (currentWaypointIndex + 1) % wayPoints.Count;
                    agent.SetDestination(wayPoints[currentWaypointIndex].transform.position);
                    Debug.Log("Next");
                }
                
            }
            yield return new WaitForSeconds(Random.Range(min, max));
        }
    }
}
