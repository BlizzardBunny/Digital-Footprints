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

    public void ReturnToMainMenu()
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

    public void SaveGame()
    {        
        PlayerPrefs.SetString("currLevel", StaticFunction.getCurrentLevel());

        if (StaticFunction.tutorialStart)
        {
            PlayerPrefs.SetInt("tutorialStart", 1);
        }
        else
        {
            PlayerPrefs.SetInt("tutorialStart", 0);
        }

        if (StaticFunction.dialogueLineCounter > 11)
        {
            PlayerPrefs.SetInt("dialogueLineCounter", 11);
        }
        else
        {
            PlayerPrefs.SetInt("dialogueLineCounter", 0);
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
            switch (PlayerPrefs.GetInt("tutorialStart"))
            {
                case 0:
                    StaticFunction.tutorialStart = false;
                    break;
                case 1:
                    StaticFunction.tutorialStart = true;
                    break;
            }

            StaticFunction.dialogueLineCounter = PlayerPrefs.GetInt("dialogueLineCounter");

            MakeMistakeMessage("Game loaded!");

            SceneTransitions script = (SceneTransitions)this.GetComponent(typeof(SceneTransitions));
            script.StartGame();
        }
        else
            MakeMistakeMessage("There is no save data!");
    }

    public void NewGame()
    {
        PlayerPrefs.DeleteAll();
        SceneTransitions script = (SceneTransitions)this.GetComponent(typeof(SceneTransitions));
        script.StartGame();
    }

    public void ExitGame()
    {
        SaveGame();
        Application.Quit();
    }
}
