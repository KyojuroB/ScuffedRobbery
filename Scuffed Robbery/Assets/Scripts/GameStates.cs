using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStates : MonoBehaviour
{
    public bool inRestrictedArea;
    [SerializeField] List<GameObject> shelves;
    public int shelveWithInfoInt;
    public enum disguise { NoDesguise, Level1, Level2 };
    // Start is called before the first frame update
    void Start()
    {
        shelveWithInfoInt = Random.Range(0, shelves.Count - 1);
        shelves[shelveWithInfoInt].GetComponent<Shelves>().InfoTrue();
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
