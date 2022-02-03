using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialPlayer : MonoBehaviour
{
    public Transform overlay;
    public GameObject dialoguePrefab;

    private GameObject world;
    private GameObject pointersPanel;
    private GameObject dialogue;
    private float typingSpeed = 0.03f;

    private int dialogueIndex;

    public class Dialogue
    {
        public string line { get; }
        public bool allowPlayerControl { get; }

        public Dialogue(string x) => (line, allowPlayerControl) = (x, false);
        public Dialogue(string x, bool y) => (line, allowPlayerControl) = (x, y);
    }

    private Dialogue[] stageOne = new Dialogue[]
    {
        new Dialogue("Greetings, and welcome to Digital Footprints, where your privacy is our priority."),
        new Dialogue("For your first day, we will be letting you handle low-priority customers."),
        new Dialogue("These are customers who only allow us to look through their public profiles."),
        new Dialogue("Please use this time to familiarize yourself with the company’s software and workflow."),
        new Dialogue("If you have any questions to ask the client, you may right-click the item of concern and talk to them about it."),
        new Dialogue("Do be warned that sending false information to the customer will warrant a penalty."),
        new Dialogue("So make sure to double-check your report before you [SUBMIT]."),
        new Dialogue("We hope that your time with us will be informative and fulfilling, good luck!", true)
    };
    
    private Dialogue[] stageTwo = new Dialogue[]
    {
        new Dialogue("Hello, again! It seems like you're doing really well in this company."),
        new Dialogue("So we've decided to let you handle medium-priority customers."),
        new Dialogue("These are customers who allow us to look into their privacy settings."),
        new Dialogue("More details on proper Privacy Settings can be found in your Company Standards."),
        new Dialogue("Also, you now have access to more social media sites. Make sure to check everything on both sites!"),
        new Dialogue("As always, make sure to double-check your report before you [SUBMIT]."),
        new Dialogue("Good luck!", true)
    };

    private Dialogue[] stageThree = new Dialogue[]
    {
        new Dialogue("Great news! You've been doing so well in this company that we've decided to give you access to high-priority customers."),
        new Dialogue("These are customers who allow us to look into their passwords."),
        new Dialogue("It's a good thing we had you sign that NDA before joining the company! Haha!"),
        new Dialogue("There are more details on proper Password Strength in your Company Standards."),
        new Dialogue("Also, you now have access to more social media sites. Make sure to check everything on all sites!"),
        new Dialogue("As always, make sure to double-check your report before you [SUBMIT]."),
        new Dialogue("Good luck!", true)
    };

    // Start is called before the first frame update
    void Start()
    {
        dialogueIndex = 0;
        world = GameObject.FindGameObjectWithTag("World");

        if (SceneManager.GetActiveScene().name.Equals("Tutorial"))
        {
            pointersPanel = GameObject.FindGameObjectWithTag("PointersPanel");
            StartCoroutine(runTutorial(overlay.Find("Thoughts"), StaticFunction.updateStrings("Alright, let's see what $r's account is like.")));
        }
        else
        {
            StaticFunction.tutorialStart = false;
            if (SceneManager.GetActiveScene().name.Equals("Stage 1"))
            {
                StartCoroutine(runStage(stageOne));
            }
            else if (SceneManager.GetActiveScene().name.Equals("Stage 2"))
            {
                StartCoroutine(runStage(stageTwo));
            }
            else if (SceneManager.GetActiveScene().name.Equals("Stage 3"))
            {
                StartCoroutine(runStage(stageThree));
            }
        }
    }

    IEnumerator typingAnim(Transform textBox, string line)
    {
        for (int j = 0; j < line.Length; j++)
        {
            yield return new WaitForSeconds(typingSpeed);
            textBox.GetComponent<TMPro.TextMeshProUGUI>().text += line[j];

            if (Input.GetMouseButton(0))
            {
                textBox.GetComponent<TMPro.TextMeshProUGUI>().text += line.Substring(j + 1);
                yield return new WaitForSeconds(0.5f);
                break;
            }
        }

        yield return new WaitUntil(() => Input.GetMouseButtonUp(0));
        yield break;
    }

    IEnumerator runTutorial(Transform textBox, string thought)
    {
        textBox.GetComponent<TMPro.TextMeshProUGUI>().text = "";

        yield return new WaitForSeconds(1f);

        yield return StartCoroutine(typingAnim(textBox, thought));

        overlay.SetAsFirstSibling();

        PointerGenerator script = (PointerGenerator)pointersPanel.GetComponent(typeof(PointerGenerator));
        script.tutorialStart();

        yield break;
    }

    IEnumerator runStage(Dialogue[] stageDialogue)
    {
        if (dialogue == null)
        {
            dialogue = Instantiate(
                dialoguePrefab,
                new Vector3(959.9996948242188f,540.764892578125f,0.0f),
                Quaternion.identity,
                world.transform);
        }

        Transform textBox = dialogue.transform.Find("BG").Find("Dialogue");

        for (int i = dialogueIndex; i < stageDialogue.Length; i++)
        {
            textBox.GetComponent<TMPro.TextMeshProUGUI>().text = "";

            yield return StartCoroutine(typingAnim(textBox, stageDialogue[i].line));

            if (stageDialogue[i].allowPlayerControl)
            {
                dialogueIndex = i;
                Destroy(dialogue);
                dialogue = null;
                break;
            }
        }

        yield break;
    }

    public void run(Dialogue[] dialogue)
    {
        StartCoroutine(runStage(dialogue));
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            Debug.Log("Choice Index: " + StaticFunction.choiceIndex);
            Debug.Log("Dialogue Line Counter: " + StaticFunction.dialogueLineCounter);
            Debug.Log("Editable is Drawn: " + StaticFunction.editableIsDrawn);
            Debug.Log("Instance Counter: " + StaticFunction.instanceCounter);
            Debug.Log("Tutorial Start: " + StaticFunction.tutorialStart);
        }
    }
}
