using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FlagSystem : MonoBehaviour
{
    public GameObject editableMAPrefab;
    public Sprite[] badPosts = new Sprite[StaticFunction.getBadCaptions().Length];
    public Sprite[] goodPosts = new Sprite[StaticFunction.getGoodCaptions().Length];
    public Sprite[] profilePics = new Sprite[StaticFunction.getNames().Length];

    private GameObject editableMA;
    private bool editableIsDrawn = false;

    private string snsName = "0";
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

        Transform t = transform.parent;
        while (snsName.Equals("0"))
        {
            if (t.tag == "SocialMediaPage")
            {
                snsName = t.name;
            }
            else
            {
                t = t.parent;
            }
        }

        if (instanceCounter <= 0)
        {
            if (SceneManager.GetActiveScene().name == "Stage 1")
            {
                StaticFunction.setErrorNum(1);
                StaticFunction.setTotalProfiles(3);
                
                foreach (GameObject socialMediaPage in GameObject.FindGameObjectsWithTag("SocialMediaPage"))
                {
                    socialMediaPage.SetActive(false);
                }
            }
            else if (SceneManager.GetActiveScene().name == "Stage 2")
            {
                StaticFunction.setErrorNum(3);
                StaticFunction.setTotalProfiles(4);
                foreach (GameObject socialMediaPage in GameObject.FindGameObjectsWithTag("SocialMediaPage"))
                {
                    if (socialMediaPage.transform.name == "Photogram (Panel)")
                    {
                        socialMediaPage.SetActive(false);
                        break;
                    }
                }
            }
            else if (SceneManager.GetActiveScene().name == "Stage 3")
            {
                StaticFunction.setErrorNum(5);
                StaticFunction.setTotalProfiles(5);
            }
        }          
        
        Setup();
        CheckSetup();
    }

    public int getFlagIndex()
    {
        return flagIndex;
    }

    public void setFlagIndex(int i)
    {
        flagIndex = i;
    }

    public bool getIsFlag()
    {
        return isFlag;
    }

    public void setIsFlag(bool b)
    {
        isFlag = b;
    }

    private void Setup()
    {        
        if ((rndNum <= -1) && (instanceCounter <= 0))
        {
            rndNum = UnityEngine.Random.Range(0, profilePics.Length);
        }

        try
        {
            Transform name = transform.parent.parent.transform.Find("Name");
            Transform profile = transform.parent.parent.transform.Find("ProfilePic");
            Transform bio = transform.parent.parent.transform.Find("Bio");

            name.GetComponent<TMPro.TextMeshProUGUI>().text = StaticFunction.getNames()[rndNum];
            profile.GetComponent<Image>().sprite = profilePics[rndNum];
            bio.GetComponent<TMPro.TextMeshProUGUI>().text = StaticFunction.getBios()[rndNum];
        }
        catch (Exception)
        {

        }

        int rnd = UnityEngine.Random.Range(1, GameObject.FindGameObjectsWithTag("Clickable").Length); //determine if this instance is a flag [random]

        if ((rnd == 1) && (StaticFunction.getErrorNum() > 0)) //this instance is a flag
        {
            StaticFunction.setErrorNum(StaticFunction.getErrorNum() - 1);
            isFlag = true;
            Debug.Log("Setup(): " + snsName + " " + parentName);

            if (parentName == "Address")
            {
                transform.parent.GetComponent<TMPro.TextMeshProUGUI>().text = StaticFunction.getBadAddress()[rndNum];
            }
            else if (parentName.StartsWith("Post"))
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
            else if (transform.parent.parent.name == "PrivacyWindow")
            {
                Transform everyone = transform.parent.Find("Everyone");
                Transform friends = transform.parent.Find("Friends");

                Toggle everyoneToggle = everyone.GetComponent<Toggle>();
                Toggle friendsToggle = friends.GetComponent<Toggle>();

                everyoneToggle.isOn = true;
                friendsToggle.isOn = false;
            }
        }
        else //not a flag
        {
            if (parentName == "Address")
            {
                transform.parent.GetComponent<TMPro.TextMeshProUGUI>().text = StaticFunction.getGoodAddress()[rndNum];
            }
            else if (parentName.StartsWith("Post"))
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
            else if (transform.parent.parent.name == "PrivacyWindow")
            {
                Transform everyone = transform.parent.Find("Everyone");
                Transform friends = transform.parent.Find("Friends");

                Toggle everyoneToggle = everyone.GetComponent<Toggle>();
                Toggle friendsToggle = friends.GetComponent<Toggle>();

                everyoneToggle.isOn = false;
                friendsToggle.isOn = true;
            }
        }
    }

    //for case where not enough flags were made
    private void CheckSetup()
    {        
        instanceCounter++;

        if (instanceCounter == GameObject.FindGameObjectsWithTag("Clickable").Length)
        {
            if ((StaticFunction.getErrorNum() > 0))
            {
                foreach (GameObject clickable in GameObject.FindGameObjectsWithTag("Clickable"))
                {
                    FlagSystem script = (FlagSystem)clickable.GetComponent(typeof(FlagSystem));

                    if (!script.getIsFlag())
                    {
                        StaticFunction.setErrorNum(StaticFunction.getErrorNum() - 1);
                        script.setIsFlag(true);
                        String parentName = clickable.transform.parent.name;
                        Debug.Log("CheckSetup() " + snsName + " " + parentName) ;

                        if (parentName == "Address")
                        {
                            clickable.transform.parent.GetComponent<TMPro.TextMeshProUGUI>().text = StaticFunction.getBadAddress()[rndNum];
                        }
                        else if (parentName.StartsWith("Post"))
                        {
                            //make deets of post match deets of profile
                            Transform postProfilePic = clickable.transform.parent.transform.Find("ProfilePic");
                            Transform postName = clickable.transform.parent.transform.Find("Name");
                            postProfilePic.GetComponent<Image>().sprite = profilePics[rndNum];
                            postName.GetComponent<TMPro.TextMeshProUGUI>().text = StaticFunction.getNames()[rndNum];

                            //randomize post content
                            Transform caption = clickable.transform.parent.transform.Find("Caption");
                            Transform photo = clickable.transform.parent.transform.Find("Photo");

                            int rnd2 = UnityEngine.Random.Range(0, StaticFunction.getBadCaptions().Length);
                            caption.GetComponent<TMPro.TextMeshProUGUI>().text = StaticFunction.getBadCaptions()[rnd2];
                            photo.GetComponent<Image>().sprite = badPosts[rnd2];
                            script.setFlagIndex(rnd2);
                        }
                        else if (transform.parent.parent.name == "PrivacyWindow")
                        {
                            Transform everyone = clickable.transform.parent.Find("Everyone");
                            Transform friends = clickable.transform.parent.Find("Friends");

                            Toggle everyoneToggle = everyone.GetComponent<Toggle>();
                            Toggle friendsToggle = friends.GetComponent<Toggle>();

                            everyoneToggle.isOn = true;
                            friendsToggle.isOn = false;
                        }
                    }

                    if (StaticFunction.getErrorNum() == 0)
                    {
                        break;
                    }
                }                
            }

            instanceCounter = 0;
        }
    }

    public void Flag(Button clicked)
    {
        Debug.Log(parentName + " isFlag:" + isFlag + " editableIsDrawn:" + editableIsDrawn);
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

            editableMA = Instantiate(
                editableMAPrefab,
                new Vector3(1753.4014892578125f, 685.0f, 0.0f),
                clicked.transform.rotation,
                GameObject.FindGameObjectWithTag("MessagesCanvas").transform);

            Transform messageField = editableMA.transform.Find("MessageField");

            messageField.GetComponent<TMPro.TextMeshProUGUI>().text = clicked.transform.parent.name;
            editableIsDrawn = true;
            StaticFunction.setIsChecking(true);
            StaticFunction.setCurrFlag(flagIndex);
        }
        else
        {
            foreach(GameObject x in GameObject.FindGameObjectsWithTag("EditableMA"))
            {
                Destroy(x);
            }

            foreach (GameObject x in GameObject.FindGameObjectsWithTag("Categories"))
            {
                Destroy(x);
            }

            editableIsDrawn = false;
            StaticFunction.setIsChecking(false);
            StaticFunction.setCurrFlag(-1);
        }
    }

    public void Reset()
    {
        GameObject.FindGameObjectWithTag("PrivacyWindow").GetComponent<Animator>().SetBool("isMinimized", false);
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

            foreach (GameObject x in GameObject.FindGameObjectsWithTag("ReportEntry"))
            {
                Destroy(x);
            }
        }
        
        Setup();
        CheckSetup();

        Debug.Log(parentName + " reset");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
