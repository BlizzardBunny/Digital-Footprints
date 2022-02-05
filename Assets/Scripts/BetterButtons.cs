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
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            FlagSystem script = (FlagSystem)transform.GetComponent(typeof(FlagSystem));

            script.Flag();
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            askButton = Instantiate(
                askButtonPrefab,
                Input.mousePosition,
                Quaternion.identity,
                world);

            FlagSystem script = (FlagSystem)transform.GetComponent(typeof(FlagSystem));

            StaticFunction.parentName = parentName;
            StaticFunction.flagIndex = script.getFlagIndex();
            StaticFunction.rnd2 = script.getRnd2();
            StaticFunction.isFlag = script.isFlag();
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
