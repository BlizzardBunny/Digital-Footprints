using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PanelScript : MonoBehaviour
{
    [SerializeField] GameObject categoryPanelPrefab;
    public Scrollbar scrollbar;
    public Button otherNavButton;
    private bool panelIsShowing = true;
    private float scrollVal = 0.33f;

    private static int pageNumber = 1;
    private static string currSocialMedia;
    private static GameObject[] socialMediaPages;

    public void Start()
    {
        GameObject content = GameObject.FindGameObjectWithTag("CompanyStandardsContent");
        content.transform.position = new Vector3(1580.0f, 322.0400085449219f, 0.0f);
        socialMediaPages = GameObject.FindGameObjectsWithTag("SocialMediaPage");
        currSocialMedia = "*";
    }

    public void TogglePanel(GameObject panel)
    {
        StartCoroutine(MovePanel(panel));
    }

    public void minimizePanel(GameObject panel)
    {
        Animator animator = panel.GetComponent<Animator>();
        if (animator != null)
        {
            if ((currSocialMedia != transform.name) && ((transform.name == "Digibook") || (transform.name == "Photogram") || (transform.name == "Chirper") || (transform.name == "*")))
            {
                currSocialMedia = transform.name;
                
                string panelName = transform.name + " (Panel)";
                foreach (GameObject socialMediaPage in socialMediaPages)
                {                
                    if (socialMediaPage.transform.name == panelName)
                    {
                        socialMediaPage.transform.parent.Find("Title").Find("Title TMP").GetComponent<TMPro.TextMeshProUGUI>().text = transform.name;
                        socialMediaPage.GetComponent<RectTransform>().SetAsLastSibling();
                        break;
                    }
                }

                bool panelIsMinimized = animator.GetBool("isMinimized");
                if (panelIsMinimized)
                {
                    animator.SetBool("isMinimized", !panelIsMinimized);
                }
            }
            else
            {
                bool panelIsMinimized = animator.GetBool("isMinimized");
                animator.SetBool("isMinimized", !panelIsMinimized);
            }
        }
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
            GetComponent<Button>().interactable = false;
            StartCoroutine(scrollLeft(scrollbar));
            GetComponent<Button>().interactable = true;
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

    public void JumpPage(int pageNum)
    {
        pageNumber = pageNum;
        float targetPosition = 0;
        switch (pageNum - 1)
        {
            case 1:
                targetPosition += scrollVal - 0.005f;
                break;
            case 2:
                targetPosition += (scrollVal * 2) - 0.012f;
                break;
            case 3:
                targetPosition += (scrollVal * 3) - 0.02f;
                break;
        }
        scrollbar.value = targetPosition;

        GameObject leftArrow = GameObject.FindGameObjectWithTag("LeftArrow");
        GameObject rightArrow = GameObject.FindGameObjectWithTag("RightArrow");
        leftArrow.GetComponent<Button>().interactable = true;
        if (pageNum >= 4)
        {
            rightArrow.GetComponent<Button>().interactable = false;
        }
    }

    public void Categorize(Button clicked)
    {
        Instantiate(
            categoryPanelPrefab,
            new Vector3(1749.999755859375f, 287.0400085449219f, 0.0f),
            clicked.transform.rotation,
            GameObject.FindGameObjectWithTag("CompanyStandards").transform
            );
    }

    public void CancelCategorize()
    {
       foreach (GameObject x in GameObject.FindGameObjectsWithTag("Categories"))
       {
            Destroy(x);
       }
    }

    public void ToggleFunction(int pageNum)
    {
        if (StaticFunction.getIsChecking())
        {
            Categorize(GetComponent<Button>());
        }
        else
        {
            JumpPage(pageNum);
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
        GetComponent<Button>().interactable = false;
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
        GetComponent<Button>().interactable = true;
    }

    private IEnumerator MovePanel(GameObject panel)
    {
        GetComponent<Button>().interactable = false;
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
        GetComponent<Button>().interactable = true;
    }
}
