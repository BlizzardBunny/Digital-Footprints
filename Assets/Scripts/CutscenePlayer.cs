using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CutscenePlayer : MonoBehaviour
{
    public class Dialogue
    {
        public bool isPlayerSpeaking { get; }
        public string line { get; }
        public bool isChangingScene { get; }
        public string[] choices { get; }
        public string[] answers { get; }

        public Dialogue(bool x, string y) => (isPlayerSpeaking, line, isChangingScene) = (x, y, false);
        public Dialogue(bool x, string y, bool z) => (isPlayerSpeaking, line, isChangingScene) = (x, y, z);
        public Dialogue(bool x, bool z, string[] a, string[] b) => (isPlayerSpeaking, line, isChangingScene, choices, answers) = (x, "", z, a, b);
    }

    public Sprite[] profilePics = new Sprite[StaticFunction.getNames().Length];
    public Sprite relativePic; //profile pic of the player's relative
    public GameObject customerDetails;

    public GameObject messagesPanel;
    public GameObject customerLinesPrefab;
    public GameObject playerLinesPrefab;
    public GameObject playerChoicesPrefab;

    private string relativeName = "Mom";

    private GameObject currChoiceMenu;
    private Dialogue[] currDialogue;
    private int currLine;
    private Coroutine coroutineToBeStopped;

    private Dialogue[] introDialogue = new Dialogue[] //bool is true if player is speaking, string is the line to be said
    {
        new Dialogue(false, "Ok, I managed to recover my account." ),
        new Dialogue(false, "But I�m not entirely sure why I even got hacked in the first place. " ),
        new Dialogue(false, "Can you take a look and see if there�s any issues?" ),
        new Dialogue(true, true, 
            new string[]{"No thanks. I don't really wanna do it today.", "Why? Can't you do it yourself?", "Sure thing. Let me log into your account and check." }, 
            new string[]{"No thanks. I don't really wanna do it today.", "Why? Can't you do it yourself?", "Sure thing. Let me log into your account and check." }),
        new Dialogue(false, "Thank you so much!" ),
        new Dialogue(false, "You know, I heard that there�s this new company specializing in these social media things." ),
        new Dialogue(false, "I think you can try applying to work for them, if you�re happy doing this for work." )
    };

    // Start is called before the first frame update
    void Start()
    {
        int currProfile = StaticFunction.getCurrentProfile();

        //setup customer details
        Transform pic = customerDetails.transform.Find("Pic");
        Transform name = customerDetails.transform.Find("Name");

        if (StaticFunction.tutorialStart)
        {
            pic.GetComponent<Image>().sprite = relativePic;
            name.GetComponent<TMPro.TextMeshProUGUI>().text = relativeName;
            StartCoroutine(runTutorial());
        }
        else
        {
            pic.GetComponent<Image>().sprite = profilePics[currProfile];
            name.GetComponent<TMPro.TextMeshProUGUI>().text = StaticFunction.getNames()[currProfile];
        }
    }

    IEnumerator runTutorial()
    {
        if (coroutineToBeStopped != null)
        {
            StopCoroutine(coroutineToBeStopped);
        }

        for (int i = StaticFunction.dialogueLineCounter; i < introDialogue.Length; i++)
        {
            string line;

            //update relativeName in dialogue programmatically
            if (introDialogue[i].line.Contains("$r"))
            {
                line = introDialogue[i].line.Replace("$r", relativeName);
            }
            else
            {
                line = introDialogue[i].line;
            }

            if (introDialogue[i].isPlayerSpeaking)
            {
                if (introDialogue[i].choices != null)
                {
                    currDialogue = introDialogue;
                    currLine = i;
                    yield return StartCoroutine(showChoices(introDialogue[i].choices));
                }
                else
                {
                    yield return new WaitForSeconds(3f);
                    GameObject playerMessage = Instantiate(
                    playerLinesPrefab,
                    new Vector3(959.9981079101563f, 198.29241943359376f,0.0f),
                    Quaternion.identity,
                    messagesPanel.transform.parent);

                    playerMessage.transform.Find("Background").Find("Message").GetComponent<TMPro.TextMeshProUGUI>().text = line;

                    yield return new WaitForFixedUpdate(); //wait for playerMessage to update size acc to text
                    yield return new WaitForFixedUpdate(); //skip frame

                    RectTransform bg = playerMessage.transform.Find("Background").GetComponent<RectTransform>();
                    RectTransform area = playerMessage.transform.GetComponent<RectTransform>();

                    if (bg.sizeDelta.y > area.sizeDelta.y)
                    {
                        playerMessage.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(area.sizeDelta.x, bg.sizeDelta.y + 10f);
                    }

                    yield return new WaitForFixedUpdate(); //wait for playerMessage to update size
                    yield return new WaitForFixedUpdate(); //skip frame

                    playerMessage.transform.SetParent(messagesPanel.transform);
                }
            }
            else
            {
                yield return StartCoroutine(showTypingStatus());
                GameObject customerMessage = Instantiate(
                    customerLinesPrefab,
                    new Vector3(959.9981079101563f,195.107421875f,0.0f),
                    Quaternion.identity,
                    messagesPanel.transform.parent);

                customerMessage.transform.Find("Pic").GetComponent<Image>().sprite = relativePic;
                customerMessage.transform.Find("Background").Find("Message").GetComponent<TMPro.TextMeshProUGUI>().text = line;

                yield return new WaitForFixedUpdate(); //wait for Background to update size acc to text
                yield return new WaitForFixedUpdate(); //skip frame

                RectTransform bg = customerMessage.transform.Find("Background").GetComponent<RectTransform>();
                RectTransform area = customerMessage.transform.GetComponent<RectTransform>();

                if (bg.sizeDelta.y > area.sizeDelta.y)
                {
                    customerMessage.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(area.sizeDelta.x, bg.sizeDelta.y + 10f);
                }

                yield return new WaitForFixedUpdate(); //wait for customerMessage to update size
                yield return new WaitForFixedUpdate(); //skip frame

                customerMessage.transform.SetParent(messagesPanel.transform);
            }       

            StaticFunction.dialogueLineCounter++;
        }
    }

    IEnumerator showTypingStatus()
    {
        customerDetails.transform.Find("Status").GetComponent<TMPro.TextMeshProUGUI>().text = "typing...";
        yield return new WaitForSeconds(3f);
        customerDetails.transform.Find("Status").GetComponent<TMPro.TextMeshProUGUI>().text = "online";
    }

    IEnumerator showChoices(string[] choices)
    {
        currChoiceMenu = Instantiate(
            playerChoicesPrefab,
            new Vector3(959.9981079101563f, 236.2274169921875f, 0.0f),
            Quaternion.identity,
            messagesPanel.transform.parent);

        for (int i = 1; i - 1 < choices.Length; i++)
        {
            currChoiceMenu.transform.Find("Background").Find("Choice " + i).Find("Message").GetComponent<TMPro.TextMeshProUGUI>().text = choices[i - 1];
            yield return new WaitForFixedUpdate(); //wait for currChoicMenu to update size acc to text
            yield return new WaitForFixedUpdate(); //skip frame
        }

        RectTransform bg = currChoiceMenu.transform.Find("Background").GetComponent<RectTransform>();
        RectTransform area = currChoiceMenu.transform.GetComponent<RectTransform>();

        yield return new WaitForFixedUpdate(); //wait for currChoicMenu to update size acc to text
        yield return new WaitForFixedUpdate(); //skip frame

        Debug.Log(bg.sizeDelta.y);
        Debug.Log(area.sizeDelta.y);

        if (bg.sizeDelta.y > area.sizeDelta.y)
        {
            currChoiceMenu.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(area.sizeDelta.x, bg.sizeDelta.y + 40f);
        }

        yield return new WaitForFixedUpdate(); //wait for currChoiceMenu to update size
        yield return new WaitForFixedUpdate(); //skip frame

        currChoiceMenu.transform.SetParent(messagesPanel.transform);

        StopAllCoroutines();
    }

    IEnumerator showPlayerMessage()
    {
        GameObject playerMessage = Instantiate(
        playerLinesPrefab,
        new Vector3(959.9981079101563f, 198.29241943359376f, 0.0f),
        Quaternion.identity,
        messagesPanel.transform.parent);

        playerMessage.transform.Find("Background").Find("Message").GetComponent<TMPro.TextMeshProUGUI>().text = currDialogue[currLine].answers[StaticFunction.choiceIndex];

        yield return new WaitForFixedUpdate(); //wait for playerMessage to update size acc to text
        yield return new WaitForFixedUpdate(); //skip frame

        playerMessage.transform.SetParent(messagesPanel.transform);

        StaticFunction.dialogueLineCounter++;

        StartCoroutine(runTutorial());
    }

    public void acceptChoice()
    {
        Destroy(currChoiceMenu);

        coroutineToBeStopped = StartCoroutine(showPlayerMessage());
    }
}


