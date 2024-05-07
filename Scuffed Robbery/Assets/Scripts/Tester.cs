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
        
        if (Physics.Linecast(transform.position, gobj.transform.position))
        {
            Debug.Log("blocked");
        }

    }
}
