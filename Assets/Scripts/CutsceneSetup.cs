using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CutsceneSetup : MonoBehaviour
{
    public Sprite[] profilePics = new Sprite[StaticFunction.getNames().Length];
    public Sprite relativePic; //profile pic of the player's relative
    public GameObject customerDetails;

    private string relativeName = "Mom";

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
        }
        else
        {
            pic.GetComponent<Image>().sprite = profilePics[currProfile];
            name.GetComponent<TMPro.TextMeshProUGUI>().text = StaticFunction.getNames()[currProfile];
        }
    }
}
