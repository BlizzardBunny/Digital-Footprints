using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Category : MonoBehaviour
{
    [SerializeField] string categoryDetails;
    private string buttonText;
    private Transform cdPanel, details, title;
    private bool isShowingDetails;

    private void Start()
    {
        buttonText = transform.Find("TMP").GetComponent<TMPro.TextMeshProUGUI>().text;
        cdPanel = transform.parent.Find("CategoryDetailsPanel");
        details = cdPanel.Find("Details");
        title = cdPanel.Find("Title");
        cdPanel.gameObject.SetActive(false);
        isShowingDetails = false;
    }

    public void Categorize()
    {
        if (!isShowingDetails)
        {
            cdPanel.gameObject.SetActive(true);
            details.GetComponent<TMPro.TextMeshProUGUI>().text = categoryDetails;
            title.GetComponent<TMPro.TextMeshProUGUI>().text = buttonText;
            isShowingDetails = true;
        }
        else
        {
            cdPanel.gameObject.SetActive(false);
            isShowingDetails = false;
        }

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

                if (StaticFunction.tutorialStart)
                {
                    PointerGenerator script = (PointerGenerator)(GameObject.FindGameObjectWithTag("PointersPanel")).GetComponent(typeof(PointerGenerator));

                    script.checkCategory(transform, buttonText);
                }
            }
        }
        catch
        {

        }
    }
}
