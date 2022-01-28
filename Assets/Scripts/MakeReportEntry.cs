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
            StaticFunction.setNumOfReports(0);
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
            reportEntry.transform.Find("ID").GetComponent<TMPro.TextMeshProUGUI>().text = StaticFunction.getNumOfReports().ToString();
            
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

            if (StaticFunction.getNumOfReports() == 0)
            {                
                StaticFunction.reportEntries.Add(reportEntry);
            }
            else
            {               
                reportEntry.transform.position = new Vector3(
                    StaticFunction.reportEntries[StaticFunction.reportEntries.Count - 1].transform.position.x, 
                    StaticFunction.reportEntries[StaticFunction.reportEntries.Count - 1].transform.position.y - 79f, 
                    StaticFunction.reportEntries[StaticFunction.reportEntries.Count - 1].transform.position.z
                    );
                StaticFunction.reportEntries.Add(reportEntry);
            }

            StaticFunction.setNumOfReports(StaticFunction.getNumOfReports() + 1);

            foreach (GameObject x in GameObject.FindGameObjectsWithTag("EditableMA"))
            {
                Destroy(x);
            }

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

        int id = int.Parse(transform.parent.Find("ID").GetComponent<TMPro.TextMeshProUGUI>().text);

        StaticFunction.reportEntries.RemoveAt(id);
        StaticFunction.setNumOfReports(StaticFunction.getNumOfReports() - 1);

        for (int i = id; i < StaticFunction.reportEntries.Count; i++)
        {
            Transform reportEntry = StaticFunction.reportEntries[i].transform;
            reportEntry.Translate(new Vector3(0f, 79f, 0f));
            Transform idObject = reportEntry.Find("ID");
            idObject.GetComponent<TMPro.TextMeshProUGUI>().text = i.ToString();
        }

        Destroy(transform.parent.gameObject);
    }
}
