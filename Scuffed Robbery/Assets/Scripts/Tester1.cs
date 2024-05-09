using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Tester1 : MonoBehaviour
{

    [SerializeField] GameObject canvasObj;
    [SerializeField] Material material;
    [SerializeField] GameObject uiBarPref;
    GameObject realBar;
    [SerializeField] float maxRange;
    public Material[] mat;
    public List<Material> newmat;
    MeshRenderer meshR;
    bool mouseOver;
    bool isRunning;
    // Start is called before the first frame update
    void Start()
    {
        meshR = GetComponent<MeshRenderer>();
        mat = meshR.materials;
        newmat = meshR.materials.ToList();
        newmat.Add(material);
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        if (Vector3.Distance(transform.position, FindObjectOfType<PlayerMovement>().transform.position) > maxRange)
        {
            OnMouseExit();
        }
    }
    private void OnMouseOver()
    {
        mouseOver = true;   
        if (Vector3.Distance(transform.position, FindObjectOfType<PlayerMovement>().transform.position) < maxRange)
        {
            meshR.materials = newmat.ToArray();
            if (Input.GetKeyDown(KeyCode.E))
            {
                if(!isRunning)
                {
                    
                    MouseAction();
                }
         
            }
        }
    }
    void  MouseAction()
    {
        isRunning = true;
        realBar = Instantiate(uiBarPref);
        realBar.transform.SetParent(canvasObj.transform, false);
        if (!Input.GetKey(KeyCode.E))
        {
            Destroy(realBar.gameObject);
            isRunning = false;
            return;
        }
        if (mouseOver == false)
        {
            isRunning = false;
            return;
        }
        if (!realBar.transform.GetChild(0).transform.GetChild(0).GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Bar")) 
        {
            Destroy(realBar.gameObject);
            isRunning = false;
            return;
        }

    }
    private void OnMouseExit()
    {
        mouseOver = false;
        meshR.materials = mat;
        if (realBar != null)
        {
            Destroy(realBar.gameObject);
        }
    }
}
