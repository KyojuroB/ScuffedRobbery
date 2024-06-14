using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GoldBar : MonoBehaviour
{


    [SerializeField] float maxRange;
    bool mouseOver = false;
    bool shaded = false;
    [SerializeField] GameObject tintObj;
    [SerializeField] GameObject pickUpText;
    [SerializeField] GameObject TextPrefab;
    [SerializeField] Canvas canvasObj;
    void Start()
    {

        tintObj.SetActive(false);
        // childText.enabled = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && Vector3.Distance(transform.position, FindObjectOfType<PlayerMovement>().transform.position) < maxRange && mouseOver)
        {
            if (FindObjectOfType<Inventory>().IsInventorySpace() == true)
            {
               
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
        else if (!mouseOver || Vector3.Distance(transform.position, FindObjectOfType<PlayerMovement>().transform.position) > maxRange)
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
