using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

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
    private int flagIndex;
    private int id;
    private static List<int> flagIds = new List<int>();
    private static int rndNum = -1;
    private static int otherPostIndex = -1;

    // Start is called before the first frame update
    private void Start()
    {
        parentName = transform.parent.name;
        flagIndex = -1;
        id = StaticFunction.instanceCounter;

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

        ResetStage();  
    }

    public int getFlagIndex()
    {
        return flagIndex;
    }

    public string getSNSName()
    {
        return snsName;
    }

    private void Setup()
    {
        if ((rndNum <= -1) && (StaticFunction.instanceCounter <= 0))
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

        bool flaggedItem = false;

        foreach (int j in flagIds)
        {
            if (id == j)
            {
                flaggedItem = true;
            }
        }

        if (flaggedItem) //this instance is a flag
        {
            StaticFunction.setErrorNum(StaticFunction.getErrorNum() - 1);
            Debug.Log("Setup(): " + snsName + " " + parentName + " id: " + id);

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
            else if (parentName == "Password")
            {
                Transform passwordField = transform.parent.Find("PasswordField");

                //randomize password
                int rnd2 = UnityEngine.Random.Range(0, StaticFunction.getBadPasswords().Length);

                string temp = "";
                if (StaticFunction.getBadPasswords()[rnd2].Contains('['))
                {
                    bool isInsideBrackets = false;
                    foreach (char x in StaticFunction.getBadPasswords()[rnd2])
                    {
                        if (x.Equals('['))
                        {
                            isInsideBrackets = true;
                        }
                        else if (x.Equals(']'))
                        {
                            isInsideBrackets = false;
                        }
                        else if (isInsideBrackets)
                        {
                            temp += x;
                        }
                    }             
                }

                if (temp.Equals("PROFILENAME"))
                {
                    temp = StaticFunction.getBadPasswords()[rnd2];
                    temp = temp.Replace("[PROFILENAME]", StaticFunction.getNames()[rndNum]);
                    temp = temp.Replace(' ', '_');
                }
                else if (temp.Equals("ADDRESS"))
                {
                    temp = StaticFunction.getBadPasswords()[rnd2];
                    temp = temp.Replace("[ADDRESS]", StaticFunction.getGoodAddress()[rndNum]);
                    temp = temp.Replace(", ", "_");
                    temp = temp.Replace(' ', '_');
                }
                else if (temp.Equals(""))
                {
                    temp = StaticFunction.getBadPasswords()[rnd2];
                }

                passwordField.GetComponent<TMPro.TextMeshProUGUI>().text = temp;
                flagIndex = rnd2;
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
            else if (parentName == "Password")
            {
                Transform passwordField = transform.parent.Find("PasswordField");

                //randomize password
                int rnd2 = UnityEngine.Random.Range(0, StaticFunction.getGoodPasswords().Length);
                passwordField.GetComponent<TMPro.TextMeshProUGUI>().text = StaticFunction.getGoodPasswords()[rnd2];
                flagIndex = rnd2;
            }
        }
    }

    public void Flag(Button clicked)
    {
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
            Transform snsField = editableMA.transform.Find("SNSField");

            messageField.GetComponent<TMPro.TextMeshProUGUI>().text = clicked.transform.parent.name;
            snsField.GetComponent<TMPro.TextMeshProUGUI>().text = snsName;
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

    public void ResetStage()
    {
        GameObject.FindGameObjectWithTag("MainWindow").GetComponent<Animator>().SetBool("isMinimized", true);

        if (StaticFunction.instanceCounter <= 0)
        {
            Debug.Log("===ROUND " + (StaticFunction.getProfileNum() + 1) + "===");

            flagIds.Clear();
            flagIds.TrimExcess();

            //setup stage reqs
            if (SceneManager.GetActiveScene().name == "Stage 1")
            {
                StaticFunction.setErrorNum(1);
                StaticFunction.setTotalProfiles(3);
                StaticFunction.setTotalErrors(1);
            }
            else if (SceneManager.GetActiveScene().name == "Stage 2")
            {
                StaticFunction.setErrorNum(3);
                StaticFunction.setTotalErrors(3);
                StaticFunction.setTotalProfiles(4);
                
                foreach (GameObject socialMediaPage in GameObject.FindGameObjectsWithTag("SocialMediaPage"))
                {
                    if (socialMediaPage.transform.name == "Photogram (Panel)")
                    {
                        socialMediaPage.SetActive(false);
                        break;
                    }
                }

                foreach (GameObject privacyWindow in GameObject.FindGameObjectsWithTag("PrivacyWindow"))
                {
                    privacyWindow.GetComponent<Animator>().SetBool("isMinimized", true);
                }

                foreach (GameObject password in GameObject.FindGameObjectsWithTag("PasswordWindow"))
                {
                    password.SetActive(false);
                }
            }
            else if (SceneManager.GetActiveScene().name == "Stage 3")
            {
                StaticFunction.setErrorNum(5);
                StaticFunction.setTotalErrors(5);
                StaticFunction.setTotalProfiles(5);

                foreach (GameObject privacyWindow in GameObject.FindGameObjectsWithTag("PrivacyWindow"))
                {
                    privacyWindow.GetComponent<Animator>().SetBool("isMinimized", true);
                }

                foreach (GameObject password in GameObject.FindGameObjectsWithTag("PasswordWindow"))
                {
                    password.GetComponent<Animator>().SetBool("isMinimized", true);
                }
            }

            //randomize flags
            //setup list
            List<int> clickableIDs = new List<int>();
            for (int i = 0; i < GameObject.FindGameObjectsWithTag("Clickable").Length; i++)
            {
                if (GameObject.FindGameObjectsWithTag("Clickable")[i].activeInHierarchy)
                {
                    clickableIDs.Add(i);
                }
            }

            //randomize list
            clickableIDs = clickableIDs.OrderBy(tvz => System.Guid.NewGuid()).ToList();

            //select x IDs to be flags (depends on stage)
            for (int i = 0; i < StaticFunction.getErrorNum(); i++)
            {
                flagIds.Add(clickableIDs[i]);
            }

            StaticFunction.setCurrFlag(-1);
            otherPostIndex = -1;
            StaticFunction.setErrorNum(1);
            rndNum = -1;            
        }
        
        Setup();
        StaticFunction.instanceCounter++;
    }

    public void Exit()
    {
        Application.Quit();
    }
}
