using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MakeReportEntry : MonoBehaviour
{
    public GameObject reportEntryPrefab;
    public Sprite[] snsLogos;

    private GameObject reportEntry;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void Update()
    {
        
    }

    public void MakeReport()
    {
        if (StaticFunction.getCurrentProfile() != StaticFunction.getProfileNum())
        {
            StaticFunction.setCurrentProfile(StaticFunction.getProfileNum());
        }

        Transform item = GameObject.FindGameObjectWithTag("MessageField").transform.Find("Item");
        string itemText = item.GetComponent<TMPro.TextMeshProUGUI>().text;
        Transform flag = GameObject.FindGameObjectWithTag("MessageField").transform.Find("Flag");
        string flagText = flag.GetComponent<TMPro.TextMeshProUGUI>().text;
        GameObject snsField = GameObject.FindGameObjectWithTag("SNSField");
        string sns = snsField.GetComponent<TMPro.TextMeshProUGUI>().text;

        if (StaticFunction.tutorialStart)
        {
            PointerGenerator script = (PointerGenerator)(GameObject.FindGameObjectWithTag("PointersPanel")).GetComponent(typeof(PointerGenerator));
            StaticFunction.tutorialCanSubmit = true;
            script.doubleCheck(flagText);
        }

        if (flagText.Equals("") || itemText.Equals(""))
        {
            //do nothing
        }
        else
        {
            reportEntry = Instantiate(
                reportEntryPrefab,
                GameObject.FindGameObjectWithTag("MessagesBG").transform.position,                             
                Quaternion.identity,
                GameObject.FindGameObjectWithTag("MessagesBG").transform);

            reportEntry.transform.Find("ItemName").GetComponent<TMPro.TextMeshProUGUI>().text = itemText;
            reportEntry.transform.Find("FlagName").GetComponent<TMPro.TextMeshProUGUI>().text = flagText;
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
            StaticFunction.tutorialCanSubmit = false;
            script.toggleSubmitButton();
        }

        Destroy(transform.parent.gameObject);
    }

    public void ClearMessageField()
    {
        Transform item = GameObject.FindGameObjectWithTag("MessageField").transform.Find("Item");
        Transform flag = GameObject.FindGameObjectWithTag("MessageField").transform.Find("Flag");
        item.GetComponent<TMPro.TextMeshProUGUI>().text = "";
        flag.GetComponent<TMPro.TextMeshProUGUI>().text = "";

        foreach (GameObject x in GameObject.FindGameObjectsWithTag("Categories"))
        {
            Destroy(x);
        }
    }
}
