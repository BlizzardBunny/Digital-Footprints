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

    private string parentName;
    private bool isFlag;
    private int flagIndex;

    // Start is called before the first frame update
    void Start()
    {
        parentName = transform.parent.name;
        isFlag = false;
        flagIndex = -1;
        Setup();
        CheckSetup();
    }

    private void Setup()
    {
        if (parentName == "Location")
        {            
            int rnd = UnityEngine.Random.Range(1, 5);  
            Debug.Log(parentName + " " + rnd);              
            if ((rnd == 1) && (StaticFunction.getErrorNum() > 0))
            {
                int rnd2 = UnityEngine.Random.Range(0, StaticFunction.getBadAddress().Length);
                transform.parent.GetComponent<TMPro.TextMeshProUGUI>().text = StaticFunction.getBadAddress()[rnd2];
                StaticFunction.setErrorNum(StaticFunction.getErrorNum() - 1);
                isFlag = true;
            }
            else
            {
                int rnd2 = UnityEngine.Random.Range(0, StaticFunction.getGoodAddress().Length);
                transform.parent.GetComponent<TMPro.TextMeshProUGUI>().text = StaticFunction.getGoodAddress()[rnd2];
            }
        }
        else if (parentName == "Bio")
        {

        }
        else
        {
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

    void Proc()
    {

    }
}
