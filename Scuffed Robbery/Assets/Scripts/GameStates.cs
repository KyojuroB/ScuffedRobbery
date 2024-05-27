using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameStates : MonoBehaviour
{
    public bool hasShelveInfo;
    public bool hasDonePipe;
    public bool inRestrictedArea;
    [SerializeField] List<GameObject> shelves;
    [SerializeField] List<GameObject> pipes;
    public int shelveWithInfoInt;
    public int pipeChosen;
    List<RestrictedArea> restrictedAreas;
    public bool isDesguised;   
    // Start is called before the first frame update
    void Start()
    {
       // QualitySettings.vSyncCount = 1;
       // Application.targetFrameRate = targetFrameRate;
        shelveWithInfoInt = Random.Range(0, shelves.Count - 1);
        shelves[shelveWithInfoInt].GetComponent<Shelves>().InfoTrue();
        pipeChosen = Random.Range(0, pipes.Count - 1);
        pipes[pipeChosen].GetComponent<Pipes>().SetUpState(true);

    }

    // Update is called once per frame
    void Update()
    {

    }
    


 
    public void setRestricted(bool value)
    { 
        inRestrictedArea = value;
    }
}
