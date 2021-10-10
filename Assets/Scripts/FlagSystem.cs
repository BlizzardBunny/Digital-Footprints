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
    private static int post1Index = -1;

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
            StaticFunction.setErrorNum(1);
            Setup();
            CheckSetup();
        }
    }

    private void Setup()
    {
        if (rndNum <= -1)
        {
            rndNum = UnityEngine.Random.Range(0, profilePics.Length);
        }

        Transform name = transform.parent.parent.transform.Find("Name");
        Transform profile = transform.parent.parent.transform.Find("ProfilePic");
        Transform bio = transform.parent.parent.transform.Find("Bio");

        name.GetComponent<TMPro.TextMeshProUGUI>().text = StaticFunction.getNames()[rndNum];
        profile.GetComponent<Image>().sprite = profilePics[rndNum];
        bio.GetComponent<TMPro.TextMeshProUGUI>().text = StaticFunction.getBios()[rndNum];

        if (parentName == "Location")
        {            
            int rnd = UnityEngine.Random.Range(1, 5);      
            if ((rnd == 1) && (StaticFunction.getErrorNum() > 0))
            {
                transform.parent.GetComponent<TMPro.TextMeshProUGUI>().text = StaticFunction.getBadAddress()[rndNum];
                StaticFunction.setErrorNum(StaticFunction.getErrorNum() - 1);
                isFlag = true;
            }
            else
            {
                transform.parent.GetComponent<TMPro.TextMeshProUGUI>().text = StaticFunction.getGoodAddress()[rndNum];
            }
        }
        else
        {
            Transform postProfilePic = transform.parent.transform.Find("ProfilePic");
            Transform postName = transform.parent.transform.Find("Name");
            postProfilePic.GetComponent<Image>().sprite = profilePics[rndNum];
            postName.GetComponent<TMPro.TextMeshProUGUI>().text = StaticFunction.getNames()[rndNum];

            Transform caption = transform.parent.transform.Find("Caption");
            Transform photo = transform.parent.transform.Find("Photo");

            int rnd = UnityEngine.Random.Range(1, 5);
            if ((rnd == 1) && (StaticFunction.getErrorNum() > 0))
            {
                int rnd2 = UnityEngine.Random.Range(0, StaticFunction.getBadCaptions().Length);
                caption.GetComponent<TMPro.TextMeshProUGUI>().text = StaticFunction.getBadCaptions()[rnd2];
                photo.GetComponent<Image>().sprite = badPosts[rnd2];
                StaticFunction.setErrorNum(StaticFunction.getErrorNum() - 1);
                flagIndex = rnd2;
                isFlag = true;
            }
            else
            {
                int rnd2 = UnityEngine.Random.Range(0, StaticFunction.getGoodCaptions().Length); ;
                if (parentName == "Post1")
                {
                    post1Index = rnd2;
                }
                else
                {
                    if (post1Index > -1)
                    {
                        while (rnd2 == post1Index)
                        {
                            rnd2 = UnityEngine.Random.Range(0, StaticFunction.getGoodCaptions().Length);
                        }
                    }
                }
                caption.GetComponent<TMPro.TextMeshProUGUI>().text = StaticFunction.getGoodCaptions()[rnd2];
                photo.GetComponent<Image>().sprite = goodPosts[rnd2];
            }
        }

        Debug.Log(parentName + ": Setup done");
    }

    //for case where no flags were made
    private void CheckSetup()
    {        
        if ((StaticFunction.getErrorNum() > 0) && (parentName == "Location"))
        {
            transform.parent.GetComponent<TMPro.TextMeshProUGUI>().text = StaticFunction.getBadAddress()[rndNum];
            StaticFunction.setErrorNum(StaticFunction.getErrorNum() - 1);
            isFlag = true;
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
