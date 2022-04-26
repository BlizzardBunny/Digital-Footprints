using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PausedMenuFunctions : MonoBehaviour
{
    public GameObject notifPrefab, pausedMenuPrefab;
    private GameObject notif, pausedMenu;

    void Update()
    {
        if (SceneManager.GetActiveScene().name.Equals("MainMenu"))
        {
            if (this.name.Equals("LoadButton"))
            {
                if (!PlayerPrefs.HasKey("currLevel"))
                {
                    this.transform.SetAsFirstSibling();
                }
            }
        }
    }

    public void Pause()
    {
        if (SceneManager.GetActiveScene().name.Equals("AskDialogue"))
        {
            pausedMenu = Instantiate(
                pausedMenuPrefab,
                new Vector3(959.9949951171875f, 540.3999633789063f, 0.0f),
                Quaternion.identity,
                GameObject.FindGameObjectWithTag("AskDialogueScene").transform
            );
        }
        else
        {
            pausedMenu = Instantiate(
                pausedMenuPrefab,
                new Vector3(959.9949951171875f, 540.3999633789063f, 0.0f),
                Quaternion.identity,
                GameObject.FindGameObjectWithTag("World").transform
            );
        }
    }
    
    public void UnPause()
    {
        Destroy(pausedMenu);
    }

    public void ReturnToMainMenu()
    {
        if (StaticFunction.tutorialStart)
        {
            StaticFunction.setCurrentLevel(SceneManager.GetActiveScene().name);
        }
        else
        {
            if (!SceneManager.GetActiveScene().name.Equals("AskDialogue") && !SceneManager.GetActiveScene().name.Equals("LevelSelect"))
            {
                StaticFunction.setCurrentLevel(SceneManager.GetActiveScene().name);
            }
            else if (SceneManager.GetActiveScene().name.Equals("LevelSelect"))
            {
                if (!StaticFunction.passedLevel)
                {
                    StaticFunction.setCurrentLevel(StaticFunction.prevLevel);
                }
            }
        }

        foreach (GameObject x in GameObject.FindGameObjectsWithTag("World"))
        {
            Destroy(x);
        }

        StaticFunction.roundHasStarted = false;

        SaveGame();

        SceneManager.LoadScene("MainMenu");
    }

    public void SaveGame()
    {
        if (SceneManager.GetActiveScene().name.Contains("Stage") || SceneManager.GetActiveScene().name.Equals("Tutorial"))
        {
            PlayerPrefs.SetString("currLevel", SceneManager.GetActiveScene().name);
        }
        else
        {
            PlayerPrefs.SetString("currLevel", StaticFunction.getCurrentLevel());
        }

        if (StaticFunction.tutorialStart)
        {
            PlayerPrefs.SetInt("tutorialStart", 1);
        }
        else
        {
            PlayerPrefs.SetInt("tutorialStart", 0);
        }

        if (StaticFunction.tutorialPart)
        {
            PlayerPrefs.SetInt("tutorialPart", 1);
        }
        else
        {
            PlayerPrefs.SetInt("tutorialPart", 0);
        }

        PlayerPrefs.Save();
        MakeMistakeMessage("Game data saved!");
    }

    public void MakeMistakeMessage(string message)
    {
        if (notifPrefab.name.EndsWith("1"))
        {
            notif = Instantiate(
                notifPrefab,
                new Vector3(376.07061767578127f, 1003.189697265625f, 0.0f),
                Quaternion.identity,
                GameObject.FindGameObjectWithTag("World").transform
            );
        }
        else
        {
            notif = Instantiate(
                notifPrefab,
                new Vector3(281.0283203125f, 1022.5988159179688f - (114.8024f * StaticFunction.mistakeMessages.Count), 0.0f),
                Quaternion.identity,
                GameObject.FindGameObjectWithTag("World").transform
            );
        }

        notif.transform.Find("Message").GetComponent<TMPro.TextMeshProUGUI>().text = message;
        Animator anim = notif.GetComponent<Animator>();
        anim.SetBool("isFadingOut", false);
    }

    public void LoadGame()
    {
        if (PlayerPrefs.HasKey("currLevel"))
        {
            StaticFunction.setCurrentLevel(PlayerPrefs.GetString("currLevel"));
            Debug.Log("PlayerPrefs tutorialStart: " + PlayerPrefs.GetInt("tutorialStart"));            
            switch (PlayerPrefs.GetInt("tutorialStart"))
            {
                case 0:
                    StaticFunction.tutorialStart = false;
                    break;
                case 1:
                    StaticFunction.tutorialStart = true;
                    break;
            }
            Debug.Log("tutorialStart: " + StaticFunction.tutorialStart);
            switch (PlayerPrefs.GetInt("tutorialPart"))
            {
                case 0:
                    StaticFunction.tutorialPart = false;
                    break;
                case 1:
                    StaticFunction.tutorialPart = true;
                    break;
            }
            StaticFunction.dialogueIndex = 0;
            MakeMistakeMessage("Game loaded!");

            SceneTransitions script = (SceneTransitions)this.GetComponent(typeof(SceneTransitions));
            script.StartGame();
        }
        else
            MakeMistakeMessage("There is no save data!");
    }

    public void ResetSaves()
    {
        PlayerPrefs.DeleteAll();
        StaticFunction.tutorialPart = true;
        StaticFunction.reset();
    }

    public void NewGame()
    {
        ResetSaves();
        SceneTransitions script = (SceneTransitions)this.GetComponent(typeof(SceneTransitions));
        script.StartGame();
    }

    public void ExitGame()
    {
        SaveGame();
        Application.Quit();
    }
}
