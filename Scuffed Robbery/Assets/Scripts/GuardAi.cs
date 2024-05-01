using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
public class GuardAi : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent;
        public bool isKeyDown;
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
        }

        if (isKeyDown)
        {
            agent.SetDestination(FindObjectOfType<PlayerMovement>().transform.position);
        }
        else if (!isKeyDown)
        {
            StartCoroutine(StartWayPoints());
        }
    }

    private IEnumerator StartWayPoints()
    {
        float closestDistance = 0;
        GameObject closestObject = null;
       foreach (GameObject obj in wayPoints)
       {
            // Calculate the distance between the object and the point
            float distance = Vector3.Distance(obj.transform.position, transform.position);

            // Check if this object is closer than the previously closest object
            if (distance < closestDistance)
            {
                closestObject = obj;
                closestDistance = distance;
            }
       }

        if (closestObject != null)
        {
            agent.SetDestination(closestObject.transform.position);
            yield return transform.position == closestObject.transform.position;
            Debug.Log("Yippe");
        }
            
    }
}
