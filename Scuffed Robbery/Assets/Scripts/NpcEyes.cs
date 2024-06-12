using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcEyes : MonoBehaviour
{
    public float viewRadius = 10f;
    public float viewAngle = 90f;
    public LayerMask targetMask;
    public LayerMask obstacleMask;
    public Transform eyePosition;
    ///The vars for the range and view agle ([;)
    public bool inView = false;
    public bool isRunningT;
  
    [SerializeField] GameObject investigationGroup;
    [SerializeField] GameObject realCamBar;
    [SerializeField] GameObject cameraBar;
  
    //Ray vars and stuff
    public Transform rayOrigin;
    Transform targetRay;
    private LineRenderer lineRenderer;

    private void Start()
    {
        targetRay = FindObjectOfType<PlayerMovement>().transform;
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
        lineRenderer.startWidth = 0.02f;
        lineRenderer.endWidth = 0.02f;
    }

    void Update()
    {
        FindVisibleTargets();
    }

    void FindVisibleTargets()
    {
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);

        foreach (Collider target in targetsInViewRadius)
        {
            Transform targetTransform = target.transform;
            Vector3 directionToTarget = (targetTransform.position - eyePosition.position).normalized;

            if (Vector3.Angle(eyePosition.forward, directionToTarget) < viewAngle / 2)
            {
                float distanceToTarget = Vector3.Distance(eyePosition.position, targetTransform.position);

                if (!Physics.Raycast(eyePosition.position, directionToTarget, distanceToTarget, obstacleMask))
                {

                    inView = true;
                    if (FindObjectOfType<GameStates>().inRestrictedArea)
                    {
                        if (FindObjectOfType<GameStates>().isDesguised)
                        {

                        }
                        else
                        {
                            gameObject.GetComponent<GuardAi>().isTrackingPlayer = true;
                            ShowRay();
                            if (!isRunningT)
                            {
                                
                                investigationGroup.SetActive(true);
                                realCamBar = Instantiate(cameraBar);
                                realCamBar.transform.SetParent(investigationGroup.transform, false);
                            }
                            isRunningT = true;

                            Vector3 direction = (targetRay.position - rayOrigin.position).normalized;


                            Vector3 endPosition = targetRay.position;

                            lineRenderer.SetPosition(0, rayOrigin.position);
                            lineRenderer.SetPosition(1, endPosition);


                        }
                    }
                    else
                    {                                     
                       DisableRay();                       
                    }
                    //Crime Noticedd idk do it late ig
                }
                else                      
                {
                    DisableRay();
                    inView = false;
                }
            }
            else
            {
                DisableRay();
                inView = false;
            }
        }
    }
        //How it works
        //
        //IF in view
    private void DisableRay()
    {
        isRunningT = false;
        lineRenderer.enabled = false;
        Destroy(realCamBar);
        if (investigationGroup.transform.childCount == 0)
        {
            investigationGroup.SetActive(false);
        }
    }
    private void ShowRay()
    {
        lineRenderer.enabled = true;
    }



    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, viewRadius);//Draws the actual sphere thingy thing (0_0)
        Vector3 fovLine1 = Quaternion.AngleAxis(viewAngle / 2, transform.up) * transform.forward * viewRadius;
        Vector3 fovLine2 = Quaternion.AngleAxis(-viewAngle / 2, transform.up) * transform.forward * viewRadius;
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(eyePosition.position, eyePosition.position + fovLine1);
        Gizmos.DrawLine(eyePosition.position, eyePosition.position + fovLine2);
    }
}
