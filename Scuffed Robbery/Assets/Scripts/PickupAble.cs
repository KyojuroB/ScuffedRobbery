using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PickupAble : MonoBehaviour
{
    [SerializeField] int objIndex;
    bool inRange = false;
    [SerializeField] TextMeshPro childText;
    void Start()
    {
        childText.enabled = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            childText.enabled = true;
            inRange = true;
        
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            childText.enabled = false;
            inRange = false;
      

        }
    }
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && inRange)
        {
            if (FindObjectOfType<Inventory>().IsInventorySpace() == true) 
            {
                FindObjectOfType<Inventory>().AddToInventory(objIndex);
                Destroy(gameObject);
            }
           
            
        }
    }
}
