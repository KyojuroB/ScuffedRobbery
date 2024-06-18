using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerNpc : MonoBehaviour
{
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
       anim = GetComponent<Animator>();
        anim.SetBool("Typing", true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
