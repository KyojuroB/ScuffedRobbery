using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Tester1 : MonoBehaviour
{
    [SerializeField] List<GameObject> objs;
    private void OnTriggerEnter(Collider other)
    {
        objs.Add(other.gameObject);
    }
}


