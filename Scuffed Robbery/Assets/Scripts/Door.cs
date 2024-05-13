using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class Door : MonoBehaviour
{
    Animator myAnim;
    bool inRange = false;
    [SerializeField] bool isKeyCard;
    [SerializeField] int keycardIndex;
    bool isOpen = false;
    Axis direction = Axis.Y;


    void Start()
    {
         myAnim = transform.GetChild(0).GetComponent<Animator>();
    }
    void Update()
    {
        //if ())
        





        if (inRange&&Input.GetKeyDown(KeyCode.E))
        {
            if (isKeyCard)
            {
                if (FindObjectOfType<Inventory>().IsEquiped(keycardIndex))
                {
                    if (isOpen)
                    {
                        myAnim.SetTrigger("Close");
                        // myAnim.ResetTrigger("Close");
                        isOpen = false;
                    }
                    else
                    {

                        Vector3 directionToReference = FindObjectOfType<PlayerMovement>().transform.position - transform.position;
                        float dotProduct = Vector3.Dot(transform.forward, directionToReference);
                        if (dotProduct > 0) 
                        {
                            myAnim.SetTrigger("Open2");
                        }
                        else if (dotProduct < 0)
                        {
                            myAnim.SetTrigger("Open1");
                        }
                        isOpen = true;
                    }
                }                
            }///////////
                                  
                                                               
            else 
            {
                if (isOpen)
                {
                    myAnim.SetTrigger("Close");
                   // myAnim.ResetTrigger("Close");
                   isOpen = false;
                }
                else
                {
                    
                    Vector3 directionToReference = FindObjectOfType<PlayerMovement>().transform.position - transform.position;
                    float dotProduct = Vector3.Dot(transform.forward, directionToReference);
                    if (dotProduct > 0)
                    {
                        myAnim.SetTrigger("Open2");
                    }
                    else if (dotProduct < 0)
                    {
                        myAnim.SetTrigger("Open1");
                    }
                    isOpen = true;
                }
            }    
        }
    }   
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        { 
                  inRange = true;
            Debug.Log("In");
        }
    
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            inRange = false;
            Debug.Log("Out");
        }
    }

    
}