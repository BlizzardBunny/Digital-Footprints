using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogueControls : MonoBehaviour
{
    private Transform dialogueBox;
    private int dialogueIndex = 1;
    private string[] dialogue;
    private Coroutine showMsg;

    private void Start()
    {
        try
        {
            dialogueBox = transform.parent.transform.Find("Dialogue");  
        }
        catch (System.Exception) {}       
        
        if (transform.name == "Close")
        {
            FadeOutTimer();
        }
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

    public void DestroyParent()
    {
        Debug.Log(transform.parent.Find("Message").GetComponent<TMPro.TextMeshProUGUI>().text);
        StopCoroutine(showMsg);

        //update list
        int id = int.Parse(transform.parent.Find("ID").GetComponent<TMPro.TextMeshProUGUI>().text);
        StaticFunction.mistakeMessages.RemoveAt(id);
        for (int i = id; i < StaticFunction.mistakeMessages.Count; i++)
        {
            StaticFunction.mistakeMessages[i].transform.Translate(new Vector3(0f, 114.8024f, 0f));
            StaticFunction.mistakeMessages[i].transform.Find("ID").GetComponent<TMPro.TextMeshProUGUI>().text = i.ToString();
        }

        Destroy(transform.parent.gameObject);
    }

    public void FadeOutTimer()
    {
        Animator anim = transform.parent.GetComponent<Animator>();
        showMsg = StartCoroutine(ShowMessage(anim));
    }

    IEnumerator ShowMessage(Animator anim)
    {
        yield return new WaitForSeconds(5f);
        anim.SetBool("isFadingOut", true);
        yield return new WaitForSeconds(1f);        
        DestroyParent();
    }
}
