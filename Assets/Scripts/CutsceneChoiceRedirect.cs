using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CutsceneChoiceRedirect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{ 
    private GameObject world;
    private Image imageComponent;
    private Color32 defColor = new Color32(255, 255, 255, 255);
    private Color32 highlight = new Color32(255, 255, 0, 125);

    private void Start()
    {
        world = GameObject.FindGameObjectWithTag("AskDialogueScene");

        imageComponent = transform.GetComponent<Image>();            
        
        ////Finds the Skip button
        //GameObject Skip = GameObject.Find("/World (AskDialogue)/ChatWindow/Skip");
        ////If it's still the tutorial, set as inactive so it isn't visible
        ////Else, reenable it
        //if (StaticFunction.tutorialStart)
        //{
        //    Skip.SetActive(false);
        //}
        //else
        //{
        //    Skip.SetActive(true);
        //}
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
            if (transform.name.Contains("Choice"))
            {
                acceptChoice();
            }
            else if (transform.name.Equals("Next"))
            {
                nextScene();
            }
            else //Skip Button
            {
                if (StaticFunction.tutorialStart)
                {
                    CutscenePlayer script = (CutscenePlayer)world.GetComponent(typeof(CutscenePlayer));
                    if (StaticFunction.tutorialPart)
                    {
                        script.nextScene("Tutorial");
                    }
                    else
                    {
                        StaticFunction.tutorialStart = false;
                        script.nextScene("Stage 1");
                    }
                }
            }
        }
    }
}
