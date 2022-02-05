using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ask : MonoBehaviour
{
    public void ask()
    {
        SceneManager.LoadScene("AskDialogue");
    }
}
