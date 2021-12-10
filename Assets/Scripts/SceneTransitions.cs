using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//this script handles transitions between the different scenes
public class SceneTransitions : MonoBehaviour
{
    //Loads Stage 1 when you click Start Game on the Main Menu
    public void StartGame()
    {
        SceneManager.LoadScene("Stage 1");
    }
    //Returns to the Main Menu
    public void returnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    //Loads the end screen where you can select whether to move to next level, restart level, or go to main menu
    public static void LevelEnd()
    {
        SceneManager.LoadScene("LevelSelect");
    }
    //This is when you click on next level at the end screen
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
    //This is when you click on restart level on the end screen
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
    //Exits the game
    public void ExitGame()
    {
        Application.Quit();
    }
}
