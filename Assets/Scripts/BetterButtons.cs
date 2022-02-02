using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BetterButtons : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private Image imageComponent;
    private Color32 defColor = new Color32(255, 255, 255, 0);
    private Color32 highlight = new Color32(255, 255, 0, 125);

    private void Start()
    {
        imageComponent = transform.GetComponent<Image>();
    }    

    public void OnPointerEnter(PointerEventData eventData)
    {
        imageComponent.color = highlight;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        imageComponent.color = defColor;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            FlagSystem script = (FlagSystem)transform.GetComponent(typeof(FlagSystem));

            script.Flag();
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {

        }
    }
}
