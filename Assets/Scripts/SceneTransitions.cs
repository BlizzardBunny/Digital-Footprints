using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//this script handles transitions between the different scenes
public class SceneTransitions : MonoBehaviour
{
    private Coroutine introAnim;
    public Button NextLevelButton;
    public GameObject confirmationPrefab;

    private GameObject confirmation;
    public void Awake()
    {
        if (NextLevelButton != null)
        {
            //if the player's on level 3 or failed the stage, don't let them go to the next level
            if (StaticFunction.getCurrentLevel() == "Stage 3" || StaticFunction.getMistakes() > StaticFunction.getTotalProfiles())
            {
                NextLevelButton.enabled = false;
                NextLevelButton.GetComponentInChildren<Text>().text = "";
                NextLevelButton.GetComponent<Image>().color = new Color(0, 0, 0);

                foreach(GameObject x in GameObject.FindGameObjectsWithTag("LevelTitle"))
                {
                    if (StaticFunction.getMistakes() > StaticFunction.getTotalProfiles())
                    {
                        x.GetComponent<TMPro.TextMeshProUGUI>().text = "Level Failed";
                    }
                    else
                    {
                        x.GetComponent<TMPro.TextMeshProUGUI>().text = "Level Complete";
                    }
                }
            }
            else
            {
                NextLevelButton.enabled = true;
                NextLevelButton.GetComponentInChildren<Text>().text = "Next Level";                    
            }
        }
    }

    public void Confirm()
    {
        confirmation = Instantiate(
               confirmationPrefab,
               new Vector3(959.9976806640625f,540.0001220703125f,0.0f),
               Quaternion.identity,
               transform.parent);
    }

    public void DestroyConfirm()
    {
        foreach(GameObject x in GameObject.FindGameObjectsWithTag("Confirmation"))
        {
            Destroy(x);
        }
    }

    //Loads Stage 1 when you click Start Game on the Main Menu
    public void StartGame()
    {
        foreach (GameObject x in GameObject.FindGameObjectsWithTag("Confirmation"))
        {
            x.transform.SetAsFirstSibling();
        }
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("ToFadeOnTitle"))
        {
            Animator anim = g.GetComponent<Animator>();
            anim.SetBool("isFading", true);
        }

        Animator bgAnim = GameObject.FindGameObjectWithTag("TitleBG").GetComponent<Animator>();
        introAnim = StartCoroutine(WaitandLoad(bgAnim));
    }

    IEnumerator WaitandLoad(Animator anim)
    {
        yield return new WaitForSeconds(1f);
        anim.SetBool("isZooming", true);
        yield return new WaitForSeconds(1f);
        anim.SetBool("isFading", true);
        yield return new WaitForSeconds(1f);
        StaticFunction.resetVals(3, 1);
        StaticFunction.instanceCounter = 0;
        SceneManager.LoadScene(StaticFunction.getCurrentLevel());

        //set currlevel to the level after this one
        string nextLevel = "";
        string currLevel = StaticFunction.getCurrentLevel();
        if (currLevel.Equals("AskDialogue"))
        {
            if (StaticFunction.tutorialStart)
            {
                nextLevel = "Tutorial";
            }
            else
            {

            }

            StaticFunction.setCurrentLevel(nextLevel);
        }
        DestroyConfirm();
        StopCoroutine(introAnim);
    }
    //Returns to the Main Menu
    public void returnToMainMenu()
    {
        if (StaticFunction.tutorialStart)
        {
            StaticFunction.setCurrentLevel(SceneManager.GetActiveScene().name);
        }
        else
        {
            if (!SceneManager.GetActiveScene().name.Equals("AskDialogue"))
            {
                StaticFunction.setCurrentLevel(SceneManager.GetActiveScene().name);
            }
        }

        foreach (GameObject x in GameObject.FindGameObjectsWithTag("World"))
        {
            Destroy(x);
        }

        SceneManager.LoadScene("MainMenu");
    }

    //Loads the end screen where you can select whether to move to next level, restart level, or go to main menu
    public void LevelEnd()
    {        
        SceneManager.LoadScene("LevelSelect");        
    }
    //This is when you click on next level at the end screen
    public void LoadLevel()
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
        StaticFunction.setCurrentProfile(0);
        SceneManager.LoadScene(currLevel);
    }
    //Exits the game
    public void ExitGame()
    {
        StaticFunction.roundHasStarted = false;
        Application.Quit();
    }
}
