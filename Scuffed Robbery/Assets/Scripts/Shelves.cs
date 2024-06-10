using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Shelves : MonoBehaviour
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
    bool isInfo = false;
    bool isFinished = false;

    void Start()
    {
        meshR = GetComponent<MeshRenderer>();
        mat = meshR.materials;
        newmat = mat.ToList();
        newmat.Add(material);
    }
    public void InfoTrue()
    {
        isInfo = true;
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
                    if (isInfo)
                    {
                        FindObjectOfType<GameStates>().hasShelveInfo = true;
                        Debug.Log("InfoOne");
                    }
                    if (realBar != null)
                    { 
                        Destroy(realBar.gameObject);
                    }
                    meshR.materials = mat;
                    Destroy(this);
                }

            }

        }
        if (mouseOver && Input.GetKeyDown(KeyCode.E) && !isRunning)
        {
            MouseAction();
            FindObjectOfType<GameStates>().Setillegal(true);
        }

  
        if (isRunning && !Input.GetKey(KeyCode.E))
        {
         
            Destroy(realBar.gameObject);
            isRunning = false;

        }

       
        if (mouseOver && Vector3.Distance(transform.position, FindObjectOfType<PlayerMovement>().transform.position) < maxRange && !isFinished)
        {
            meshR.materials = newmat.ToArray();
        }
        else
        {
            FindObjectOfType<GameStates>().Setillegal(false);
            meshR.materials = mat;
            if (realBar != null)
            {
                Destroy(realBar);
            }
        }
    }

    void MouseAction()
    {
        isRunning = true;
        realBar = Instantiate(uiBarPref);
        realBar.transform.SetParent(canvasObj.transform, false);
    

        Animator barAnimator = realBar.transform.Find("BarAnim").GetComponent<Animator>(); 


        if (barAnimator != null)
        {
            
            barAnimator.Play("Bar");
        }
        else
        {
           
            Debug.LogError("Animator component not found on the UI bar prefab.");
            Destroy(realBar);
            isRunning = false;
            return;
        }
    }

    private void OnMouseOver()
    {
        mouseOver = true;
        
        if (Vector3.Distance(transform.position, FindObjectOfType<PlayerMovement>().transform.position) < maxRange && !isFinished)
        {
          
            meshR.materials = newmat.ToArray();
        }
    }

    private void OnMouseExit()
    {
        FindObjectOfType<GameStates>().Setillegal(false);
        isRunning = false;
        mouseOver = false;
        meshR.materials = mat;
        if (realBar != null)
        {
            
            Transform barAnimChild = realBar.transform.Find("BarAnim");
            if (barAnimChild != null)
            {
              
                Destroy(barAnimChild.gameObject);
            }
            
            Destroy(realBar);
        }
    }
}
