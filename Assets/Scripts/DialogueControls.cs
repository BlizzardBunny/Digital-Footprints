using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogueControls : MonoBehaviour
{
    private Transform dialogueBox;
    private int dialogueIndex = 1;
    private string[] dialogue;

    private void Start()
    {
        dialogueBox = transform.parent.transform.Find("Dialogue");    
    }

    public void setDialogue(string[] d)
    {
        dialogue = d;
    }

    public void ContDialogue()
    {
        if (dialogueIndex >= dialogue.Length)
        {
            dialogueIndex = 0;
            StaticFunction.instanceCounter = 0;
            if (SceneManager.GetActiveScene().name == "Stage 1")
            {            
                StaticFunction.resetVals(4,3);
                Debug.Log("===================STAGE 2===================");
                SceneManager.LoadScene("Stage 2");
            }
            else if (SceneManager.GetActiveScene().name == "Stage 2")
            {
                StaticFunction.resetVals(5, 5);
                Debug.Log("===================STAGE 3===================");
                SceneManager.LoadScene("Stage 3");
            }
            else if (SceneManager.GetActiveScene().name == "Stage 3")
            {
                Application.Quit();
            }
        }
        else
        {
            dialogueBox.GetComponent<TMPro.TextMeshProUGUI>().text = dialogue[dialogueIndex];
            dialogueIndex++;
        }
    }
}
