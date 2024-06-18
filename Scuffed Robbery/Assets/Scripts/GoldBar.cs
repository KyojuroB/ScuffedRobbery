using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GoldBar : MonoBehaviour
{
    [SerializeField] GameObject canvasObj;
    [SerializeField] GameObject uiBarPref;
    GameObject realBar;
    [SerializeField] float maxRange;
    MeshRenderer meshR;
    bool mouseOver;
    bool isRunning;
    bool isFinished = false;

    void Start()
    {
        meshR = GetComponent<MeshRenderer>();
        meshR.enabled = false;
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
                    FindObjectOfType<FinishArea>().completeTask(3);
                    FindObjectOfType<GameStates>().collectGold();
                    Debug.Log("Done");
                    
                    if (realBar != null)
                    {
                        Destroy(realBar.gameObject);
                    }
                    meshR.enabled = false;
                    Destroy(gameObject);
                }

            }

        }
        if (mouseOver && Input.GetKeyDown(KeyCode.E) && !isRunning)
        {
            MouseAction();
        }


        if (isRunning && !Input.GetKey(KeyCode.E))
        {
            Destroy(realBar.gameObject);
            isRunning = false;
        }


        if (mouseOver && Vector3.Distance(transform.position, FindObjectOfType<PlayerMovement>().transform.position) < maxRange && !isFinished)
        {
            meshR.enabled = true;
        }
        else
        {

            meshR.enabled = false;
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
            meshR.enabled = true;
        }
    }

    private void OnMouseExit()
    {
        isRunning = false;
        mouseOver = false;
        meshR.enabled = false;
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
