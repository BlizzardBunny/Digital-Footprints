using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemHighlighter : MonoBehaviour
{
    private bool isToggled = false;
    private Color toggleColor = new Color32(245, 245, 0, 100);
    private Color normalColor = new Color32(0, 0, 0, 0);        
    private Button thingToHighlight;
    public void ChangeButtonColor(Button button)
    {
        string thingWeWantToFind = button.transform.parent.Find("ItemName").GetComponent <TMPro.TextMeshProUGUI>().text;

        foreach (GameObject x in GameObject.FindGameObjectsWithTag("Clickable"))
        {
            if (x.transform.parent.name == thingWeWantToFind)
            {
                thingToHighlight = x.GetComponent<Button>();
                break;
            }
        }
        if (isToggled == false)
        {
            ColorBlock cb = thingToHighlight.colors;
            cb.normalColor = toggleColor;
            cb.highlightedColor = toggleColor;
            thingToHighlight.colors = cb;
            isToggled = !isToggled;
        }
        else
        {
            ColorBlock cb = thingToHighlight.colors;
            cb.normalColor = normalColor;
            cb.highlightedColor = toggleColor;
            thingToHighlight.colors = cb;
            isToggled = !isToggled;
        }            
    }
}
