using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickToClose : MonoBehaviour, IPointerClickHandler
{
    private PausedMenuFunctions script;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.pointerEnter.name.Contains(this.name))
        {
            script.UnPause();
        }
    }

    public void triggerUnPause()
    {
        script.UnPause();
    }

    // Start is called before the first frame update
    void Start()
    {
        GameObject exitButton = GameObject.FindGameObjectWithTag("Exit");
        script = (PausedMenuFunctions)exitButton.GetComponent(typeof(PausedMenuFunctions));
    }
}
