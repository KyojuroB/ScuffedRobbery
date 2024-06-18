using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CustomerAi : MonoBehaviour
{
    public int index;
    public GameObject asssignedSeat;
    public GameObject leaveSeat;
    bool isTalking = false;
    Animator animator;
    NavMeshAgent agent;
    bool isRunning=false;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        agent.SetDestination(asssignedSeat.transform.position);

    }

    // Update is called once per frame
    void Update()
    {
        if (agent.remainingDistance > 0.2f)
        {
            animator.SetBool("Talking", false);
            animator.SetBool("walking", true);

        }
        if (agent.remainingDistance < 0.1f)
        {
         //   agent.Stop();
            animator.SetBool("walking", false);
            animator.SetBool("Talking", true);
            if (!isTalking&&!isRunning)
            {
                StartCoroutine(TalkingFunc());

            }

        }
        if (isTalking)
        {
            if (agent.remainingDistance < 1)
            {
                FindObjectOfType<CustomerSpawner>().RemoveCustomer(index);
                Destroy(gameObject);
            }

        }
    }
    IEnumerator TalkingFunc()
    {
        
        isRunning = true;
        gameObject.transform.localRotation = Quaternion.Euler(90, 0, 0);
        Debug.Log("Talking");
       
        yield return new WaitForSeconds(Random.Range(10,21));
        agent.SetDestination(leaveSeat.transform.position);
        isTalking = true;
    }
}
