using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tester : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {


       
        
        if (Input.GetKeyDown(KeyCode.E))
        {
            Vector3 directionToReference = FindObjectOfType<PlayerMovement>().transform.position - transform.position;

            // Get the dot product of the direction vector and this object's forward vector
            float dotProduct = Vector3.Dot(transform.forward, directionToReference);
            if (dotProduct > 0) 
            {
                Debug.Log("1");
            }
            else if (dotProduct < 0) 
            {
             Debug.Log("2");
            }
        }
        
    }
}
