using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.ProBuilder;
using UnityEngine.XR;

public class Door : MonoBehaviour
{
    Animator myAnim;
    [SerializeField] float range;
    [SerializeField] bool isKeyCard;
    [SerializeField] int keycardIndex;
    bool isOpen = false;
    [SerializeField] GameObject noKeyText;
    [SerializeField] GameObject canvasObj;
    [SerializeField] Material material;
    public Material[] mat;
    public List<Material> newmat;
    [SerializeField] MeshRenderer meshR;
    public bool mouseOver;

    [SerializeField] AudioClip openAudio;
    [SerializeField] AudioClip closeAudio;
    [SerializeField] AudioClip deneidAudio;
    [SerializeField] AudioClip grantedAudio;
    AudioSource audio;

    void Start()
    {
        audio = GetComponent<AudioSource>();
        mat = meshR.materials;
        newmat = mat.ToList();
        newmat.Add(material);
        myAnim = transform.parent.GetComponent<Animator>();
    }

    void Update()
    {
        if (mouseOver && Vector3.Distance(transform.position, FindObjectOfType<PlayerMovement>().transform.position) < range)
        {
            meshR.materials = newmat.ToArray();
        }
        else
        {
            meshR.materials = mat;
        }

        if (Vector3.Distance(transform.position, FindObjectOfType<PlayerMovement>().transform.position) < range && Input.GetKeyDown(KeyCode.E) && mouseOver)
        {
            if (isKeyCard)
            {
                if (FindObjectOfType<Inventory>().IsEquiped(keycardIndex))
                {
                    if (isOpen)
                    {
                        audio.PlayOneShot(closeAudio);
                        myAnim.SetTrigger("Close");
                        isOpen = false;
                    }
                    else
                    {
                        Vector3 directionToReference = FindObjectOfType<PlayerMovement>().transform.position - transform.position;
                        float dotProduct = Vector3.Dot(transform.forward, directionToReference);
                        if (dotProduct > 0)
                        {
                            audio.PlayOneShot(grantedAudio);
                            audio.PlayOneShot(openAudio);
                            myAnim.SetTrigger("Open2");
                        }
                        else if (dotProduct < 0)
                        {
                            audio.PlayOneShot(grantedAudio);
                            audio.PlayOneShot(openAudio);
                            myAnim.SetTrigger("Open1");
                        }
                        isOpen = true;
                    }
                }
                else
                {
                    audio.PlayOneShot(deneidAudio);
                    var text = Instantiate(noKeyText);
                    text.transform.SetParent(canvasObj.transform, false);
                    StartCoroutine(DelText(text));
                }
            }
            else
            {
                if (isOpen)
                {
                    audio.PlayOneShot(closeAudio);
                    myAnim.SetTrigger("Close");
                    isOpen = false;
                }
                else
                {
                    Vector3 directionToReference = FindObjectOfType<PlayerMovement>().transform.position - transform.position;
                    float dotProduct = Vector3.Dot(transform.forward, directionToReference);
                    if (dotProduct > 0)
                    {
                        audio.PlayOneShot(openAudio);
                        myAnim.SetTrigger("Open2");
                    }
                    else if (dotProduct < 0)
                    {
                        audio.PlayOneShot(openAudio);
                        myAnim.SetTrigger("Open1");
                    }
                    isOpen = true;
                }
            }
        }
    }

    IEnumerator DelText(GameObject text)
    {
        yield return new WaitForSeconds(3);
        Destroy(text);
    }

    private void OnMouseOver()
    {
        if (Vector3.Distance(transform.position, FindObjectOfType<PlayerMovement>().transform.position) < range)
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

    // New part for opening the door when an object with a specific tag gets close
    [SerializeField] string targetTag;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Inside");
        if (other.CompareTag(targetTag))
        {
            Debug.Log("tag");
            if (!isOpen)
            {
                Vector3 directionToReference = other.transform.position - transform.position;
                float dotProduct = Vector3.Dot(transform.forward, directionToReference);
                if (dotProduct > 0)
                {
                    audio.PlayOneShot(openAudio);
                    myAnim.SetTrigger("Open2");
                }
                else if (dotProduct < 0)
                {
                    audio.PlayOneShot(openAudio);
                    myAnim.SetTrigger("Open1");
                    
                }
                isOpen = true;
                StartCoroutine(close());
            }
        }
    }
    IEnumerator close()
    {
        yield return new WaitForSeconds(2);



        if (isOpen)
        {
            audio.PlayOneShot(closeAudio);
            myAnim.SetTrigger("Close");
            isOpen = false;
        }


    }

    private void OnTriggerExit(Collider other)
    {
       // if (other.CompareTag(targetTag))
        //{
          //  if (isOpen)
            //{
              //  audio.PlayOneShot(closeAudio);
                //myAnim.SetTrigger("Close");
                //isOpen = false;
            //}
        //}
    }
}