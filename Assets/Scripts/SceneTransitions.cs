using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//this script handles transitions between the different scenes
public class SceneTransitions : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Stage 1");
    }
    public void returnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
