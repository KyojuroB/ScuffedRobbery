using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ServerHack : MonoBehaviour
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
    [SerializeField] int toolIndex;
    [SerializeField] GameObject noToolText;
    [SerializeField] GameObject FullUSB;
    [SerializeField] GameObject instPos;
    [SerializeField] AudioClip insetAudio;

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
                    FindObjectOfType<Narrator>().CreateText("Good, head back up and find where they keep all the files, we might be able to find things out.", 7);
                    AudioSource.PlayClipAtPoint(insetAudio, transform.position);
                    isFinished = true;
                    Instantiate(FullUSB, instPos.transform.position, Quaternion.identity);
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


        if (mouseOver == true && Input.GetKeyDown(KeyCode.E) && !isRunning)
        {
            if (FindObjectOfType<Inventory>().IsEquiped(toolIndex))
            {
                FindObjectOfType<Inventory>().RemoveFromInventory(toolIndex);
                isRunning = true;
                realBar = Instantiate(uiBarPref);
                realBar.transform.SetParent(canvasObj.transform, false);
                Debug.Log("w");
            }
            else 
            {
                var text = Instantiate(noToolText);
                text.transform.SetParent(canvasObj.transform, false);
                StartCoroutine(DelText(text));
            }

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
           

        }
    }
    IEnumerator DelText(GameObject text)
    {
        yield return new WaitForSeconds(3);
        Destroy(text);
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
