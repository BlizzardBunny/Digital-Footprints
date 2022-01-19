using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CutscenePlayer : MonoBehaviour
{
    public Sprite[] profilePics = new Sprite[StaticFunction.getNames().Length];
    public Sprite relativePic; //profile pic of the player's relative
    public GameObject customerDetails;

    public GameObject messagesPanel;
    public GameObject customerLinesPrefab;
    public GameObject playerLinesPrefab;
    public GameObject playerChoicesPrefab;

    private string relativeName = "Mom";

    public class TwoDArray
    {
        public bool isPlayerSpeaking { get; }
        public string line { get; }
        public bool isChangingScene { get; }

        public TwoDArray(bool x, string y) => (isPlayerSpeaking, line, isChangingScene) = (x, y, false);
        public TwoDArray(bool x, string y, bool z) => (isPlayerSpeaking, line, isChangingScene) = (x, y, z);
    }

    private TwoDArray[] introDialogue = new TwoDArray[] //bool is true if player is speaking, string is the line to be said
    {
        new TwoDArray(false, "Ok, I managed to recover my account." ),
        new TwoDArray(false, "But I’m not entirely sure why I even got hacked in the first place. " ),
        new TwoDArray(false, "Can you take a look and see if there’s any issues?" ),
        new TwoDArray(true, "Sure thing. Let me log into your account and check." , true),
        new TwoDArray(false, "Thank you so much!" ),
        new TwoDArray(false, "You know, I heard that there’s this new company specializing in these social media things." ),
        new TwoDArray(false, "I think you can try applying to work for them, if you’re happy doing this for work." )
    };

    private string[] tutorialDialogue = new string[]
    {
        "Greetings, and welcome to Digital Footprints, where your privacy is our priority.",
        "For your first day, we would be providing you with some dummy accounts to work on. Use this time to familiarize yourself with the company’s software and workflow.",
        "You will be looking for privacy issues. What privacy issues are is covered by our Company Standards widget on the right.",
        "If you see anything that looks like a privacy issue, click on it and our system would flag it as such.",
        "This would then be reflected in the client’s report where you can double check what you have flagged. Make sure to click send to confirm your flag.",
        "If you are satisfied with your work, click SUBMIT and the client would be notified of your recommendations.",
        "Do be warned that while you are free to make mistakes on the dummy account, doing so with a real customer’s account would warrant a penalty.",
        "We hope that your time with us will be informative and fulfilling, good luck!"
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
            runTutorial();
        }
        else
        {
            pic.GetComponent<Image>().sprite = profilePics[currProfile];
            name.GetComponent<TMPro.TextMeshProUGUI>().text = StaticFunction.getNames()[currProfile];
        }
    }

    void runTutorial()
    {
        for (int i = StaticFunction.dialogueLineCounter; i < introDialogue.Length; i++)
        {
            if (introDialogue[i].isPlayerSpeaking)
            {
                GameObject playerMessage = Instantiate(
                    playerLinesPrefab,
                    new Vector3(959.9981079101563f,198.29248046875f,0.0f),
                    Quaternion.identity,
                    messagesPanel.transform);

                playerMessage.transform.Find("Background").Find("Message").GetComponent<TMPro.TextMeshProUGUI>().text = introDialogue[i].line;
            }
            else
            {
                GameObject customerMessage = Instantiate(
                    customerLinesPrefab,
                    new Vector3(645.9981079101563f,188.83648681640626f,0.0f),
                    Quaternion.identity,
                    messagesPanel.transform);

                customerMessage.transform.Find("Pic").GetComponent<Image>().sprite = relativePic;
                customerMessage.transform.Find("Background").Find("Message").GetComponent<TMPro.TextMeshProUGUI>().text = introDialogue[i].line;
            }       

            StaticFunction.dialogueLineCounter++;
        }
    }
}


