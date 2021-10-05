using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PanelScript : MonoBehaviour
{    
    public Scrollbar scrollbar;
    public Button otherNavButton;
    private bool panelIsShowing = true;
    private float scrollVal = 0.33f;

    private static int pageNumber = 1;

    public void TogglePanel(GameObject panel)
    {
        StartCoroutine(MovePanel(panel));
    }

    public void scrollPanelRight()
    {
        if (pageNumber != 5)
        {
            pageNumber++;
        }
        
        Debug.Log(pageNumber);      

        if (pageNumber < 5)
        {
            StartCoroutine(scrollRight(scrollbar));
            GetComponent<Button>().interactable = scrollbar.value != 0.99f;
            otherNavButton.interactable = true;
        }
        
        if (pageNumber >= 4)
        {
            GetComponent<Button>().interactable = false;
            pageNumber = 4;
        }
        else if (GetComponent<Button>().interactable == false)
        {
            GetComponent<Button>().interactable = true;
        }        
    }
    public void scrollPanelLeft()
    {
        if (pageNumber != 0)
        {
            pageNumber--;
        }

        Debug.Log(pageNumber);        

        if (pageNumber > 0)
        {
            StartCoroutine(scrollLeft(scrollbar));
            GetComponent<Button>().interactable = scrollbar.value != 0;
            otherNavButton.interactable = true;
        }     
        
        if (pageNumber <= 1)
        {
            GetComponent<Button>().interactable = false;
            pageNumber = 1;
        }
        else if (GetComponent<Button>().interactable == false)
        {
            GetComponent<Button>().interactable = true;
        }  
    }

    private IEnumerator scrollLeft(Scrollbar scrollbar)
    {
        float elapsedTime = 0;
        float timeToMove = 0.66f;
        float originalPosition = scrollbar.value;
        float targetPosition = scrollbar.value - scrollVal;
        Debug.Log("scroll panel");
        while (elapsedTime < timeToMove)
        {
            scrollbar.value = Mathf.Lerp(originalPosition, targetPosition, (elapsedTime / timeToMove));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
    private IEnumerator scrollRight(Scrollbar scrollbar)
    {
        float elapsedTime = 0;
        float timeToMove = 0.66f;
        float originalPosition = scrollbar.value;
        float targetPosition = scrollbar.value + scrollVal;
        Debug.Log("scroll panel");
        while (elapsedTime < timeToMove)
        {
            scrollbar.value = Mathf.Lerp(originalPosition, targetPosition, (elapsedTime / timeToMove));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    private IEnumerator MovePanel(GameObject panel)
    {
        float elapsedTime = 0;
        float timeToMove = 0.75f;
        Vector3 originalPosition = panel.transform.position;
        Vector3 targetPosition = panel.transform.position;
        if (panelIsShowing == true)
        {
            targetPosition.x = targetPosition.x + 340;
        }
        else
        {
            targetPosition.x = targetPosition.x - 340;
        }
        while (elapsedTime < timeToMove)
        {
            panel.transform.position = Vector3.Lerp(originalPosition, targetPosition, (elapsedTime / timeToMove));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        panel.transform.position = targetPosition;
        panelIsShowing =  !panelIsShowing;
    }
}
