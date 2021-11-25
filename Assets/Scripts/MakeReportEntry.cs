using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeReportEntry : MonoBehaviour
{
    public GameObject reportEntryPrefab;
    private GameObject messageField;
    private GameObject snsField;
    private GameObject reportEntry;

    private static int numOfReports = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void MakeReport()
    {
        messageField = GameObject.FindGameObjectWithTag("MessageField");
        snsField = GameObject.FindGameObjectWithTag("SNSField");

        string[] reportDetails = messageField.GetComponent<TMPro.TextMeshProUGUI>().text.Split(new string[] { " - " }, System.StringSplitOptions.None);

        if (reportDetails[1] != "")
        {            
            reportEntry = Instantiate(
                reportEntryPrefab,
                new Vector3(1752.53125f, 979.4930419921875f - (79f * (float) numOfReports), 0.0f), //need to automotate y value at some point for more than one error                            
                Quaternion.identity,
                GameObject.FindGameObjectWithTag("MessagesBG").transform);

            reportEntry.transform.Find("ItemName").GetComponent<TMPro.TextMeshProUGUI>().text = reportDetails[0];
            reportEntry.transform.Find("FlagName").GetComponent<TMPro.TextMeshProUGUI>().text = reportDetails[1];
            reportEntry.transform.Find("SNSName").GetComponent<TMPro.TextMeshProUGUI>().text = snsField.GetComponent<TMPro.TextMeshProUGUI>().text;

            foreach (GameObject x in GameObject.FindGameObjectsWithTag("EditableMA"))
            {
                Destroy(x);
            }

            foreach (GameObject x in GameObject.FindGameObjectsWithTag("Categories"))
            {
                Destroy(x);
            }
        }
        
        numOfReports++;
    }    
}
