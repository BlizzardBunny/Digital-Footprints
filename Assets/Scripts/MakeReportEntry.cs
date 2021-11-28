using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeReportEntry : MonoBehaviour
{
    public GameObject reportEntryPrefab;
    private GameObject messageField;
    private GameObject snsField;
    private GameObject reportEntry;

    private static GameObject prevReportEntry;
    private static int numOfReports = 0;
    private static int currProfile = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void MakeReport()
    {
        if (currProfile != StaticFunction.getProfileNum())
        {
            numOfReports = 0;
            currProfile = StaticFunction.getProfileNum();
        }

        messageField = GameObject.FindGameObjectWithTag("MessageField");
        snsField = GameObject.FindGameObjectWithTag("SNSField");

        string[] reportDetails = messageField.GetComponent<TMPro.TextMeshProUGUI>().text.Split(new string[] { " - " }, System.StringSplitOptions.None);

        if (reportDetails[1] != "")
        {
            reportEntry = Instantiate(
                reportEntryPrefab,
                new Vector3(1752.53125f, 979.4930419921875f, 0.0f),                             
                Quaternion.identity,
                GameObject.FindGameObjectWithTag("MessagesBG").transform);

            reportEntry.transform.Find("ItemName").GetComponent<TMPro.TextMeshProUGUI>().text = reportDetails[0];
            reportEntry.transform.Find("FlagName").GetComponent<TMPro.TextMeshProUGUI>().text = reportDetails[1];
            reportEntry.transform.Find("SNSName").GetComponent<TMPro.TextMeshProUGUI>().text = snsField.GetComponent<TMPro.TextMeshProUGUI>().text;

            if (StaticFunction.getTotalErrors() > 0)
            {              
                if (numOfReports == 0)
                {
                    prevReportEntry = reportEntry;
                }
                else
                {
                    reportEntry.transform.position = new Vector3(prevReportEntry.transform.position.x, prevReportEntry.transform.position.y - 79f, prevReportEntry.transform.position.z);
                    prevReportEntry = reportEntry;
                }

                numOfReports++;
            }

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
}
