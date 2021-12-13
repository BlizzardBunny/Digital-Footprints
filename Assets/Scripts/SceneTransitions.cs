using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//this script handles transitions between the different scenes
public class SceneTransitions : MonoBehaviour
{
    private Coroutine fadeOut;
    public Button NextLevelButton;

    public void Awake()
    {
        if (StaticFunction.getCurrentLevel() == "Stage 3")
        {
            NextLevelButton.enabled = false;
            NextLevelButton.GetComponentInChildren<Text>().text = "";
            NextLevelButton.GetComponent<Image>().color = new Color(0, 0, 0);
        }
        else
        {
            NextLevelButton.enabled = true;
            NextLevelButton.GetComponentInChildren<Text>().text = "Next Level";
        }
    }
    //Loads Stage 1 when you click Start Game on the Main Menu
    public void StartGame()
    {
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("ToFadeOnTitle"))
        {
            Animator anim = g.GetComponent<Animator>();
            anim.SetBool("isFading", true);
        }

        Animator bgAnim = GameObject.FindGameObjectWithTag("TitleBG").GetComponent<Animator>();
        fadeOut = StartCoroutine(WaitandLoad(bgAnim));
    }

    IEnumerator WaitandLoad(Animator anim)
    {
        yield return new WaitForSeconds(0.5f);
        anim.SetBool("isZooming", true);
        yield return new WaitForSeconds(0.5f);
        anim.SetBool("isFading", true);
        yield return new WaitForSeconds(0.5f);
        StaticFunction.resetVals(3, 1);
        StaticFunction.instanceCounter = 0;
        SceneManager.LoadScene("Stage 1");
        StopCoroutine(fadeOut);
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
        StaticFunction.setNumOfReports(0);
        StaticFunction.setCurrentProfile(0);
        SceneManager.LoadScene(currLevel);
    }
    //Exits the game
    public void ExitGame()
    {
        Application.Quit();
    }
}
