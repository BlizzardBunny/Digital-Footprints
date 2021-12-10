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
    public static void LevelEnd()
    {
        SceneManager.LoadScene("LevelSelect");
    }
    public static void LoadLevel()
    {
        string currLevel = StaticFunction.getCurrentLevel();
        if (currLevel == "Stage 1")
        {
            StaticFunction.resetVals(4, 3);
            SceneManager.LoadScene("Stage 2");
        }
        else if (currLevel == "Stage 2")
        {
            StaticFunction.resetVals(5, 5);
            SceneManager.LoadScene("Stage 3");
        }
        else if (currLevel == "Stage 3")
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
    public static void RestartLevel()
    {
        string currLevel = StaticFunction.getCurrentLevel();
        if (currLevel == "Stage 1")
        {
            StaticFunction.resetVals(3, 1);
        }
        else if (currLevel == "Stage 2")
        {
            StaticFunction.resetVals(4, 3);
        }
        else if (currLevel == "Stage 3")
        {
            StaticFunction.resetVals(5, 5);
        }
        SceneManager.LoadScene(currLevel);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
