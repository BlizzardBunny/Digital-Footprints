using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MakeReportEntry : MonoBehaviour
{
    public GameObject reportEntryPrefab;
    public Sprite[] snsLogos;

    private GameObject messageField;
    private GameObject snsField;
    private GameObject reportEntry;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void MakeReport()
    {
        if (StaticFunction.getCurrentProfile() != StaticFunction.getProfileNum())
        {
            StaticFunction.setCurrentProfile(StaticFunction.getProfileNum());
        }

        messageField = GameObject.FindGameObjectWithTag("MessageField");
        snsField = GameObject.FindGameObjectWithTag("SNSField");
        string sns = snsField.GetComponent<TMPro.TextMeshProUGUI>().text;

        string[] reportDetails = messageField.GetComponent<TMPro.TextMeshProUGUI>().text.Split(new string[] { " - " }, System.StringSplitOptions.None);

        if (StaticFunction.tutorialStart)
        {
            PointerGenerator script = (PointerGenerator)(GameObject.FindGameObjectWithTag("PointersPanel")).GetComponent(typeof(PointerGenerator));

            script.doubleCheck(reportDetails[1]);
        }

        if (reportDetails.Length < 2)
        {
            //do nothing
        }
        else
        {
            reportEntry = Instantiate(
                reportEntryPrefab,
                new Vector3(1752.53125f, 979.4930419921875f, 0.0f),                             
                Quaternion.identity,
                GameObject.FindGameObjectWithTag("MessagesBG").transform);

            reportEntry.transform.Find("ItemName").GetComponent<TMPro.TextMeshProUGUI>().text = reportDetails[0];
            reportEntry.transform.Find("FlagName").GetComponent<TMPro.TextMeshProUGUI>().text = reportDetails[1];
            reportEntry.transform.Find("SNSName").GetComponent<TMPro.TextMeshProUGUI>().text = sns;
            
            if (sns.Contains("Chirper"))
            {
                reportEntry.transform.Find("SNSLogo").GetComponent<Image>().sprite = snsLogos[0];
            }
            else if (sns.Contains("Digibook"))
            {
                reportEntry.transform.Find("SNSLogo").GetComponent<Image>().sprite = snsLogos[1];
            }
            else if (sns.Contains("Photogram"))
            {
                reportEntry.transform.Find("SNSLogo").GetComponent<Image>().sprite = snsLogos[2];
            }

            foreach (GameObject x in GameObject.FindGameObjectsWithTag("EditableMA"))
            {
                Destroy(x);
            }

            StaticFunction.editableIsDrawn = false;

            foreach (GameObject x in GameObject.FindGameObjectsWithTag("Categories"))
            {
                Destroy(x);
            }
        }
    } 
    
    public void DeleteReport()
    {
        if (StaticFunction.tutorialStart)
        {
            PointerGenerator script = (PointerGenerator)(GameObject.FindGameObjectWithTag("PointersPanel")).GetComponent(typeof(PointerGenerator));

            script.toggleSubmitButton();
        }

        Destroy(transform.parent.gameObject);
    }
}
