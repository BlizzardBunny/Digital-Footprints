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
            StaticFunction.setCurrentLevel(SceneManager.GetActiveScene().name);
            SceneTransitions script = (SceneTransitions)GameObject.FindGameObjectWithTag("Exit").GetComponent(typeof(SceneTransitions));
            script.LevelEnd();
        }
        else
        {
            dialogueBox.GetComponent<TMPro.TextMeshProUGUI>().text = dialogue[dialogueIndex];
            dialogueIndex++;
        }
    }

    public void DestroyParent()
    {
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
