using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CutsceneChoiceRedirect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{ 
    private GameObject world;
    private Image imageComponent;
    private Color32 defColor = new Color32(255, 255, 255, 255);
    private Color32 highlight = new Color32(255, 255, 0, 125);

    private void Start()
    {
        foreach (GameObject x in GameObject.FindGameObjectsWithTag("World"))
        {
            if (x.transform.name.Contains("AskDialogue"))
            {
                world = x;
                break;
            }
        }

        imageComponent = transform.GetComponent<Image>();
    }

    public void acceptChoice()
    {
        CutscenePlayer script = (CutscenePlayer)world.GetComponent(typeof(CutscenePlayer));

        int choiceIndex = int.Parse(this.name.Replace("Choice ", ""));

        StaticFunction.choiceIndex = choiceIndex - 1;

        script.acceptChoice();
    }

    public void nextScene()
    {
        CutscenePlayer script = (CutscenePlayer)world.GetComponent(typeof(CutscenePlayer));

        script.nextScene();
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
            Debug.Log(StaticFunction.reloadSameStage);

            if (transform.name.Contains("Choice"))
            {
                acceptChoice();
            }
            else
            {
                nextScene();
            }
        }
    }
}
