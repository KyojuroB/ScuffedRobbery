using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class SecurityCam : MonoBehaviour
{
    [SerializeField] GameObject investigationGroup;
    [SerializeField] GameObject cameraBar;
    GameObject realCamBar;
    
    public  bool isInArea;
    MeshRenderer mR;
    public Transform rayOrigin;       
    Transform target;         
    private LineRenderer lineRenderer; 
    // Start is called before the first frame update
    bool isRunningT;
    void Start()
    {
        investigationGroup.SetActive(false);
        target = FindObjectOfType<PlayerMovement>().transform;
        mR = GetComponent<MeshRenderer>();
        mR.enabled = false;
        
        lineRenderer = GetComponent<LineRenderer>();

       
        lineRenderer.positionCount = 2; 
        lineRenderer.startWidth = 0.02f; 
        lineRenderer.endWidth = 0.02f;   
    }

    // Update is called once per frame
    void Update()
    {
        if (isInArea)
        {
            if (FindObjectOfType<GameStates>().inRestrictedArea)
            {
                if (FindObjectOfType<GameStates>().isDesguised)
                {

                }
                else
                {
                    ShowRay();
                    if (target == null || rayOrigin == null)
                    {
                        Debug.LogError("Ray Origin or Target is not assigned.");
                        return;
                    }
                    if (!isRunningT)
                    {
                        setUpBar();
                    }
                    isRunningT = true;
                    
                    Vector3 direction = (target.position - rayOrigin.position).normalized;


                    Vector3 endPosition = target.position;

                    lineRenderer.SetPosition(0, rayOrigin.position);
                    lineRenderer.SetPosition(1, endPosition);
                }
            }
            else
            {
                DisableRay();
            }
            //else if crime noticed (Reminder : Ben program it latter)

        }
        else 
        {
            DisableRay();
        }

        if (Input.GetKey(KeyCode.T))
        {
            mR.enabled = true;
        }
        else 
        {
            mR.enabled = false;
        }
    }
    private void setUpBar()
    {
        
        investigationGroup.SetActive(true);
        realCamBar = Instantiate(cameraBar);
        realCamBar.transform.SetParent(investigationGroup.transform, false);
    }
    private void DisableRay()
    {
        isRunningT = false;
        lineRenderer.enabled = false;
        Destroy(realCamBar);
        if (investigationGroup.transform.childCount == 0)
        {
            investigationGroup.SetActive(false);
        }
    }
    private void ShowRay()
    {
        lineRenderer.enabled = true;
    }
    private void FixedUpdate()
    {
 
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == ("Player"))
        { 
            isInArea = true;
           
            Debug.Log("In");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == ("Player"))
        {
            isInArea = false;
        }
    }


}
