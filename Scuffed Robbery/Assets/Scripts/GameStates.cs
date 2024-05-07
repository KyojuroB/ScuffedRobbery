using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStates : MonoBehaviour
{
    public bool inRestrictedArea;
    public enum disguise { NoDesguise, Level1, Level2 };
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetRestricted(bool i)
    { 
        inRestrictedArea = i;
    }
    public bool getRestricted()
    { 
        return inRestrictedArea;
    }
}
