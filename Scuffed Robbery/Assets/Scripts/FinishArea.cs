using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishArea : MonoBehaviour
{
    public bool isInArea;
    [SerializeField] GameObject finishAreaUi;
    //
    [SerializeField] GameObject tasksParent;
    [SerializeField] GameObject completionParent;

    [SerializeField] List<TextMeshProUGUI> tasksText;
    [SerializeField] List<TextMeshProUGUI> tasksCompletion;

    public List<int> tasksNeeded;
    public List<int> tasksDone;
    public List<bool> isTaskComlete;
  
    private void Start()
    {
        for (int i = 0; i < tasksParent.transform.childCount; i++)
        {
            tasksText.Add(tasksParent.transform.GetChild(i).gameObject.GetComponent<TextMeshProUGUI>());
            isTaskComlete.Add(false);
     
        }
        /////////////////
        for (int i = 0; i < completionParent.transform.childCount; i++)
        {
            tasksCompletion.Add(completionParent.transform.GetChild(i).gameObject.GetComponent<TextMeshProUGUI>());

        }



        finishAreaUi.gameObject.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {

            OnTriggerFunc();
        }
    }
    private void OnTriggerFunc()
    {
        for (int i = 0; i < isTaskComlete.Count; i++)
        {
            if (isTaskComlete[i] == true)
            {
                
                if (i == isTaskComlete.Count-1)
                {
                    Debug.Log("Win");
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                    SceneManager.LoadScene("Win Screen");
                }
            }
            else
            {

                isInArea = true;
                finishAreaUi.gameObject.SetActive(true);
                return;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            isInArea = false;
            finishAreaUi.gameObject.SetActive(false);
        }
    }
    public void completeTask(int taskListIndex)
    {
        
        tasksDone[taskListIndex]++;
        tasksCompletion[taskListIndex].text = "(" + tasksDone[taskListIndex] +"/"+ tasksNeeded[taskListIndex] +")";
        if (tasksDone[taskListIndex] == tasksNeeded[taskListIndex])
        {
            
            tasksText[taskListIndex].color = Color.green;
            isTaskComlete[taskListIndex] = true;

        }
    }
}
