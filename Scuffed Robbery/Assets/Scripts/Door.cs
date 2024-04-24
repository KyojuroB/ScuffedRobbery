using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Door : MonoBehaviour
{
    Animator myAnim;
    bool inRange = false;
    [SerializeField] bool isKeyCard;
    [SerializeField] int keycardIndex;
    
    void Start()
    {
         myAnim = transform.GetChild(0).GetComponent<Animator>();
    }
    void Update()
    {
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
                
            }
            else 
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