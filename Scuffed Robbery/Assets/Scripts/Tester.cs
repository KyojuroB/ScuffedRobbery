using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Tester : MonoBehaviour
{
    [SerializeField]GameObject gobj;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(Physics.Linecast(transform.position, gobj.transform.position, 6));
        if (Physics.Linecast(transform.position, gobj.transform.position ,6))
        {
            Debug.Log("blocked");
        }

    }
}
