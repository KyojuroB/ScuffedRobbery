using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecurityCam : MonoBehaviour
{
    bool isInArea;
    MeshRenderer mR;
    // Start is called before the first frame update
    void Start()
    {

        mR = GetComponent<MeshRenderer>();
        mR.enabled = false;
    }

    // Update is called once per frame
    void Update()
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
