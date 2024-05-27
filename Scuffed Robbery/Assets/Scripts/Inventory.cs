using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;



public class Inventory : MonoBehaviour
{
    [Header("organization")]
    [SerializeField] GameObject selector;
    [SerializeField] int selectorPos;
    [SerializeField] GameObject inventoryGroup;
    [SerializeField] Sprite uiMask;

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
            itemPicInSlot.Add(actualSlots[i].transform.GetChild(0).gameObject.GetComponent<Image>());
        }
    }  
    public bool IsEquiped(int objIndex)
    {

        if (objInInv[selectorPos] == objIndex)
        {
           

            return true;
        }
        else 
        {
            return false;
        }
               
         
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
                itemPicInSlot[i].sprite = itemPic[indexCode];
                objInInv[i] = indexCode;
                
                i = objInInv.Count;
            }           
        }
    }
    public void RemoveFromInventory(int objIndex) 
    {
        for (int i = 0; i < objInInv.Count; i++)
        {
            if (objInInv[i] == objIndex)
            {
                itemPicInSlot[i].sprite = uiMask;
                objInInv[i] = 0;
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
