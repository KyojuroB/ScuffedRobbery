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
                    if (myAnim.GetBool("isOpen") == true)
                    {
                        
                        myAnim.SetBool("isOpen", false);
                    }
                    else if (myAnim.GetBool("isOpen") == false)
                    {
                        myAnim.SetBool("isOpen", true);
                    }
                }                
            }///////////
            else 
            {
               // if (transform.position.y +FindAnyObjectByType<PlayerMovement>().transform.position. <)
              //  { 
                
              //  }
             if (isOpen == true)
             {
                 myAnim.SetBool("isOpen", false);
             }
             else if (isOpen == false) 
             {
                myAnim.SetBool("isOpen", true);
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