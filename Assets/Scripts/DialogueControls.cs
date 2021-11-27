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
			Application.Quit();
        }
        else
        {
            dialogueBox.GetComponent<TMPro.TextMeshProUGUI>().text = dialogue[dialogueIndex];
            dialogueIndex++;
        }
    }
}
