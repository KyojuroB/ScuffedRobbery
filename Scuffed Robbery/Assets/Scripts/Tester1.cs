using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

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
    bool isRunning = false;
    bool isFinished = false;

    void Start()
    {
        meshR = GetComponent<MeshRenderer>();
        mat = meshR.materials;
        newmat = mat.ToList();
        newmat.Add(material);
    }
 

    void Update()
    {
        if (isRunning)
        {
            if (realBar != null)
            {
                if (realBar.transform.Find("BarAnim").GetComponent<Image>().fillAmount == 1)
                {
                    isFinished = true;
                    Debug.Log("Done");
                
                    if (realBar != null)
                    {
                        Destroy(realBar.gameObject);
                    }
                    meshR.materials = mat;
                    Destroy(this);
                }

            }

        }


        if (mouseOver == true && Input.GetKeyDown(KeyCode.E)&& !isRunning)
        {
           
            isRunning = true;
            realBar = Instantiate(uiBarPref);
            realBar.transform.SetParent(canvasObj.transform, false);
            Debug.Log("w");
        }

        // Change material if mouse is over and within range
        if (mouseOver && Vector3.Distance(transform.position, FindObjectOfType<PlayerMovement>().transform.position) < maxRange && !isFinished)
        {
            mouseOver = true;
            meshR.materials = newmat.ToArray();
        }
        else
        {
            // Revert to original material 
            mouseOver = false;
            meshR.materials = mat;
            if (realBar != null) ;
       
        }
    }

    void MouseAction()
    {

    }

    private void OnMouseOver()
    {
       

        if (Vector3.Distance(transform.position, FindObjectOfType<PlayerMovement>().transform.position) < maxRange && !isFinished)
        {
            mouseOver = true;
            meshR.materials = newmat.ToArray();
        }
    }

    private void OnMouseExit()
    {
  
        mouseOver = false;
        meshR.materials = mat;

    }
}


