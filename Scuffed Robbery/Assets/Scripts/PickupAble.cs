using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.XR;

public class PickupAble : MonoBehaviour
{
    [SerializeField] int objIndex;   
    [SerializeField] float maxRange;
    bool mouseOver = false;
    bool shaded = false;
    [SerializeField] GameObject tintObj;
    [SerializeField] GameObject pickUpText;
    [SerializeField] GameObject TextPrefab;
    [SerializeField] Canvas canvasObj;
    [SerializeField] AudioClip pickUpSound;
   
    void Start()
    {

       canvasObj = FindObjectOfType<Canvas>();
        tintObj.SetActive(false);
       // childText.enabled = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && Vector3.Distance(transform.position, FindObjectOfType<PlayerMovement>().transform.position) < maxRange && mouseOver)
        {
            if (FindObjectOfType<Inventory>().IsInventorySpace() == true) 
            {
                FindObjectOfType<Inventory>().AddToInventory(objIndex);
                if (objIndex == 7)
                {
                    FindObjectOfType<FinishArea>().completeTask(1);
                }
                if (objIndex == 2)
                {
                    FindObjectOfType<Narrator>().hasCard = true;
                    FindObjectOfType<Narrator>().CheckForCardAndUni();

                }
                if (objIndex == 4)
                {
                    FindObjectOfType<Narrator>().CreateText("Alright make your way to the basement and find the server, you'll know it when you see it.",7);
                }
                AudioSource.PlayClipAtPoint(pickUpSound, transform.position);
                Destroy(pickUpText);
                Destroy(gameObject);
            }
           
            
        }
        if (mouseOver && Vector3.Distance(transform.position, FindObjectOfType<PlayerMovement>().transform.position) < maxRange && !shaded)
        {
            tintObj.SetActive(true);
            pickUpText = Instantiate(TextPrefab);
            pickUpText.transform.SetParent(canvasObj.transform, false);
            shaded = true;
        }
        else if(!mouseOver || Vector3.Distance(transform.position, FindObjectOfType<PlayerMovement>().transform.position) > maxRange)
        {
            
            tintObj.SetActive(false);
            Destroy(pickUpText);
            shaded = false;
        }
    }

    private void OnMouseOver()
    {
        mouseOver = true;

    }

    private void OnMouseExit()
    {
        mouseOver = false;
    }
}
