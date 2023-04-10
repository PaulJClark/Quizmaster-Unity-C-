using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void OpenLevelNorse()
    {
        SceneManager.LoadScene("Norse1");
        Debug.Log("Next level.");
    }

    public void OpenLevelGreek()
    {
        SceneManager.LoadScene("Greek");
    }

    void Update()
    {
        if(Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
