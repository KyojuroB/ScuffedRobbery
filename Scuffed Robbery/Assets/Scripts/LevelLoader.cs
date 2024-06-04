using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
  
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private IEnumerator LoadingBar()
    {
        yield return new WaitForSeconds(5f);
        Debug.Log("Loaded");
        SceneManager.LoadScene(1);
    }

    public void loadGame()
    {
        StartCoroutine(LoadingBar());
    }
    public void LoadSceneByInt(int sceneInt)
    {
        SceneManager.LoadScene(sceneInt);
    }
    public void Quit()
    {
        Application.Quit();
    }
}
