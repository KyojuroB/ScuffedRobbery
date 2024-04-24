using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UIElements;

public class Inventory : MonoBehaviour
{
    [Header("organization")]
    [SerializeField] GameObject selector;
    [SerializeField] int selectorPos;
    [SerializeField] GameObject inventoryGroup;

    [Header("Alll Lists")]
    [SerializeField] List<GameObject> actualSlots;
    [SerializeField] List<Sprite> itemPic;
    [SerializeField] List<Image> itemPicInSlot;
    [SerializeField] List<int> objInInv;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < inventoryGroup.transform.childCount; i++)
        {
            actualSlots.Add(inventoryGroup.transform.GetChild(i).gameObject);
            objInInv.Add(0);
        }
        for (int i = 0; i < actualSlots.Count; i++)
        {
            actualSlots[i].transform.GetChild(0).GetComponent<Image>();
        }
    }

    public bool IsEquiped(int objIndex)
    {
        for (int i = 0; i < objInInv.Count; i++)
        {
            if (objInInv[i] ==objIndex)
            {
                Debug.Log("we got it");
               
                return true;
            }
        }
        
         return false;
    }
    public bool IsInventorySpace()
    {
        for (int i = 0; i < objInInv.Count; i++)
        {
            if (objInInv[i] == 0)
            {
                Debug.Log("slot " + i + " Is open");
                return true;               
            }
        }
        
        return false;
    }


  
        
    public void AddToInventory(int indexCode)
    {
        for (int i = 0; i < objInInv.Count; i++)
        {
            if (objInInv[i] == 0)
            {
                objInInv[i] = indexCode;
                //itemPicInSlot[i].image = itemPic[i];
                i = objInInv.Count;
            }
         
            
        }
    }
    void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (selectorPos+ 1 > 5)
            {
                selectorPos = 0;
               // return;
            }
            else 
            {
                selectorPos++;
            }
            selector.transform.position = actualSlots[selectorPos].transform.position;
          
        }
        else if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (selectorPos-1 < 0)
            {
                selectorPos = 5;
               // return;
            }
            else
            {
                selectorPos--;
            }
            selector.transform.position = actualSlots[selectorPos].transform.position;
        }

    }
}
