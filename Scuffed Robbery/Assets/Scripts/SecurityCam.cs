using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecurityCam : MonoBehaviour
{
    bool isInArea;
    // Start is called before the first frame update
    void Start()
    {
        
      
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void FixedUpdate()
    {
 
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == ("Player"))
        { 
            isInArea = true;
            StartCoroutine(CamActivation());
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == ("Player"))
        {
            isInArea = false;
        }
    }
    private IEnumerator CamActivation()
    {

        Debug.Log("Get OFFF MY LAAAAANNDD");
        yield return isInArea = false;

    }

}
