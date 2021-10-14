using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeReportEntry : MonoBehaviour
{
    public GameObject reportEntryPrefab;
    private GameObject messageField;
    private GameObject reportEntry;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void MakeReport()
    {
        messageField = GameObject.FindGameObjectWithTag("MessageField");

        string[] reportDetails = messageField.GetComponent<TMPro.TextMeshProUGUI>().text.Split(new string[] { " - " }, System.StringSplitOptions.None);

        if (reportDetails[1] != "")
        {
            reportEntry = Instantiate(
                reportEntryPrefab,
                new Vector3(1725.1199951171875f, 951.2784423828125f, 0.0f), //need to automotate y value at some point for more than one error
                Quaternion.identity,
                GameObject.FindGameObjectWithTag("MessagesBG").transform);

            reportEntry.transform.Find("ItemName").GetComponent<TMPro.TextMeshProUGUI>().text = reportDetails[0];
            reportEntry.transform.Find("FlagName").GetComponent<TMPro.TextMeshProUGUI>().text = reportDetails[1];

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