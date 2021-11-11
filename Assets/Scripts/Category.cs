using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Category : MonoBehaviour
{
    private string buttonText;

    private void Start()
    {
        buttonText = transform.Find("TMP").GetComponent<TMPro.TextMeshProUGUI>().text;
    }

    public void Categorize()
    {
        try
        {
            Transform messageField = GameObject.FindGameObjectWithTag("MessageField").transform;
            string text = messageField.GetComponent<TMPro.TextMeshProUGUI>().text;

            messageField.GetComponent<TMPro.TextMeshProUGUI>().text = text + " - " + buttonText;

            //if (text == "Address")
            //{
            //    messageField.GetComponent<TMPro.TextMeshProUGUI>().text = text + " - Location Data";
            //    if (buttonText == "Location Data")
            //    {
                    
            //    }
            //}
            //else
            //{
            //    messageField.GetComponent<TMPro.TextMeshProUGUI>().text = text + " - " + StaticFunction.getCaptionFlags()[StaticFunction.getCurrFlag()];
            //    if (buttonText == StaticFunction.getCaptionFlags()[StaticFunction.getCurrFlag()])
            //    {
                    
            //    }
            //}
        }
        catch
        {

        }
    }
}
