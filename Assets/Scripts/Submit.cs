using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Submit : MonoBehaviour
{
    
    public GameObject confirmationPrefab;

    private GameObject confirmation;
    private Transform profileNum;
    private Transform totalProfiles;

    // Start is called before the first frame update
    void Start()
    {
        profileNum = GameObject.FindGameObjectWithTag("Count").transform;
        totalProfiles = GameObject.FindGameObjectWithTag("Total").transform;
        totalProfiles.GetComponent<TMPro.TextMeshProUGUI>().text = "/" + StaticFunction.getTotalProfiles().ToString();
        profileNum.GetComponent<TMPro.TextMeshProUGUI>().text = StaticFunction.getProfileNum().ToString();
    }

    public void Confirm()
    {
        confirmation = Instantiate(
                confirmationPrefab,
                new Vector3(962.2625122070313f,540.0f,0.0f),
                Quaternion.identity,
                GameObject.FindGameObjectWithTag("World").transform);
        GameObject.FindGameObjectWithTag("Submit").GetComponent<Button>().interactable = false;
    }

    public void Cancel()
    {
        foreach (GameObject x in GameObject.FindGameObjectsWithTag("Confirmation"))
        {
            Destroy(x);
        }

        GameObject.FindGameObjectWithTag("Submit").GetComponent<Button>().interactable = true;
    }

    public void SubmitReport()
    {
        if (StaticFunction.getErrorNum() == 0)
        {
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
        }

        if (StaticFunction.getProfileNum() == StaticFunction.getTotalProfiles())
        {

        }
    }
}
