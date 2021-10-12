using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FlagSystem : MonoBehaviour
{
    [SerializeField] GameObject editableMAPrefab;
    public Sprite[] badPosts = new Sprite[StaticFunction.getBadCaptions().Length];
    public Sprite[] goodPosts = new Sprite[StaticFunction.getGoodCaptions().Length];
    public Sprite[] profilePics = new Sprite[StaticFunction.getNames().Length];

    private GameObject editableMA;
    private bool editableIsDrawn = false;

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
            isFlag = false;
            flagIndex = -1;
            if (instanceCounter <= 0)
            {
                StaticFunction.setCurrFlag(-1);
                otherPostIndex = -1;
                StaticFunction.setErrorNum(1);
                rndNum = -1;

                foreach (GameObject x in GameObject.FindGameObjectsWithTag("EditableMA"))
                {
                    Destroy(x);
                }

                foreach (GameObject x in GameObject.FindGameObjectsWithTag("Categories"))
                {
                    Destroy(x);
                }
            }
            Setup();
            CheckSetup();
        }
    }

    private void Setup()
    {
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

        if ((rnd == 1) && (StaticFunction.getErrorNum() > 0)) //this instance is a flag
        {
            StaticFunction.setErrorNum(StaticFunction.getErrorNum() - 1);
            isFlag = true;
            Debug.Log(parentName + " " + isFlag + " " + editableIsDrawn);

            if (parentName == "Address") //not a post prefab
            {
                transform.parent.GetComponent<TMPro.TextMeshProUGUI>().text = StaticFunction.getBadAddress()[rndNum];
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
            }
        }
        else //not a flag
        {
            if (parentName == "Address") //not a post prefab
            {
                transform.parent.GetComponent<TMPro.TextMeshProUGUI>().text = StaticFunction.getGoodAddress()[rndNum];
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

                    if (rnd2 == StaticFunction.getGoodCaptions().Length)
                    {
                        rnd2 -= 2;
                    }
                }
                caption.GetComponent<TMPro.TextMeshProUGUI>().text = StaticFunction.getGoodCaptions()[rnd2];
                photo.GetComponent<Image>().sprite = goodPosts[rnd2];
            }
        }
    }

    //for case where no flags were made
    private void CheckSetup()
    {        
        instanceCounter++;

        if (instanceCounter == GameObject.FindGameObjectsWithTag("Clickable").Length)
        {
            if ((StaticFunction.getErrorNum() > 0))
            {
                StaticFunction.setErrorNum(StaticFunction.getErrorNum() - 1);
                isFlag = true;
                Debug.Log(parentName + " " + isFlag + " " + editableIsDrawn);

                if (parentName == "Address") //not a post prefab
                {
                    transform.parent.GetComponent<TMPro.TextMeshProUGUI>().text = StaticFunction.getBadAddress()[rndNum];
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
                }
            }

            instanceCounter = 0;
        }
    }

    public void Flag(Button clicked)
    {
        Debug.Log(parentName + " " + isFlag + " " + editableIsDrawn);
        if (!editableIsDrawn)
        {
            try
            {
                foreach (GameObject x in GameObject.FindGameObjectsWithTag("EditableMA"))
                {
                    Destroy(x);
                }
            }
            catch
            {
            }
            
            if (isFlag)
            {
                editableMA = Instantiate(
                editableMAPrefab,
                new Vector3(1725.1201171875f, 666.411865234375f, 0.0f),
                clicked.transform.rotation,
                clicked.transform.parent.transform.parent.transform.parent.transform.parent); //can't use a tag here because it's the specific SNS parent

                Transform messageField = editableMA.transform.Find("MessageField");

                messageField.GetComponent<TMPro.TextMeshProUGUI>().text = clicked.transform.parent.name;
                editableIsDrawn = true;
                StaticFunction.setIsChecking(true);
                StaticFunction.setCurrFlag(flagIndex);
            }
        }
        else
        {
            foreach(GameObject x in GameObject.FindGameObjectsWithTag("EditableMA"))
            {
                Destroy(x);
            }
            editableIsDrawn = false;
            StaticFunction.setIsChecking(false);
            StaticFunction.setCurrFlag(-1);
        }
    }

    public void Exit()
    {
        Application.Quit();
    }
}
