using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BetterButtons : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public GameObject askButtonPrefab;

    private Image imageComponent;
    private Color32 defColor = new Color32(255, 255, 255, 0);
    private Color32 highlight = new Color32(255, 255, 0, 125);

    private Transform world;
    private GameObject askButton;

    private string parentName;

    private void Start()
    {
        world = GameObject.FindGameObjectWithTag("World").transform;
        imageComponent = transform.GetComponent<Image>();
        parentName = transform.parent.name;
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
        Flag script = (Flag)transform.GetComponent(typeof(Flag));

        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (StaticFunction.tutorialStart)
            {
                if (!StaticFunction.tutorialCanSubmit)
                {
                    script.FlagItem();
                    StaticFunction.tutorialCanSubmit = true;
                }
            }
            else
            {
                script.FlagItem();
            }
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (GameObject.FindGameObjectsWithTag("AskButton").Length > 0)
            {
                foreach (GameObject x in GameObject.FindGameObjectsWithTag("AskButton"))
                {
                    Destroy(x);
                }
            }

            if (!StaticFunction.tutorialStart && (parentName.Equals("Address")))
            {
                askButton = Instantiate(
                askButtonPrefab,
                Input.mousePosition,
                Quaternion.identity,
                world);

                try
                {
                    if (transform.parent.parent.name.Equals("PrivacyWindow"))
                    {
                        StaticFunction.parentName = "PrivacyWindow";
                    }
                    else
                    {
                        StaticFunction.parentName = parentName;
                    }
                }
                catch
                {
                    StaticFunction.parentName = parentName;
                }

                StaticFunction.flagIndex = script.flagIndex;
                StaticFunction.isFlag = script.flaggedItem;
            }
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if ((askButton != null))
            {
                Destroy(askButton);
            }
        }
    }
}
