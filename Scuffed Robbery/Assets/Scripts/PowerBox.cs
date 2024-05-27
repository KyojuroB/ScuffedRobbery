using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
public class PowerBox : MonoBehaviour
{
    [SerializeField] GameObject wallcol;
    [SerializeField] GameObject noToolText;
    [SerializeField] Animator parkingGate1;
    [SerializeField] Animator parkingGate2;
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
    [SerializeField] int intForTool;
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
                    parkingGate1.enabled = true; 
                    parkingGate2.enabled = true;
                    isFinished = true;
                    Destroy(wallcol.gameObject);
                    Debug.Log("Done");
                    meshR.materials = mat;
                    Destroy(this);
                }

            }

        }
        if (mouseOver && Input.GetKeyDown(KeyCode.E) && !isRunning && Vector3.Distance(transform.position, FindObjectOfType<PlayerMovement>().transform.position) < maxRange)
        {
            if (FindObjectOfType<Inventory>().IsEquiped(intForTool))
            {
                MouseAction();
            }
            else
            {
                var text = Instantiate(noToolText);
                text.transform.SetParent(canvasObj.transform, false);
                StartCoroutine(DelText(text));
            }


        }
        if (isRunning && !FindObjectOfType<Inventory>().IsEquiped(intForTool)) 
        {
            Destroy(realBar.gameObject);
            isRunning = false;
        }
        // Check for input to stop the action
        if (isRunning && !Input.GetKey(KeyCode.E))
        {
            Destroy(realBar.gameObject);
            isRunning = false;
        }

        // Change material if mouse is over and within range
        if (mouseOver && Vector3.Distance(transform.position, FindObjectOfType<PlayerMovement>().transform.position) < maxRange && !isFinished)
        {
            meshR.materials = newmat.ToArray();
        }
        else
        {
            // Revert to original material 
            meshR.materials = mat;
            if (realBar != null)
            {
                Destroy(realBar);
            }
        }
    }
    IEnumerator DelText(GameObject text)
    {
        yield return new WaitForSeconds(3);
        Destroy(text);
    }

    void MouseAction()
    {
        isRunning = true;
        realBar = Instantiate(uiBarPref);
        realBar.transform.SetParent(canvasObj.transform, false);


        Animator barAnimator = realBar.transform.Find("BarAnim").GetComponent<Animator>();


        if (barAnimator != null)
        {

          //  barAnimator.Play("Bar");
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
