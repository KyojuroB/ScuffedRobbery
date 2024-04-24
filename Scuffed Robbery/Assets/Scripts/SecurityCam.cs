using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecurityCam : MonoBehaviour
{
    RaycastHit hit;
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
        for (float i = 0; i < 360; i++)
        {
            Debug.DrawRay(this.gameObject.transform.position, Quaternion.Euler(0, i, 0) * this.gameObject.transform.forward * 2, Color.red, 10);
            if (Physics.Raycast(this.gameObject.transform.position, Quaternion.Euler(0, i, 0) * this.gameObject.transform.forward * 2, out hit, 10))
            {
                if (hit.transform.gameObject.tag == "Player")
                {
                    Debug.Log("Holy fucking shit it worked");
                }
            }
        }
    }

}
