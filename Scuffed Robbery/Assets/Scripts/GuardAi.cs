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
    [SerializeField] List<GameObject> wayPoints;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            isKeyDown = true;
        }


        if (Input.GetKeyUp(KeyCode.Q))
        {
            isKeyDown = false;
           StartWayPoints();
        }

        if (isKeyDown)
        {
            //  agent.SetDestination(FindObjectOfType<PlayerMovement>().transform.position);
        }
    }
    private void StartWayPoints()
    {
        int ammountOfTimesRan = 0;
        float closestDistance = 99999;
        int indexForCloset = 0;
        GameObject closestObject = null;

        Debug.Log("There are " + wayPoints.Count + " Waypoints");

        for (int i = 0; i < wayPoints.Count; i++)
        {

            float distance = Vector3.Distance(transform.position, wayPoints[i].transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestObject = wayPoints[i];
                indexForCloset = i;
            }

            ammountOfTimesRan++;
        }

        if (ammountOfTimesRan == wayPoints.Count)
        {
            agent.SetDestination(closestObject.transform.position);
            if (!agent.pathPending)
            {
                if (agent.remainingDistance <= agent.stoppingDistance)
                {
                    if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                    {


                    }
                    // Done



                    Debug.Log("Yippe");
                    StartCoroutine(PatrolWaypoints(indexForCloset));
                }
            }
        }
    }
    private IEnumerator PatrolWaypoints(int currentWaypoint)
    {
        Debug.Log(currentWaypoint + " Is are current waypoint");
        int nextWayPoint;
        if (currentWaypoint + 1 > wayPoints.Count)
        {
            nextWayPoint = 0;
        }
        else
        {
            nextWayPoint = currentWaypoint + 1;
        }
        Debug.Log(nextWayPoint + " Is are destination"); 



        agent.SetDestination(wayPoints[nextWayPoint].transform.position);
        yield return transform.position == wayPoints[nextWayPoint].transform.position;
        yield return new WaitForSeconds(Random.Range(4, 12));
        StartCoroutine(PatrolWaypoints(nextWayPoint + 1));
    }
}
