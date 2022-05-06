using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using static TutorialPlayer;

public class Submit : MonoBehaviour
{
    public GameObject confirmationPrefab;
    public GameObject dialoguePrefab;
    public GameObject mistakeMessagePrefab;

    private Canvas worldCanvas;
    private GameObject confirmation;
    private GameObject dialogue;
    private GameObject mistakeMessage;
    private Transform profileNum;
    private Transform totalProfiles;
    private int errorsCaught = 0;
    private int wrongFlags = 0;
    private bool perfectProfile = false;

    private Dialogue[] roundMessagesPerfect = new Dialogue[]
    {
        new Dialogue("Excellent, it seems that you fully grasp how our system works here at Digital Footprints."),
        new Dialogue("Do be reminded that as you gain trust with the company, you will be given more responsibilities"),
        new Dialogue("Make sure to keep up the good work!")
    };

    private Dialogue[] roundMessagesGood = new Dialogue[]
    {
        new Dialogue("While you did make some mistakes here and there, I hope that this did help you understand how our system works."),
        new Dialogue("Do be reminded that as you gain trust with the company, you will be given more responsibilities"),
        new Dialogue("I look forward to seeing you improve!")
    };

    private Dialogue[] roundMessagesBad = new Dialogue[]
    {
        new Dialogue("I strongly recommend reviewing today�s work to understand what went wrong."),
        new Dialogue("Do be reminded that as you gain trust with the company, you will be given more responsibilities"),
        new Dialogue("Please take this time to improve.")
    };

    // Start is called before the first frame update
    void Start()
    {
        profileNum = GameObject.FindGameObjectWithTag("Count").transform;
        totalProfiles = GameObject.FindGameObjectWithTag("Total").transform;
        totalProfiles.GetComponent<TMPro.TextMeshProUGUI>().text = "/" + StaticFunction.getTotalProfiles().ToString();
        profileNum.GetComponent<TMPro.TextMeshProUGUI>().text = StaticFunction.getProfileNum().ToString();
        GameObject.FindGameObjectWithTag("ErrorTag").GetComponent<TMPro.TextMeshProUGUI>().text = StaticFunction.getMistakes().ToString();
        worldCanvas = GameObject.FindGameObjectWithTag("World").GetComponent<Canvas>();
        //mistakeNotifLogos = new Sprite[2];
    }

    public void Confirm()
    {
        confirmation = Instantiate(
                confirmationPrefab,
                GameObject.FindGameObjectWithTag("World").transform.position,
                Quaternion.identity,
                GameObject.FindGameObjectWithTag("World").transform
        );
    }

    public void Cancel()
    {
        foreach (GameObject x in GameObject.FindGameObjectsWithTag("Confirmation"))
        {
            Destroy(x);
        }
    }

    public void MakeMistakeMessage(string message)
    {
        RectTransform parent = GameObject.FindGameObjectWithTag("World").GetComponent<RectTransform>();
        RectTransform notifSize = mistakeMessagePrefab.GetComponent<RectTransform>();
        mistakeMessage = Instantiate(
                mistakeMessagePrefab,
                parent.position +
                        new Vector3(
                            -((parent.rect.width * worldCanvas.scaleFactor) / 2) + ((notifSize.rect.width * worldCanvas.scaleFactor) / 2),
                            ((parent.rect.height * worldCanvas.scaleFactor) / 2) - ((notifSize.rect.height * worldCanvas.scaleFactor) / 2),
                            0.0f) - new Vector3(0.0f, ((notifSize.rect.height * worldCanvas.scaleFactor)) * StaticFunction.mistakeMessages.Count, 0.0f),
                Quaternion.identity,
                GameObject.FindGameObjectWithTag("World").transform
        );

        if (perfectProfile)
        {
            mistakeMessage.transform.Find("Notif Logo").GetComponent<Image>().sprite = mistakeMessage.transform.Find("Perfect Logo").GetComponent<Image>().sprite;
            perfectProfile = false;
        }
        else
        {
            mistakeMessage.transform.Find("Notif Logo").GetComponent<Image>().sprite = mistakeMessage.transform.Find("Mistake Logo").GetComponent<Image>().sprite;
        }

        mistakeMessage.transform.Find("Message").GetComponent<TMPro.TextMeshProUGUI>().text = message;
        mistakeMessage.transform.Find("ID").GetComponent<TMPro.TextMeshProUGUI>().text = StaticFunction.mistakeMessages.Count.ToString();
        Animator anim = mistakeMessage.GetComponent<Animator>();
        anim.SetBool("isFadingOut", false);

        StaticFunction.mistakeMessages.Add(mistakeMessage);
    }

    public void SubmitReport()
    {
        errorsCaught = 0;
        perfectProfile = false; 

        if (GameObject.FindGameObjectsWithTag("MistakeNotif").Length == 0)
        {
            StaticFunction.mistakeMessages.Clear();
        }

        CheckCorrectness();

        if (StaticFunction.getProfileNum() <= StaticFunction.getTotalProfiles())
        {
            ResetStage();
        }

        //after final profile
        if (StaticFunction.tutorialStart)
        {
            StaticFunction.roundHasStarted = false;
            PointerGenerator script = (PointerGenerator)(GameObject.FindGameObjectWithTag("PointersPanel")).GetComponent(typeof(PointerGenerator));

            script.endTutorial();
        }
        else if (StaticFunction.getProfileNum() == StaticFunction.getTotalProfiles())
        {
            StaticFunction.dialogueIndex = 0;

            TutorialPlayer script = (TutorialPlayer)GameObject.FindGameObjectWithTag("World").GetComponent(typeof(TutorialPlayer));

            if (StaticFunction.getMistakes() >= StaticFunction.getTotalProfiles())
            {
                script.run(roundMessagesBad);
            }
            else if (StaticFunction.getMistakes() == 0)
            {
                script.run(roundMessagesPerfect);
            }
            else
            {
                script.run(roundMessagesGood);
            }

            StaticFunction.reloadSameStage = false;
            StaticFunction.roundHasStarted = false;
            StaticFunction.gotoLevelSelect = true;
        }
    }

    void CheckCorrectness()
    {
        if (GameObject.FindGameObjectsWithTag("ReportEntry").Length == 0)
        {
            StaticFunction.setMistakes(StaticFunction.getMistakes() + StaticFunction.getTotalErrors());
            if (StaticFunction.getTotalErrors() == 1)
            {
                MakeMistakeMessage("No reports were made. You incur " + StaticFunction.getTotalErrors() + " penalty.");
            }
            else
            {
                MakeMistakeMessage("No reports were made. You incur " + StaticFunction.getTotalErrors() + " penalties.");
            }

            foreach (StaticFunction.Triple flag in StaticFunction.flags)
            {
                MakeMistakeMessage(flag.Item1 + " in " + flag.Item2.Replace(" (Panel)","") + " should be " + flag.Item3);
            }
        }
        else
        {
            foreach (GameObject reportEntry in GameObject.FindGameObjectsWithTag("ReportEntry"))
            {
                string itemName = reportEntry.transform.Find("ItemName").GetComponent<TMPro.TextMeshProUGUI>().text;
                string flagName = reportEntry.transform.Find("FlagName").GetComponent<TMPro.TextMeshProUGUI>().text;
                string snsName = reportEntry.transform.Find("SNSName").GetComponent<TMPro.TextMeshProUGUI>().text;

                foreach (GameObject clickable in GameObject.FindGameObjectsWithTag("Clickable"))
                {
                    Flag script = (Flag)clickable.GetComponent(typeof(Flag));

                    if ((script.parentName == itemName) && (script.snsName == snsName))
                    {
                        if (!script.flaggedItem)
                        {
                            StaticFunction.setMistakes(StaticFunction.getMistakes() + 1);
                            MakeMistakeMessage(itemName + " is not a flag.");
                        }
                        else if (itemName == "Address")
                        {
                            if (flagName != "Personal Information")
                            {
                                StaticFunction.setMistakes(StaticFunction.getMistakes() + 1);
                                MakeMistakeMessage(itemName + " is not " + flagName + ".\nIt should be Personal Information.");
                                wrongFlags++;
                                foreach (StaticFunction.Triple flag in StaticFunction.flags)
                                {
                                    if ((flag.Item1 == itemName) && (flag.Item2 == snsName) && (flag.Item3 == "Personal Information"))
                                    {
                                        StaticFunction.flags.Remove(flag);
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                Debug.Log("Caught " + itemName);

                                foreach (StaticFunction.Triple flag in StaticFunction.flags)
                                {
                                    if ((flag.Item1 == itemName) && (flag.Item2 == snsName) && (flag.Item3 == flagName))
                                    {
                                        StaticFunction.flags.Remove(flag);
                                        break;
                                    }
                                }
                            }
                            errorsCaught++;
                        }
                        else if (itemName.StartsWith("Post"))
                        {
                            int flagIndex = script.flagIndex;

                            if (flagIndex <= -1)
                            {
                                Debug.Log(script.snsName + " " + itemName + ", " + flagName + ": flagindex error");
                            }
                            else if (flagName != StaticFunction.getCaptionFlags()[flagIndex])
                            {
                                StaticFunction.setMistakes(StaticFunction.getMistakes() + 1);
                                MakeMistakeMessage(itemName + " is not " + flagName + ".\nIt should be " + StaticFunction.getCaptionFlags()[flagIndex]);
                                wrongFlags++;
                                foreach (StaticFunction.Triple flag in StaticFunction.flags)
                                {
                                    if ((flag.Item1 == itemName) && (flag.Item2 == snsName) && (flag.Item3 == StaticFunction.getCaptionFlags()[flagIndex]))
                                    {
                                        StaticFunction.flags.Remove(flag);
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                Debug.Log("Caught " + itemName);
                                foreach (StaticFunction.Triple flag in StaticFunction.flags)
                                {
                                    if ((flag.Item1 == itemName) && (flag.Item2 == snsName) && (flag.Item3 == flagName))
                                    {
                                        StaticFunction.flags.Remove(flag);
                                        break;
                                    }
                                }
                            }
                            errorsCaught++;
                        }
                        else if (clickable.transform.parent.parent.name == "PrivacyWindow")
                        {
                            Toggle everyoneToggle = clickable.transform.parent.Find("Everyone").GetComponent<Toggle>();
                            Toggle friendsToggle = clickable.transform.parent.Find("Friends").GetComponent<Toggle>();

                            if ((!everyoneToggle.isOn) && (friendsToggle.isOn))
                            {
                                StaticFunction.setMistakes(StaticFunction.getMistakes() + 1);
                                MakeMistakeMessage(itemName + " is not a flag. It is set to FriendsOnly.");
                            }
                            else
                            {
                                for (int i = 0; i < StaticFunction.getPrivacySettingChoices().Length; i++)
                                {
                                    if (StaticFunction.getPrivacySettingChoices()[i].Equals(itemName))
                                    {
                                        if (!StaticFunction.getPrivacySettingFlags()[i].Equals(flagName))
                                        {
                                            StaticFunction.setMistakes(StaticFunction.getMistakes() + 1);
                                            MakeMistakeMessage(itemName + " is not " + flagName + ".\nIt should be " + StaticFunction.getPrivacySettingFlags()[i]);
                                            wrongFlags++;
                                            foreach (StaticFunction.Triple flag in StaticFunction.flags)
                                            {
                                                if ((flag.Item1 == itemName) && (flag.Item2 == snsName) && (flag.Item3 == StaticFunction.getPrivacySettingFlags()[i]))
                                                {
                                                    StaticFunction.flags.Remove(flag);
                                                    break;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            Debug.Log("Caught " + itemName);
                                            foreach (StaticFunction.Triple flag in StaticFunction.flags)
                                            {
                                                if ((flag.Item1 == itemName) && (flag.Item2 == snsName) && (flag.Item3 == flagName))
                                                {
                                                    StaticFunction.flags.Remove(flag);
                                                    break;
                                                }
                                            }
                                        }
                                        errorsCaught++;
                                        break;
                                    }
                                }
                            }
                        }
                        else if (itemName == "Password")
                        {
                            int flagIndex = script.flagIndex;

                            if (flagIndex <= -1)
                            {
                                Debug.Log(script.snsName + " " + itemName + ", " + flagName + ": flagindex error");
                            }
                            else if (flagName != StaticFunction.getPasswordFlags()[flagIndex])
                            {
                                StaticFunction.setMistakes(StaticFunction.getMistakes() + 1);
                                MakeMistakeMessage(itemName + " is not " + flagName + ".\nIt should be " + StaticFunction.getPasswordFlags()[flagIndex]);
                                wrongFlags++;
                                foreach (StaticFunction.Triple flag in StaticFunction.flags)
                                {
                                    if ((flag.Item1 == itemName) && (flag.Item2 == snsName) && (flag.Item3 == StaticFunction.getPasswordFlags()[flagIndex]))
                                    {
                                        StaticFunction.flags.Remove(flag);
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                Debug.Log("Caught " + itemName);
                                foreach (StaticFunction.Triple flag in StaticFunction.flags)
                                {
                                    if ((flag.Item1 == itemName) && (flag.Item2 == snsName) && (flag.Item3 == flagName))
                                    {
                                        StaticFunction.flags.Remove(flag);
                                        break;
                                    }
                                }
                            }
                            errorsCaught++;
                        }

                        break;
                    }
                }
            }

            //check for not enough errors caught
            if (StaticFunction.getTotalErrors() > errorsCaught)
            {
                StaticFunction.setMistakes(StaticFunction.getMistakes() + (StaticFunction.getTotalErrors() - errorsCaught));
                MakeMistakeMessage("You missed " + (StaticFunction.getTotalErrors() - errorsCaught) + " errors! You incur that many mistakes.");

                foreach (StaticFunction.Triple flag in StaticFunction.flags)
                {
                    MakeMistakeMessage(flag.Item1 + " in " + flag.Item2.Replace(" (Panel)", "") + " should be " + flag.Item3);
                }
            }
            else if (StaticFunction.getTotalErrors() == errorsCaught)
            {
                if (wrongFlags == 0)
                {
                    perfectProfile = true;
                    MakeMistakeMessage("Great job!!! You got all the errors on that profile!");
                }
            }
        }
    }

    void ResetStage()
    {
        MakeMistakeMessage("New profile loaded.");

        GameObject.FindGameObjectWithTag("ErrorTag").GetComponent<TMPro.TextMeshProUGUI>().text = StaticFunction.getMistakes().ToString();

        //reset stage
        StaticFunction.setProfileNum(StaticFunction.getProfileNum() + 1);
        profileNum.GetComponent<TMPro.TextMeshProUGUI>().text = StaticFunction.getProfileNum().ToString();

        //setInstanceCounter
        StaticFunction.instanceCounter = 0;

        Animator mainWindowAnim = GameObject.FindGameObjectWithTag("MainWindow").GetComponent<Animator>();
        mainWindowAnim.SetBool("isMinimized", true);

        foreach (GameObject x in GameObject.FindGameObjectsWithTag("Confirmation"))
        {
            Destroy(x);
        }

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

        FlagSystemSetup script = (FlagSystemSetup)GameObject.FindGameObjectWithTag("World").GetComponent(typeof(FlagSystemSetup));
        script.ResetRound();
    }
}
