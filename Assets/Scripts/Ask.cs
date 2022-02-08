using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ask : MonoBehaviour
{
    public void ask()
    {
        StaticFunction.dialogueLineCounter = 0;
        SceneManager.LoadScene("AskDialogue");
    }
}
