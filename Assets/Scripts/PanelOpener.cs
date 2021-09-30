using System.Collections;
using UnityEngine;


public class PanelOpener : MonoBehaviour
{
    private bool panelIsShowing = true;
    
    public void TogglePanel(GameObject panel)
    {
        StartCoroutine(MovePanel(panel));
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
            //moves the player sprite to the targetPos over a set time
            panel.transform.position = Vector3.Lerp(originalPosition, targetPosition, (elapsedTime / timeToMove));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        panel.transform.position = targetPosition;
        panelIsShowing =  !panelIsShowing;
    }
}
