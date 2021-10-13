using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Submit : MonoBehaviour
{
    public Transform profileNum;
    public Transform totalProfiles;

    // Start is called before the first frame update
    void Start()
    {
        totalProfiles.GetComponent<TMPro.TextMeshProUGUI>().text = "/" + StaticFunction.getTotalProfiles().ToString();
        profileNum.GetComponent<TMPro.TextMeshProUGUI>().text = StaticFunction.getProfileNum().ToString();
    }

    public void SubmitReport()
    {
        Debug.Log("Submit was pressed. " + StaticFunction.getErrorNum());
        if (StaticFunction.getErrorNum() == 0)
        {
            StaticFunction.setProfileNum(StaticFunction.getProfileNum() + 1);
            profileNum.GetComponent<TMPro.TextMeshProUGUI>().text = StaticFunction.getProfileNum().ToString();
            foreach (GameObject x in GameObject.FindGameObjectsWithTag("Clickable"))
            {
                FlagSystem script = (FlagSystem) x.GetComponent(typeof(FlagSystem));
                script.Reset();
            }
        }
    }
}
