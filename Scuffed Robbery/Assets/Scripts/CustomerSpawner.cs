using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    [SerializeField] GameObject customerPrefab;
    [SerializeField] List<GameObject> spawnPoints;
    [SerializeField] int spawnCoolDown;
    [SerializeField] GameObject leave;
    ////

    [SerializeField] List<GameObject> boothSpeakPos;
    public List<bool> BoothAvailable;
    ////
    public int spawnerAt = 0;
    public int ammountOfCustomer = 0;
    public bool isRunning = false;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!isRunning && ammountOfCustomer < 3)
        {
            StartCoroutine(CustomerSpawn());
        }    
    }
    IEnumerator CustomerSpawn()
    {
        isRunning = true;
        var customer = Instantiate(customerPrefab, spawnPoints[spawnerAt].transform.position, Quaternion.identity);
        for (int i = 0; i < boothSpeakPos.Count; i++)
        {
            if (BoothAvailable[i] ==false)
            {
                customer.GetComponent<CustomerAi>().index = i;
                customer.GetComponent<CustomerAi>().asssignedSeat = boothSpeakPos[i];
                BoothAvailable[i] = true;
                i = boothSpeakPos.Count;
            }
            
        }
     
        ammountOfCustomer++;
        customer.GetComponent<CustomerAi>().leaveSeat = leave;


        int spTest = spawnerAt+1;
        if (spTest > spawnPoints.Count-1)
        {
            spawnerAt = 0;
        }
        else
        {
            spawnerAt++;
        }
 
        yield return new WaitForSeconds(spawnCoolDown);
        isRunning = false;
    }
    public void RemoveCustomer(int i)
    { 
        ammountOfCustomer--;
        BoothAvailable[i] = false;
    }    
}
