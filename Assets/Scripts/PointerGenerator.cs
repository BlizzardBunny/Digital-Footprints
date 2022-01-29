using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PointerGenerator : MonoBehaviour
{
    public Transform overlay;
    public GameObject pointerPrefab;
    public Button submitButton;

    private GameObject pointer;
    private Transform[] points;
    private float characterTypingSpeed = 0.03f;
    private bool activateSubmit = false;

    //for checking correctness
    private int flagIndex;

    public enum Directions
    {
        Up, Down, Left, Right, Null
    }

    public class PointerInfo
    {
        public Directions direction { get; }
        public string details { get; }
        public bool allowPlayerControl { get; }

        public PointerInfo(Directions x, string y) => (direction, details, allowPlayerControl) = (x, y, false);
        public PointerInfo(Directions x, string y, bool z) => (direction, details, allowPlayerControl) = (x, y, z);
    }

    private PointerInfo[] tutorial = new PointerInfo[]
    {
        new PointerInfo(Directions.Null, "This is my desktop."),
        new PointerInfo(Directions.Right, "I can't believe I actually downloaded this weird Report App for this."),
        new PointerInfo(Directions.Down, "Well, I better open up Digibook and see what $r did wrong this time."),
        new PointerInfo(Directions.Null, "(Hint: Look for ways $r could have leaked her data online.)", true)
    };

    private void Start()
    {
        points = getChildren(transform.Find("Path 0"));
    }

    public Transform[] getChildren(Transform parent)
    {
        Transform[] ret = new Transform[parent.childCount];

        for (int i = 0; i < ret.Length; i++)
        {
            ret[i] = parent.Find("Point " + i);
        }

        return ret;
    }

    public void tutorialStart()
    {
        StartCoroutine(run(tutorial, points));
    }

    IEnumerator run(PointerInfo[] pointerInfos, Transform[] points)
    {
        transform.SetAsLastSibling();
        for (int i = 0; i < pointerInfos.Length; i++)
        {
            pointer = Instantiate(
                pointerPrefab,
                points[i].position,
                Quaternion.identity,
                transform);

            for (int j = 0; j < pointer.transform.childCount; j++)
            {
                Transform child = pointer.transform.GetChild(j);
                if (child.name.Equals("BG") || child.name.Equals("Details"))
                {
                    //do nothing
                }
                else
                {
                    if (child.name.Equals(pointerInfos[i].direction.ToString()))
                    {
                        //do nothing
                    }
                    else
                    {
                        Destroy(child.gameObject);
                    }
                }
            }

            Transform textbox = pointer.transform.Find("Details");
            textbox.GetComponent<TMPro.TextMeshProUGUI>().text = "";

            string details = StaticFunction.updateStrings(pointerInfos[i].details);

            for (int j = 0; j < details.Length; j++)
            {
                yield return new WaitForSeconds(characterTypingSpeed);
                textbox.GetComponent<TMPro.TextMeshProUGUI>().text += details[j];

                if (Input.GetMouseButton(0))
                {
                    textbox.GetComponent<TMPro.TextMeshProUGUI>().text += details.Substring(j + 1);
                    yield return new WaitForSeconds(0.5f);
                    break;
                }
            }

            yield return new WaitUntil(() => Input.GetMouseButtonUp(0));

            Destroy(pointer);

            if (pointerInfos[i].allowPlayerControl)
            {
                transform.SetAsFirstSibling();
            }
        }

        yield break;
    }

    public void wrongItem(Transform clickable)
    {
        toggleSubmitButton();

        Transform[] pathPoints = getChildren(transform.Find("Mistake Path 1"));

        pathPoints[1].position = new Vector3(pathPoints[1].position.x, clickable.position.y, pathPoints[1].position.z);

        PointerInfo[] mistakeMessages = new PointerInfo[]
        {
            new PointerInfo(Directions.Null, "Nah. I'm pretty sure there's nothing wrong with this one."),
            new PointerInfo(Directions.Left, "I better click it again to delete it from the Report App.", true)
        };

        StartCoroutine(run(mistakeMessages, pathPoints));
    }

    public void correctItem(Transform clickable, int flag)
    {
        toggleSubmitButton();

        flagIndex = flag;     

        Transform[] pathPoints = getChildren(transform.Find("Path 1"));

        PointerInfo[] goodMessages = new PointerInfo[]
        {
            new PointerInfo(Directions.Null, "Yep. This one is definitely suspicious."),
            new PointerInfo(Directions.Null, "But the question is, in what way?"),
            new PointerInfo(Directions.Right, "Maybe this Company Standards $r gave me will help me out?", true)
        };

        StartCoroutine(run(goodMessages, pathPoints));
    }

    public void checkCategory(Transform category, string flagName)
    {
        toggleSubmitButton();

        if (flagIndex < 0)
        {
            if (flagName.Equals("Personal Information"))
            {
                correctCategory();
            }
            else
            {
                wrongCategory(category);
            }
        }
        else
        {
            if (flagName == StaticFunction.getCaptionFlags()[flagIndex])
            {
                correctCategory();
            }
            else
            {
                wrongCategory(category);
            }
        }
    }

    public void wrongCategory(Transform category)
    {
        Transform[] pathPoints = getChildren(transform.Find("Mistake Path 2"));

        pathPoints[2].position = new Vector3(pathPoints[2].position.x, category.position.y, pathPoints[2].position.z);

        PointerInfo[] mistakeMessages = new PointerInfo[]
        {
            new PointerInfo(Directions.Null, "No. I don't think that's right."),
            new PointerInfo(Directions.Null, "I should definitely read the Company Standards closer."),
            new PointerInfo(Directions.Right, "Luckily, I can just undo by clicking this again.", true)
        };

        StartCoroutine(run(mistakeMessages, pathPoints));
    }

    public void correctCategory()
    {
        Transform[] pathPoints = getChildren(transform.Find("Path 2"));

        PointerInfo[] goodMessages = new PointerInfo[]
        {
            new PointerInfo(Directions.Null, "That seems right."),
            new PointerInfo(Directions.Right, "I guess I should click \"Send\" to lock it in?", true)
        };

        StartCoroutine(run(goodMessages, pathPoints));
    }

    public void doubleCheck(string flagName)
    {
        toggleSubmitButton();

        if (GameObject.FindGameObjectsWithTag("ReportEntry").Length > 0)
        {
            wrongReport(true);
        }
        else if (flagIndex < 0)
        {
            if (flagName.Equals("Personal Information"))
            {
                correctReport();
            }
            else
            {
                wrongReport(false);
            }
        }
        else
        {
            if (flagName == StaticFunction.getCaptionFlags()[flagIndex])
            {
                correctReport();
            }
            else
            {
                wrongReport(false);
            }
        }
    }

    public void wrongReport(bool hasTooManyReports)
    {
        Transform[] pathPoints = getChildren(transform.Find("Mistake Path 3"));

        PointerInfo[] mistakeMessages;
        
        if (hasTooManyReports)
        {
            mistakeMessages = new PointerInfo[]
            {
                new PointerInfo(Directions.Null, "No. I think I made too many reports."),
                new PointerInfo(Directions.Right, "I should click the \"X\" on everything and try this again from the top.", true)
            };
        }
        else
        {
            mistakeMessages = new PointerInfo[]
            {
                new PointerInfo(Directions.Null, "No. I don't think that's right."),
                new PointerInfo(Directions.Right, "I should click the \"X\" and try this again from the top.", true)
            };
        }

        StartCoroutine(run(mistakeMessages, pathPoints));
    }

    public void correctReport()
    {
        submitButton.interactable = true;
        activateSubmit = true;

        Transform[] pathPoints = getChildren(transform.Find("Path 3"));

        PointerInfo[] goodMessages = new PointerInfo[]
        {
            new PointerInfo(Directions.Null, "Alright. Eveything seems to be in order."),
            new PointerInfo(Directions.Right, "I should click \"Submit\" to send this to $r.", true)
        };

        StartCoroutine(run(goodMessages, pathPoints));
    }

    public void toggleSubmitButton()
    {
        if (activateSubmit)
        {
            submitButton.interactable = false;
            activateSubmit = false;
        }
    }

    public void endTutorial()
    {
        StartCoroutine(end());
    }

    IEnumerator end()
    {
        overlay.Find("Thoughts").GetComponent<TMPro.TextMeshProUGUI>().text = "";
        overlay.SetAsLastSibling();

        Animator anim = overlay.Find("Blackscreen").GetComponent<Animator>();
        anim.SetBool("isFadingIn", false);

        yield return new WaitForSeconds(1f);
        StaticFunction.setCurrentLevel("AskDialogue");
        SceneManager.LoadScene("AskDialogue");
    }
}
