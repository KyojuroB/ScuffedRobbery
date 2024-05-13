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
                    Debug.Log("Done");
                }

            }

        }
        if (mouseOver && Input.GetKeyDown(KeyCode.E) && !isRunning)
        {
            MouseAction();
        }

        // Check for input to stop the action
        if (isRunning && !Input.GetKey(KeyCode.E))
        {
            Destroy(realBar.gameObject);
            isRunning = false;
        }

        // Change material if mouse is over and within range
        if (mouseOver && Vector3.Distance(transform.position, FindObjectOfType<PlayerMovement>().transform.position) < maxRange)
        {
            meshR.materials = newmat.ToArray();
        }
        else
        {
            // Revert to original material if not within range or mouse is not over
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

        // Find the child object that contains the Animator component
        Animator barAnimator = realBar.transform.Find("BarAnim").GetComponent<Animator>(); // Replace "BarAnim" with the actual name of the child object


        if (barAnimator != null)
        {
            // Play the "Bar" animation if Animator component is found
            barAnimator.Play("Bar");
        }
        else
        {
            // Handle the case where Animator component is not found
            Debug.LogError("Animator component not found on the UI bar prefab.");
            Destroy(realBar);
            isRunning = false;
            return;
        }
    }

    private void OnMouseOver()
    {
        mouseOver = true;
        // Check distance and change material if within range
        if (Vector3.Distance(transform.position, FindObjectOfType<PlayerMovement>().transform.position) < maxRange)
        {
            meshR.materials = newmat.ToArray();
        }
    }

    private void OnMouseExit()
    {
        isRunning = false;
        mouseOver = false;
        meshR.materials = mat;
        if (realBar != null)
        {
            // Check if the realBar has a child object named "BarAnim"
            Transform barAnimChild = realBar.transform.Find("BarAnim");
            if (barAnimChild != null)
            {
                // Destroy the child object
                Destroy(barAnimChild.gameObject);
            }
            // Then destroy the realBar itself
            Destroy(realBar);
        }
    }
}
