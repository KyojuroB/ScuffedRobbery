using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameStates : MonoBehaviour
{

    public bool hasShelveInfo;
    public bool hasDonePipe;
    public bool inRestrictedArea;
    public bool illegal;
    [SerializeField] List<GameObject> shelves;
    [SerializeField] List<GameObject> pipes;
    public int shelveWithInfoInt;
    public int pipeChosen;
    public bool isDesguised;
    [SerializeField] List<Material> pipeColors;
    [SerializeField] GameObject vaultPipe;
    [SerializeField] GameObject desguiseIcon;
    [SerializeField] GameObject desguiseTresspass;
    [SerializeField] GameObject ilegalUi;
    public GameObject goldParent;
    public int ammountOfGold;
    public int goldCollected;
    public bool hasAllGold;
    [SerializeField] GameObject goldImage;
    [SerializeField] GameObject normalCanvas;
    [SerializeField] GameObject looseCanvas;
    [SerializeField] GameObject MusicPlayer;
    // Start is called before the first frame update
    void Start()
    {
 
        goldImage.SetActive(false);
        ammountOfGold = goldParent.transform.childCount;
        ilegalUi.SetActive(false);
        desguiseTresspass.SetActive(false);
        desguiseIcon.SetActive(false);
        // QualitySettings.vSyncCount = 1;
        // Application.targetFrameRate = targetFrameRate;
        shelveWithInfoInt = Random.Range(0, shelves.Count);
        shelves[shelveWithInfoInt].GetComponent<Shelves>().InfoTrue();
        pipeChosen = Random.Range(0, pipes.Count);
        pipes[pipeChosen].GetComponent<Pipes>().SetUpState(true);
        vaultPipe.GetComponent<MeshRenderer>().material = pipeColors[pipeChosen];

    }
    public void Desguiseue()
    {
        isDesguised = true;
        desguiseTresspass.SetActive(true);
        desguiseIcon.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void collectGold()
    {
        if (goldCollected == 0)
        {
            goldImage.SetActive(true);
        }
        goldCollected++;
        goldImage.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = goldCollected.ToString() + "/" + ammountOfGold.ToString();
        if (goldCollected == ammountOfGold)
        { 
            hasAllGold = true;
        }
    }    


    public void Setillegal(bool value)
    {
        illegal = value;
        if (illegal)
        {
            ilegalUi.SetActive(true);
            return;
        }
        else if(!illegal)
        {
            ilegalUi.SetActive(false);
            return;
        }
    }
    public void setRestricted(bool value)
    { 
        inRestrictedArea = value;
    }
    public void LoseGame()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        FindObjectOfType<PlayerMovement>().GetComponent<PlayerMovement>().enabled = false;
        normalCanvas.SetActive(false);
        looseCanvas.SetActive(true);
        StartCoroutine(LooseTransition());
        MusicPlayer.SetActive(false);
    }
    IEnumerator LooseTransition()
    { 
        yield return new WaitForSeconds(4);
        SceneManager.LoadScene("Loose Screen");
    }
}
