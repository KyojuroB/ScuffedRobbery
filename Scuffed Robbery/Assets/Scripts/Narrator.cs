using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Narrator : MonoBehaviour
{
 
    [SerializeField] GameObject textObj;
    TextMeshProUGUI tmText;
    public bool hasCard;
    public bool hasuni;
    // Start is called before the first frame update
    void Start()
    {
        tmText = textObj.GetComponent<TextMeshProUGUI>();
        StartCoroutine(StartSequence());
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void CheckForCardAndUni()
    {
        if (hasCard && hasuni)
        {
            CreateText("Now go and find the security room and grab that usb stick, were going to need it.", 7);
        }
    }
    public void CreateText(string funcText, int duration)
    {
        tmText.text = funcText;
        StartCoroutine(RemoveText(duration));
    }
    IEnumerator RemoveText(int duration)
    {
        yield return new WaitForSeconds(duration);
        tmText.text = null;
    }
    IEnumerator StartSequence()
    {
        CreateText("Testing? Hello?", 3);
        yield return new WaitForSeconds(3);
        CreateText("Im helping you out today.", 4);
        yield return new WaitForSeconds(4);
        CreateText("So you lost a bet huh? anyways lets talk about the mission.", 5);
        yield return new WaitForSeconds(5);
        CreateText("Were breaking into the vault, stealing gold.", 5);
        yield return new WaitForSeconds(5);
        CreateText("This placed has cameras and guards everywhere so be careful.", 4);
        yield return new WaitForSeconds(4);
        CreateText("We had someone leave a keycard and uniform,", 4);
        yield return new WaitForSeconds(4);
        CreateText("Should be in one of the bathrooms, go find them.", 6);
    }
}
