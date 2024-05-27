using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Tester1 : MonoBehaviour
{
    public float viewRadius = 10f;
    public float viewAngle = 90f;
    public LayerMask targetMask;
    public LayerMask obstacleMask;
    public Transform eyePosition;

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
                    
                    OnTargetVisible(targetTransform);
                }
            }
        }
    }

    void OnTargetVisible(Transform target)
    {
        Debug.Log("Target in sight: " + target.name);
    }

   
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, viewRadius);
        Vector3 fovLine1 = Quaternion.AngleAxis(viewAngle / 2, transform.up) * transform.forward * viewRadius;
        Vector3 fovLine2 = Quaternion.AngleAxis(-viewAngle / 2, transform.up) * transform.forward * viewRadius;
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(eyePosition.position, eyePosition.position + fovLine1);
        Gizmos.DrawLine(eyePosition.position, eyePosition.position + fovLine2);
    }
}


