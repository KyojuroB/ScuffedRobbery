using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestrictedArea : MonoBehaviour
{
    [SerializeField] GameObject uiWarning;
    public bool isInside = false;
    private int triggerCount = 0;
    MeshRenderer mR;

    private void Start()
    {

        mR = GetComponent<MeshRenderer>();
        mR.enabled = false;
        uiWarning.SetActive(false);
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.T))
        {
            mR.enabled = true;
        }
        else
        {
            mR.enabled = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            triggerCount++;
            if (triggerCount > 0)
            {
                FindObjectOfType<GameStates>().setRestricted(true);
                uiWarning.SetActive(true);
                isInside = true;
                
            }
            Debug.Log("Entered the trigger area.");
        }



    }

    // Called when another collider exits the trigger
    private void OnTriggerExit(Collider other)
    {

        if (other.CompareTag("Player")) 
        {
            triggerCount--;
            if (triggerCount <= 0)
            {
                FindObjectOfType<GameStates>().setRestricted(false);
                isInside = false;
                uiWarning.SetActive(false);
            }
            Debug.Log("Exited the trigger area.");
        }
    }
}   
