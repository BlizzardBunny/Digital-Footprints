using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PausedMenuFunctions : MonoBehaviour
{
    void ReturnToMainMenu()
    {
        StaticFunction.setCurrentLevel(SceneManager.GetActiveScene().name);
        SceneManager.LoadScene("MainMenu");
    }

    void SaveGame()
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
        Debug.Log("Game data saved!");
    }

    void LoadGame()
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
            Debug.Log("Game data loaded!");
        }
        else
            Debug.LogError("There is no save data!");
    }
}
