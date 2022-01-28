using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Submit : MonoBehaviour
{    
    public GameObject confirmationPrefab;
    public GameObject dialoguePrefab;
    public GameObject mistakeMessagePrefab;

    private GameObject confirmation;
    private GameObject dialogue;
    private GameObject mistakeMessage;
    private Transform profileNum;
    private Transform totalProfiles;
    private int errorsCaught = 0;
    private bool perfectProfile = false;

    // Start is called before the first frame update
    void Start()
    {
        profileNum = GameObject.FindGameObjectWithTag("Count").transform;
        totalProfiles = GameObject.FindGameObjectWithTag("Total").transform;
        totalProfiles.GetComponent<TMPro.TextMeshProUGUI>().text = "/" + StaticFunction.getTotalProfiles().ToString();
        profileNum.GetComponent<TMPro.TextMeshProUGUI>().text = StaticFunction.getProfileNum().ToString();
        GameObject.FindGameObjectWithTag("ErrorTag").GetComponent<TMPro.TextMeshProUGUI>().text = StaticFunction.getMistakes().ToString();
        //mistakeNotifLogos = new Sprite[2];
    }

    public void Confirm()
    {
        confirmation = Instantiate(
                confirmationPrefab,
                new Vector3(962.2625122070313f, 540.0f, 0.0f),
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
        mistakeMessage = Instantiate(
                mistakeMessagePrefab,
                new Vector3(281.0283203125f, 1022.5988159179688f - (114.8024f * StaticFunction.mistakeMessages.Count), 0.0f),
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

        ResetStage();

        //after final profile
        if (StaticFunction.tutorialStart)
        {
            SceneManager.LoadScene("AskDialogue");
        }
        else if (StaticFunction.getProfileNum() == StaticFunction.getTotalProfiles())
        {
            dialogue = Instantiate(
                dialoguePrefab,
                new Vector3(960.0f, 540.0f, 0.0f),
                Quaternion.identity,
                GameObject.FindGameObjectWithTag("World").transform);

            DialogueControls script = (DialogueControls) dialogue.transform.Find("Next").GetComponent(typeof(DialogueControls));
            
            if (StaticFunction.getMistakes() == StaticFunction.getTotalProfiles())
            {
                dialogue.transform.Find("Dialogue").GetComponent<TMPro.TextMeshProUGUI>().text = StaticFunction.getBadDialogue()[0];
                script.setDialogue(StaticFunction.getBadDialogue());
            }
            else if (StaticFunction.getMistakes() == 0)
            {
                dialogue.transform.Find("Dialogue").GetComponent<TMPro.TextMeshProUGUI>().text = StaticFunction.getPerfectDialogue()[0];
                script.setDialogue(StaticFunction.getPerfectDialogue());
            }
            else
            {
                dialogue.transform.Find("Dialogue").GetComponent<TMPro.TextMeshProUGUI>().text = StaticFunction.getGoodDialogue()[0];
                script.setDialogue(StaticFunction.getGoodDialogue());
            }
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
                    FlagSystem script = (FlagSystem)clickable.GetComponent(typeof(FlagSystem));

                    if ((clickable.transform.parent.name == itemName) && (script.getSNSName() == snsName))
                    {
                        if (!script.isFlag())
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
                            }
                            Debug.Log("Caught " + itemName);
                            errorsCaught++;
                        }
                        else if (itemName.StartsWith("Post"))
                        {
                            int flagIndex = script.getFlagIndex();

                            if (flagIndex <= -1)
                            {
                                Debug.Log(script.getSNSName() + " " + itemName + ", " + flagName + ": flagindex error");
                            }
                            else if (flagName != StaticFunction.getCaptionFlags()[flagIndex])
                            {
                                StaticFunction.setMistakes(StaticFunction.getMistakes() + 1);
                                MakeMistakeMessage(itemName + " is not " + flagName + ".\nIt should be " + StaticFunction.getCaptionFlags()[flagIndex]);
                            }
                            Debug.Log("Caught " + itemName);
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
                                        }
                                        Debug.Log("Caught " + itemName);
                                        errorsCaught++;
                                        break;
                                    }
                                }
                            }
                        }
                        else if (itemName == "Password")
                        {
                            int flagIndex = script.getFlagIndex();

                            if (flagIndex <= -1)
                            {
                                Debug.Log(script.getSNSName() + " " + itemName + ", " + flagName + ": flagindex error");
                            }
                            else if (flagName != StaticFunction.getPasswordFlags()[flagIndex])
                            {
                                StaticFunction.setMistakes(StaticFunction.getMistakes() + 1);
                                MakeMistakeMessage(itemName + " is not " + flagName + ".\nIt should be " + StaticFunction.getPasswordFlags()[flagIndex]);
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
            }
            else if (StaticFunction.getTotalErrors() == errorsCaught)
            {
                perfectProfile = true;
                MakeMistakeMessage("Great job!!! You got all the errors on that profile!");
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

        foreach (GameObject x in GameObject.FindGameObjectsWithTag("Clickable"))
        {
            FlagSystem script = (FlagSystem) x.GetComponent(typeof(FlagSystem));
            script.ResetStage();
        }

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
    }
}
