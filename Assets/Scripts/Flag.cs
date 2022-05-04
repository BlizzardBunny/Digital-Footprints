using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Flag : MonoBehaviour
{
    public Sprite[] profilePics;
    public Sprite[] goodPosts, badPosts;

    public GameObject editableMAPrefab;

    public bool flaggedItem { get; private set; }
    public string parentName { get; private set; }
    public int flagIndex { get; private set; }
    public string snsName { get; private set; }

    private GameObject editableMA;
    private GameObject pointersPanel;

    private static int otherPostIndex;

    private void Awake()
    {
        pointersPanel = GameObject.FindGameObjectWithTag("PointersPanel");
        parentName = transform.parent.name;

        Transform t = transform.parent;
        while (snsName == null)
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
    }

    public int RandomFlagIndex(int maximum, int id)
    {
        int ret = -1;
        ret = UnityEngine.Random.Range(0, maximum);
        StaticFunction.flagIndexes.Add(ret);
        return ret;
    }

    public void SetupFlaggedItem(int profileNum, int id)
    {
        flaggedItem = true;
        StaticFunction.setErrorNum(StaticFunction.getErrorNum() - 1);
        Debug.Log("Is flag: " + parentName + " in " + snsName);

        if (parentName == "Address")
        {
            transform.parent.GetComponent<TMPro.TextMeshProUGUI>().text = StaticFunction.getBadAddress()[profileNum];
            StaticFunction.flags.Add(new StaticFunction.Triple(parentName, snsName, "Personal Information"));
        }
        else if (parentName.StartsWith("Post"))
        {            
            //make deets of post match deets of profile
            Transform postProfilePic = transform.parent.transform.Find("ProfilePic");
            Transform postName = transform.parent.transform.Find("Name");
            postProfilePic.GetComponent<Image>().sprite = profilePics[profileNum];
            postName.GetComponent<TMPro.TextMeshProUGUI>().text = StaticFunction.getNames()[profileNum];
            
            //randomize post content
            Transform caption = transform.parent.transform.Find("Caption");
            Transform photo = transform.parent.transform.Find("Photo");

            flagIndex = RandomFlagIndex(StaticFunction.getBadCaptions().Length, id);
            caption.GetComponent<TMPro.TextMeshProUGUI>().text = StaticFunction.getBadCaptions()[flagIndex];
            photo.GetComponent<Image>().sprite = badPosts[flagIndex];
            StaticFunction.flags.Add(new StaticFunction.Triple(parentName, snsName, StaticFunction.getCaptionFlags()[flagIndex]));
        }
        else if (transform.parent.parent.name == "PrivacyWindow")
        {
            Transform everyone = transform.parent.Find("Everyone");
            Transform friends = transform.parent.Find("Friends");

            Toggle everyoneToggle = everyone.GetComponent<Toggle>();
            Toggle friendsToggle = friends.GetComponent<Toggle>();

            everyoneToggle.isOn = true;
            friendsToggle.isOn = false;
            for (int i = 0; i < StaticFunction.getPrivacySettingChoices().Length; i++)
            {
                if (parentName.Equals(StaticFunction.getPrivacySettingChoices()[i]))
                {
                    StaticFunction.flags.Add(new StaticFunction.Triple(parentName, snsName, StaticFunction.getPrivacySettingFlags()[i]));
                    break;
                }
            }
        }
        else if (parentName == "Password")
        {
            Transform passwordField = transform.parent.Find("PasswordField");

            //randomize password
            flagIndex = RandomFlagIndex(StaticFunction.getBadPasswords().Length, id);

            string temp = "";
            if (StaticFunction.getBadPasswords()[flagIndex].Contains("["))
            {
                bool isInsideBrackets = false;
                foreach (char x in StaticFunction.getBadPasswords()[flagIndex])
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
                temp = StaticFunction.getBadPasswords()[flagIndex];
                temp = temp.Replace("[PROFILENAME]", StaticFunction.getNames()[profileNum]);
                temp = temp.Replace(' ', '_');
            }
            else if (temp.Equals("ADDRESS"))
            {
                temp = StaticFunction.getBadPasswords()[flagIndex];
                temp = temp.Replace("[ADDRESS]", StaticFunction.getGoodAddress()[profileNum]);
                temp = temp.Replace(", ", "_");
                temp = temp.Replace(' ', '_');
            }
            else if (temp.Equals(""))
            {
                temp = StaticFunction.getBadPasswords()[flagIndex];
            }

            passwordField.GetComponent<TMPro.TextMeshProUGUI>().text = temp;
            StaticFunction.flags.Add(new StaticFunction.Triple(parentName, snsName, StaticFunction.getPasswordFlags()[flagIndex]));
        }
    }

    public void SetupNonFlaggedItem(int profileNum, int id) 
    {
        flaggedItem = false;
        if (parentName == "Address")
        {
            transform.parent.GetComponent<TMPro.TextMeshProUGUI>().text = StaticFunction.getGoodAddress()[profileNum];
        }
        else if (parentName.StartsWith("Post"))
        {
            //make deets of post match deets of profile
            Transform postProfilePic = transform.parent.transform.Find("ProfilePic");
            Transform postName = transform.parent.transform.Find("Name");
            postProfilePic.GetComponent<Image>().sprite = profilePics[profileNum];
            postName.GetComponent<TMPro.TextMeshProUGUI>().text = StaticFunction.getNames()[profileNum];

            //randomize post content
            Transform caption = transform.parent.transform.Find("Caption");
            Transform photo = transform.parent.transform.Find("Photo");

            flagIndex = RandomFlagIndex(StaticFunction.getGoodCaptions().Length, id);
            if (otherPostIndex <= -1)
            {
                otherPostIndex = flagIndex;
            }
            else
            {
                if (flagIndex == otherPostIndex)
                {
                    flagIndex++;
                }

                if (flagIndex == StaticFunction.getGoodCaptions().Length)
                {
                    flagIndex -= 2;
                }
            }
            caption.GetComponent<TMPro.TextMeshProUGUI>().text = StaticFunction.getGoodCaptions()[flagIndex];
            photo.GetComponent<Image>().sprite = goodPosts[flagIndex];
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
            flagIndex = UnityEngine.Random.Range(0, StaticFunction.getGoodPasswords().Length);
            passwordField.GetComponent<TMPro.TextMeshProUGUI>().text = StaticFunction.getGoodPasswords()[flagIndex];
        }
    }

    public void FlagItem()
    {
        if (StaticFunction.tutorialStart && (pointersPanel != null))
        {
            PointerGenerator script = (PointerGenerator)pointersPanel.GetComponent(typeof(PointerGenerator));
            if (!flaggedItem)
            {
                if (!StaticFunction.editableIsDrawn)
                {
                    script.wrongItem(transform);
                }
            }
            else
            {
                if (!StaticFunction.editableIsDrawn)
                {
                    script.correctItem(transform, flagIndex);

                }
            }
        }

        if (!StaticFunction.editableIsDrawn)
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
                transform.rotation,
                GameObject.FindGameObjectWithTag("MessagesCanvas").transform);

            if (SceneManager.GetActiveScene().name.Equals("Tutorial"))
            {
                editableMA.transform.Find("Clear").GetComponent<Button>().interactable = false;
            }

            Transform item = editableMA.transform.Find("MessageField").Find("Item");
            Transform snsField = editableMA.transform.Find("SNSField");

            item.GetComponent<TMPro.TextMeshProUGUI>().text = parentName;
            snsField.GetComponent<TMPro.TextMeshProUGUI>().text = snsName;
            StaticFunction.editableIsDrawn = true;
            StaticFunction.setIsChecking(true);
            StaticFunction.setCurrFlag(flagIndex);
        }
        else
        {
            editableMA = GameObject.FindGameObjectWithTag("EditableMA");

            Transform item = editableMA.transform.Find("MessageField").Find("Item");

            if (item.GetComponent<TMPro.TextMeshProUGUI>().text != parentName)
            {
                Transform snsField = editableMA.transform.Find("SNSField");

                item.GetComponent<TMPro.TextMeshProUGUI>().text = parentName;
                snsField.GetComponent<TMPro.TextMeshProUGUI>().text = snsName;
                StaticFunction.editableIsDrawn = true;
                StaticFunction.setIsChecking(true);
                StaticFunction.setCurrFlag(flagIndex);
            }
            else
            {
                foreach (GameObject x in GameObject.FindGameObjectsWithTag("EditableMA"))
                {
                    Destroy(x);
                }

                StaticFunction.editableIsDrawn = false;
                StaticFunction.setIsChecking(false);
                StaticFunction.setCurrFlag(-1);
            }

            foreach (GameObject x in GameObject.FindGameObjectsWithTag("Categories"))
            {
                Destroy(x);
            }
        }
    }
}
