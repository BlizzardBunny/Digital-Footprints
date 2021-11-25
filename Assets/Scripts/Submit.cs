using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Submit : MonoBehaviour
{    
    public GameObject confirmationPrefab;
    public GameObject dialoguePrefab;

    private GameObject confirmation;
    private GameObject dialogue;
    private Transform profileNum;
    private Transform totalProfiles;

    // Start is called before the first frame update
    void Start()
    {
        profileNum = GameObject.FindGameObjectWithTag("Count").transform;
        totalProfiles = GameObject.FindGameObjectWithTag("Total").transform;
        totalProfiles.GetComponent<TMPro.TextMeshProUGUI>().text = "/" + StaticFunction.getTotalProfiles().ToString();
        profileNum.GetComponent<TMPro.TextMeshProUGUI>().text = StaticFunction.getProfileNum().ToString();
        GameObject.FindGameObjectWithTag("ErrorTag").GetComponent<TMPro.TextMeshProUGUI>().text = StaticFunction.getMistakes().ToString();
    }

    public void Confirm()
    {
        confirmation = Instantiate(
                confirmationPrefab,
                new Vector3(962.2625122070313f,540.0f,0.0f),
                Quaternion.identity,
                GameObject.FindGameObjectWithTag("World").transform);
    }

    public void Cancel()
    {
        foreach (GameObject x in GameObject.FindGameObjectsWithTag("Confirmation"))
        {
            Destroy(x);
        }
    }

    public void SubmitReport()
    {
        //check correctness
		
		if (GameObject.FindGameObjectsWithTag("ReportEntry").Length == 0)
		{
			StaticFunction.setMistakes(StaticFunction.getMistakes() + StaticFunction.getTotalErrors());
		}
		else
		{
            foreach(GameObject reportEntry in GameObject.FindGameObjectsWithTag("ReportEntry"))
			{
				string itemName = reportEntry.transform.Find("ItemName").GetComponent<TMPro.TextMeshProUGUI>().text;
				string flagName = reportEntry.transform.Find("FlagName").GetComponent<TMPro.TextMeshProUGUI>().text;
                string snsName = reportEntry.transform.Find("SNSName").GetComponent<TMPro.TextMeshProUGUI>().text;

                foreach (GameObject clickable in GameObject.FindGameObjectsWithTag("Clickable"))
				{
					FlagSystem script = (FlagSystem) clickable.GetComponent(typeof(FlagSystem));

					if ((clickable.transform.parent.name == itemName) && (script.getSNSName() == snsName))
					{
                        if ((itemName == "Address") && (flagName != "Personal Information"))
                        {
                            StaticFunction.setMistakes(StaticFunction.getMistakes() + 1);
                            Debug.Log(script.getSNSName() + " " + itemName + ", " + flagName + ": address is not flagged as personal info");
                        }
                        else if (itemName.StartsWith("Post"))
                        {
                            int flagIndex = script.getFlagIndex();

                            if (flagIndex <= -1)
                            {
                                StaticFunction.setMistakes(StaticFunction.getMistakes() + 1);
                                Debug.Log(script.getSNSName() + " " + itemName + ", " + flagName + ": flagindex error");
                            }
                            else if (flagName != StaticFunction.getCaptionFlags()[flagIndex])
                            {
                                StaticFunction.setMistakes(StaticFunction.getMistakes() + 1);
                                Debug.Log(script.getSNSName() + " " + itemName + ", " + flagName + ": wrong post flag");
                            }
                        }
                        else if (clickable.transform.parent.parent.name == "PrivacyWindow")
                        {
                            Toggle everyoneToggle = clickable.transform.parent.Find("Everyone").GetComponent<Toggle>();
                            Toggle friendsToggle = clickable.transform.parent.Find("Friends").GetComponent<Toggle>();

                            if ((!everyoneToggle.isOn) && (friendsToggle.isOn))
                            {
                                StaticFunction.setMistakes(StaticFunction.getMistakes() + 1);
                                Debug.Log(script.getSNSName() + " " + itemName + ", " + flagName + ": setting is not set to everyone");
                            }

                            for (int i = 0; i < StaticFunction.getPrivacySettingChoices().Length; i++)
                            {
                                if (StaticFunction.getPrivacySettingChoices()[i].Equals(itemName))
                                {
                                    if (!StaticFunction.getPrivacySettingFlags()[i].Equals(flagName))
                                    {
                                        StaticFunction.setMistakes(StaticFunction.getMistakes() + 1);
                                        Debug.Log(script.getSNSName() + " " + itemName + ", " + flagName + ": wrong privacy setting category");
                                    }
                                    break;
                                }
                            }
                        }
                        else if (itemName == "Password")
                        {
                            int flagIndex = script.getFlagIndex();

                            if (flagIndex <= -1)
                            {
                                StaticFunction.setMistakes(StaticFunction.getMistakes() + 1);
                                Debug.Log(script.getSNSName() + " " + itemName + ", " + flagName + ": flagindex error");
                            }
                            else if (flagName != StaticFunction.getPasswordFlags()[flagIndex])
                            {
                                StaticFunction.setMistakes(StaticFunction.getMistakes() + 1);
                                Debug.Log(script.getSNSName() + " " + itemName + ", " + flagName + ": wrong post flag");
                            }
                        }

                        break;
					}                
				}
			}	
		}
		
        
        GameObject.FindGameObjectWithTag("ErrorTag").GetComponent<TMPro.TextMeshProUGUI>().text = StaticFunction.getMistakes().ToString();

        //reset stage
        StaticFunction.setProfileNum(StaticFunction.getProfileNum() + 1);
        profileNum.GetComponent<TMPro.TextMeshProUGUI>().text = StaticFunction.getProfileNum().ToString();

        //setInstanceCounter
        GameObject temp = GameObject.FindGameObjectWithTag("Clickable");
        FlagSystem tempScript = (FlagSystem) temp.GetComponent(typeof(FlagSystem));
        tempScript.setInstanceCounter(0);

        foreach (GameObject x in GameObject.FindGameObjectsWithTag("Clickable"))
        {
            FlagSystem script = (FlagSystem) x.GetComponent(typeof(FlagSystem));
            script.ResetStage();
        }

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

        //after final profile
        if (StaticFunction.getProfileNum() == StaticFunction.getTotalProfiles())
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
}
