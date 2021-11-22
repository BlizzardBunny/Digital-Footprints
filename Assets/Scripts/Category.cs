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

            if (text.Contains("-"))
            {
                string[] reportDetails = text.Split(new string[] { " - " }, System.StringSplitOptions.None);
                messageField.GetComponent<TMPro.TextMeshProUGUI>().text = reportDetails[0];
            }
            else
            {
                messageField.GetComponent<TMPro.TextMeshProUGUI>().text = text + " - " + buttonText;
            }
        }
        catch
        {

        }
    }
}
