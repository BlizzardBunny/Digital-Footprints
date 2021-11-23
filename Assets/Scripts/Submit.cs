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
			StaticFunction.setMistakes(StaticFunction.getMistakes() + 1);
		}
		else
		{
			foreach(GameObject reportEntry in GameObject.FindGameObjectsWithTag("ReportEntry"))
			{
				string itemName = reportEntry.transform.Find("ItemName").GetComponent<TMPro.TextMeshProUGUI>().text;
				string flagName = reportEntry.transform.Find("FlagName").GetComponent<TMPro.TextMeshProUGUI>().text;

				foreach(GameObject clickable in GameObject.FindGameObjectsWithTag("Clickable"))
				{
					FlagSystem script = (FlagSystem) clickable.GetComponent(typeof(FlagSystem));

					if (clickable.transform.parent.name == itemName)
					{
						if (!script.getIsFlag())
						{
							StaticFunction.setMistakes(StaticFunction.getMistakes() + 1);
							break;
						}

                        if ((itemName == "Address") && (flagName != "Personal Information"))
                        {
                            StaticFunction.setMistakes(StaticFunction.getMistakes() + 1);
                        }
                        else if (itemName.StartsWith("Post"))
                        {
                            int flagIndex = script.getFlagIndex();

                            if (flagIndex <= -1)
                            {
                                StaticFunction.setMistakes(StaticFunction.getMistakes() + 1);
                            }
                            else if (flagName != StaticFunction.getCaptionFlags()[flagIndex])
                            {
                                StaticFunction.setMistakes(StaticFunction.getMistakes() + 1);
                            }
                        }
                        else if (clickable.transform.parent.parent.name == "PrivacyWindow")
                        {
                            Toggle everyoneToggle = clickable.transform.parent.Find("Everyone").GetComponent<Toggle>();
                            Toggle friendsToggle = clickable.transform.parent.Find("Friends").GetComponent<Toggle>();

                            if ((!everyoneToggle.isOn) && (friendsToggle.isOn))
                            {
                                StaticFunction.setMistakes(StaticFunction.getMistakes() + 1);
                            }

                            for (int i = 0; i < StaticFunction.getPrivacySettingChoices().Length; i++)
                            {
                                Debug.Log(itemName + "  " + flagName);
                                if (StaticFunction.getPrivacySettingChoices()[i].Equals(itemName))
                                {
                                    Debug.Log(itemName + " : " + flagName);
                                    Debug.Log(StaticFunction.getPrivacySettingChoices()[i] + " : " + StaticFunction.getPrivacySettingFlags()[i]);
                                    if (!StaticFunction.getPrivacySettingFlags()[i].Equals(flagName))
                                    {
                                        StaticFunction.setMistakes(StaticFunction.getMistakes() + 1);
                                    }
                                    break;
                                }
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

        foreach (GameObject x in GameObject.FindGameObjectsWithTag("Clickable"))
        {
            FlagSystem script = (FlagSystem) x.GetComponent(typeof(FlagSystem));
            script.Reset();
        }

        foreach (GameObject x in GameObject.FindGameObjectsWithTag("Confirmation"))
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
