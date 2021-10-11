using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FlagSystem : MonoBehaviour
{
    public Sprite[] badPosts = new Sprite[StaticFunction.getBadCaptions().Length];
    public Sprite[] goodPosts = new Sprite[StaticFunction.getGoodCaptions().Length];
    public Sprite[] profilePics = new Sprite[StaticFunction.getNames().Length];

    private string parentName;
    private bool isFlag;
    private int flagIndex;
    private static int rndNum = -1;
    private static int otherPostIndex = -1;
    private static int instanceCounter = 0; //counts how many instances have run this script

    // Start is called before the first frame update
    void Start()
    {
        parentName = transform.parent.name;
        isFlag = false;
        flagIndex = -1;
        Setup();
        CheckSetup();
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (instanceCounter <= 0)
            {
                isFlag = false;
                flagIndex = -1;
                otherPostIndex = -1;
                StaticFunction.setErrorNum(1);
                rndNum = -1;
            }
            Setup();
            CheckSetup();
        }
    }

    private void Setup()
    {
        Debug.Log(parentName + " ErrorNum A: " + StaticFunction.getErrorNum());
        if ((rndNum <= -1) && (instanceCounter <= 0))
        {
            rndNum = UnityEngine.Random.Range(0, profilePics.Length);
        }

        Transform name = transform.parent.parent.transform.Find("Name");
        Transform profile = transform.parent.parent.transform.Find("ProfilePic");
        Transform bio = transform.parent.parent.transform.Find("Bio");

        name.GetComponent<TMPro.TextMeshProUGUI>().text = StaticFunction.getNames()[rndNum];
        profile.GetComponent<Image>().sprite = profilePics[rndNum];
        bio.GetComponent<TMPro.TextMeshProUGUI>().text = StaticFunction.getBios()[rndNum];

        int rnd = UnityEngine.Random.Range(1, 4); //determine if this instance is a flag [random]
        Debug.Log(parentName + ", rnd: " + rnd);

        if ((rnd == 1) && (StaticFunction.getErrorNum() > 0)) //this instance is a flag
        {
            StaticFunction.setErrorNum(StaticFunction.getErrorNum() - 1);

            if (parentName == "Location") //not a post prefab
            {
                transform.parent.GetComponent<TMPro.TextMeshProUGUI>().text = StaticFunction.getBadAddress()[rndNum];
                isFlag = true;
                Debug.Log("Bad " + rnd + ": " + transform.parent.GetComponent<TMPro.TextMeshProUGUI>().text);
            }
            else //is a post prefab
            {
                //make deets of post match deets of profile
                Transform postProfilePic = transform.parent.transform.Find("ProfilePic");
                Transform postName = transform.parent.transform.Find("Name");
                postProfilePic.GetComponent<Image>().sprite = profilePics[rndNum];
                postName.GetComponent<TMPro.TextMeshProUGUI>().text = StaticFunction.getNames()[rndNum];

                //randomize post content
                Transform caption = transform.parent.transform.Find("Caption");
                Transform photo = transform.parent.transform.Find("Photo");

                int rnd2 = UnityEngine.Random.Range(0, StaticFunction.getBadCaptions().Length);
                caption.GetComponent<TMPro.TextMeshProUGUI>().text = StaticFunction.getBadCaptions()[rnd2];
                photo.GetComponent<Image>().sprite = badPosts[rnd2];
                flagIndex = rnd2;
                isFlag = true;
            }
        }
        else //not a flag
        {
            if (parentName == "Location") //not a post prefab
            {
                transform.parent.GetComponent<TMPro.TextMeshProUGUI>().text = StaticFunction.getGoodAddress()[rndNum];
                Debug.Log("Good " + rnd + ": " + transform.parent.GetComponent<TMPro.TextMeshProUGUI>().text);
            }
            else //is a post prefab
            {
                //make deets of post match deets of profile
                Transform postProfilePic = transform.parent.transform.Find("ProfilePic");
                Transform postName = transform.parent.transform.Find("Name");
                postProfilePic.GetComponent<Image>().sprite = profilePics[rndNum];
                postName.GetComponent<TMPro.TextMeshProUGUI>().text = StaticFunction.getNames()[rndNum];

                //randomize post content
                Transform caption = transform.parent.transform.Find("Caption");
                Transform photo = transform.parent.transform.Find("Photo");

                int rnd2 = UnityEngine.Random.Range(0, StaticFunction.getGoodCaptions().Length);
                if (otherPostIndex <= -1)
                {
                    otherPostIndex = rnd2;
                }
                else
                {
                    if (rnd2 == otherPostIndex)
                    {
                        rnd2++;
                    }
                }
                caption.GetComponent<TMPro.TextMeshProUGUI>().text = StaticFunction.getGoodCaptions()[rnd2];
                photo.GetComponent<Image>().sprite = goodPosts[rnd2];
            }
        }

        Debug.Log(parentName + " ErrorNum B: " + StaticFunction.getErrorNum());
    }

    //for case where no flags were made
    private void CheckSetup()
    {        
        instanceCounter++;

        if (instanceCounter == 3)
        {
            if ((StaticFunction.getErrorNum() > 0))
            {
                StaticFunction.setErrorNum(StaticFunction.getErrorNum() - 1);

                if (parentName == "Location") //not a post prefab
                {
                    transform.parent.GetComponent<TMPro.TextMeshProUGUI>().text = StaticFunction.getBadAddress()[rndNum];
                    isFlag = true;
                }
                else //is a post prefab
                {
                    //make deets of post match deets of profile
                    Transform postProfilePic = transform.parent.transform.Find("ProfilePic");
                    Transform postName = transform.parent.transform.Find("Name");
                    postProfilePic.GetComponent<Image>().sprite = profilePics[rndNum];
                    postName.GetComponent<TMPro.TextMeshProUGUI>().text = StaticFunction.getNames()[rndNum];

                    //randomize post content
                    Transform caption = transform.parent.transform.Find("Caption");
                    Transform photo = transform.parent.transform.Find("Photo");

                    int rnd2 = UnityEngine.Random.Range(0, StaticFunction.getBadCaptions().Length);
                    caption.GetComponent<TMPro.TextMeshProUGUI>().text = StaticFunction.getBadCaptions()[rnd2];
                    photo.GetComponent<Image>().sprite = badPosts[rnd2];
                    flagIndex = rnd2;
                    isFlag = true;
                }
            }

            instanceCounter = 0;
        }
    }
    public void Proc()
    {
        if (isFlag)
        {
            if (parentName == "Location")
            {

            }
        }
        else
        {

        }
    }

    public void Exit()
    {
        Application.Quit();
    }
}
