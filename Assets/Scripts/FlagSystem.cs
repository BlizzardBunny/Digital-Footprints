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
    private Transform origCaptionTransform;
    private Transform origPhotoTransform;

    // Start is called before the first frame update
    void Start()
    {
        parentName = transform.parent.name;
        isFlag = false;
        flagIndex = -1;
        if (rndNum <= -1)
        {
            rndNum = UnityEngine.Random.Range(0, profilePics.Length);
        }

        if (parentName != "Location")
        {
            origCaptionTransform = transform.parent.transform.Find("Caption");
            origPhotoTransform = transform.parent.transform.Find("Photo");
        }
        Setup();
        CheckSetup();
    }

    private void Setup()
    {
        Transform name = transform.parent.parent.transform.Find("Name");
        Transform profile = transform.parent.parent.transform.Find("ProfilePic");
        Transform bio = transform.parent.parent.transform.Find("Bio");

        name.GetComponent<TMPro.TextMeshProUGUI>().text = StaticFunction.getNames()[rndNum];
        profile.GetComponent<Image>().sprite = profilePics[rndNum];
        bio.GetComponent<TMPro.TextMeshProUGUI>().text = StaticFunction.getBios()[rndNum];

        if (parentName == "Location")
        {            
            int rnd = UnityEngine.Random.Range(1, 5);  
            Debug.Log(parentName + " " + rnd);              
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
            Debug.Log(parentName + " " + rnd);
            if ((rnd == 1) && (StaticFunction.getErrorNum() > 0))
            {
                int rnd2 = UnityEngine.Random.Range(0, StaticFunction.getBadCaptions().Length);
                caption.GetComponent<TMPro.TextMeshProUGUI>().text = StaticFunction.getBadCaptions()[rnd2];
                photo.GetComponent<Image>().sprite = badPosts[rnd2];
                StaticFunction.setErrorNum(StaticFunction.getErrorNum() - 1);
                isFlag = true;
            }
            else
            {
                int rnd2 = UnityEngine.Random.Range(0, StaticFunction.getGoodCaptions().Length);
                caption.GetComponent<TMPro.TextMeshProUGUI>().text = StaticFunction.getGoodCaptions()[rnd2];
                photo.GetComponent<Image>().sprite = goodPosts[rnd2];
            }

            if (photo.GetComponent<Image>().sprite == null)
            {
                caption.GetComponent<RectTransform>().position = photo.GetComponent<RectTransform>().position;
                caption.GetComponent<RectTransform>().sizeDelta = photo.GetComponent<RectTransform>().sizeDelta;
                caption.GetComponent<TMP_Text>().fontSize = 100f;
                photo.GetComponent<Image>().color = new Color32((byte)UnityEngine.Random.Range(0,255), (byte)UnityEngine.Random.Range(0, 255), (byte)UnityEngine.Random.Range(0, 255), 128);
            }
        }
    }

    //for the case when all the posts end up safe
    private void CheckSetup()
    {
        if ((StaticFunction.getErrorNum() > 0) && (parentName == "Post2"))
        {
            Transform caption = transform.parent.transform.Find("Caption");
            Transform photo = transform.parent.transform.Find("Photo");

            int rnd = UnityEngine.Random.Range(0, StaticFunction.getBadCaptions().Length);
            caption.GetComponent<TMPro.TextMeshProUGUI>().text = StaticFunction.getBadCaptions()[rnd];
            photo.GetComponent<Image>().sprite = badPosts[rnd];
            StaticFunction.setErrorNum(StaticFunction.getErrorNum() - 1);
            isFlag = true;
        }
    }

    public void Proc()
    {

    }

    public void Exit()
    {
        Application.Quit();
    }
}
