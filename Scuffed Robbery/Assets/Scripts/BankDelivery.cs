using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class BankDelivery : MonoBehaviour
{
 
    [SerializeField] Material material;
    [SerializeField] float maxRange;
    public Material[] mat;
    public List<Material> newmat;
    MeshRenderer meshR;
    bool mouseOver;
   
    bool isFinished = false;
  public  bool isOpen = false;
    Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
        meshR =gameObject.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>();
        mat = meshR.materials;
        newmat = mat.ToList();
        newmat.Add(material);
    }


    void Update()
    {



        if (mouseOver == true && Input.GetKeyDown(KeyCode.E))
        {
            if (!isOpen)
            {
                anim.SetBool("OpenDoor", true);
                isOpen = true;
               

            }
            else
            {
                anim.SetBool("OpenDoor", false);
                isOpen = false;
               
            }
             
            Debug.Log("w");
        }

       
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


        }
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
